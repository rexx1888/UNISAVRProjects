using System;
using UnityEngine;

namespace VRStandardAssets.Utils
{
    // This class encapsulates all the input required for most VR games.
    // It has events that can be subscribed to by classes that need specific input.
    // This class must exist in every scene and so can be attached to the main
    // camera for ease.

/// <summary>
/// 
/// The Diagonal swipe directions were added by William Holman in October-November 2018. for details, read the programmer readme in the github repo.
/// 
/// </summary>


    public class VRInput : MonoBehaviour
    {
        //Swipe directions
        public enum SwipeDirection
        {
            NONE,
            UP,
            DOWN,
            LEFT,
            RIGHT, 
            
            SouthWestDiagonal,
            SouthEastDiagonal,
            NorthEastDiagonal,
            NorthWestDiagonal,
        };


        public event Action<SwipeDirection> OnSwipe;       // Called every frame passing in the swipe, including if there is no swipe.
        public event Action OnClick;                                // Called when Fire1 is released and it's not a double click.
        public event Action OnDown;                                 // Called when Fire1 is pressed.
        public event Action OnHold;                                 // Called when Fire1 is held down.
        public event Action OnUp;                                   // Called when Fire1 is released.
        public event Action OnDoubleClick;                          // Called when a double click is detected.
        public event Action OnCancel;                               // Called when Cancel is pressed.


        [SerializeField] protected float m_DoubleClickTime = 0.3f;    //The max time allowed between double clicks
        [SerializeField] protected float m_SwipeWidth = 0.1f;         //The width of a swipe

        protected float f_ButtonHeldTime = 0;
        [SerializeField] protected float f_ButtonMaxHoldTime = 0.1f;
        protected Vector2 swipeData = Vector2.zero;


        protected Vector2 m_MouseDownPosition;                        // The screen position of the mouse when Fire1 is pressed.
        protected Vector2 m_MouseUpPosition;                          // The screen position of the mouse when Fire1 is released.
        protected float m_LastMouseUpTime;                            // The time when Fire1 was last released.
        protected float m_LastHorizontalValue;                        // The previous value of the horizontal axis used to detect keyboard swipes.
        protected float m_LastVerticalValue;                          // The previous value of the vertical axis used to detect keyboard swipes.


        public float DoubleClickTime{ get { return m_DoubleClickTime; } }


        protected void Update()
        {
            CheckInput();
        }


        protected void CheckInput()
        {
            // Set the default swipe to be none.
            SwipeDirection swipe = SwipeDirection.NONE;

            if (Input.GetButtonDown("Fire1"))
            {
                // When Fire1 is pressed record the position of the mouse.
                m_MouseDownPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            
                // If anything has subscribed to OnDown call it.
                if (OnDown != null)
                    OnDown();
            }
            
            //for when the button is held down
            if(Input.GetButton("Fire1"))
            {
                f_ButtonHeldTime += Time.deltaTime;
                if(f_ButtonHeldTime >= f_ButtonMaxHoldTime)
                {
                    if(OnHold != null)
                    {
                        OnHold();
                    }
                }
            }

            // This if statement is to gather information about the mouse when the button is up.
            if (Input.GetButtonUp ("Fire1"))
            {
                // When Fire1 is released record the position of the mouse.
                m_MouseUpPosition = new Vector2 (Input.mousePosition.x, Input.mousePosition.y);

                // Detect the direction between the mouse positions when Fire1 is pressed and released.
                swipe = DetectSwipe ();
            }

            // If there was no swipe this frame from the mouse, check for a keyboard swipe.
            if (swipe == SwipeDirection.NONE)
                swipe = DetectKeyboardEmulatedSwipe();

            // If there are any subscribers to OnSwipe call it passing in the detected swipe.
            if (OnSwipe != null)
                OnSwipe(swipe);

            // This if statement is to trigger events based on the information gathered before.
            if(Input.GetButtonUp ("Fire1"))
            {
                // If anything has subscribed to OnUp call it.
                if (OnUp != null)
                    OnUp();

                // If the time between the last release of Fire1 and now is less
                // than the allowed double click time then it's a double click.
                if (Time.time - m_LastMouseUpTime < m_DoubleClickTime)
                {
                    // If anything has subscribed to OnDoubleClick call it.
                    if (OnDoubleClick != null)
                        OnDoubleClick();
                }
                else
                {
                    // If it's not a double click, it's a single click.
                    // If anything has subscribed to OnClick call it.
                    if (OnClick != null)
                        OnClick();
                }

                // Record the time when Fire1 is released.
                m_LastMouseUpTime = Time.time;
            }

            // If the Cancel button is pressed and there are subscribers to OnCancel call it.
            if (Input.GetButtonDown("Cancel"))
            {
                if (OnCancel != null)
                    OnCancel();
            }
        }


        protected SwipeDirection DetectSwipe ()
        {
            // Get the direction from the mouse position when Fire1 is pressed to when it is released.
            swipeData = (m_MouseUpPosition - m_MouseDownPosition).normalized;

            // If the direction of the swipe has a small width it is vertical.
            bool swipeIsVertical = Mathf.Abs (swipeData.x) < m_SwipeWidth;

            // If the direction of the swipe has a small height it is horizontal.
            bool swipeIsHorizontal = Mathf.Abs(swipeData.y) < m_SwipeWidth;

            //if the direction has a bit of both directions, its probably diagonal
            bool swipeIsDiagonal = (Mathf.Abs(swipeData.y) > m_SwipeWidth * 0.75f && (Mathf.Abs(swipeData.x) > m_SwipeWidth * 0.75f));

            //if its both vert and horizontal, its diagonal
            if (swipeIsDiagonal)
            {

                // If the swipe has a positive y component and is vertical the swipe is up, AKA north.
                if (swipeData.y > 0)
                {
                    // If the swipe has a positive x component and is horizontal the swipe is left, AKA East
                    if (swipeData.x > 0)
                    {
                        return SwipeDirection.NorthEastDiagonal;
                    }
                    else // If the swipe has a negative x component and is horizontal the swipe is right, AKA West
                    {
                        return SwipeDirection.NorthWestDiagonal;
                    }

                }
                else  // If the swipe has a negative y component and is vertical the swipe is down, AKA south
                {
                    // If the swipe has a positive x component and is horizontal the swipe is right, AKA East
                    if (swipeData.x > 0)
                    {
                        return SwipeDirection.SouthEastDiagonal;
                    }
                    else // If the swipe has a negative x component and is horizontal the swipe is left, AKA West
                    {
                        return SwipeDirection.SouthWestDiagonal;
                    }
                }
            }



            // If the swipe has a positive y component and is vertical the swipe is up.
            if (swipeData.y > 0f && swipeIsVertical)
                return SwipeDirection.UP;

            // If the swipe has a negative y component and is vertical the swipe is down.
            if (swipeData.y < 0f && swipeIsVertical)
                return SwipeDirection.DOWN;

            // If the swipe has a positive x component and is horizontal the swipe is right.
            if (swipeData.x > 0f && swipeIsHorizontal)
                return SwipeDirection.LEFT;

            // If the swipe has a negative x component and is vertical the swipe is left.
            if (swipeData.x < 0f && swipeIsHorizontal)
                return SwipeDirection.RIGHT;

            // If the swipe meets none of these requirements there is no swipe.
            swipeData = Vector2.zero;
            return SwipeDirection.NONE;
        }


        protected SwipeDirection DetectKeyboardEmulatedSwipe ()
        {
            // Store the values for Horizontal and Vertical axes.
            float horizontal = Input.GetAxis ("Horizontal");
            float vertical = Input.GetAxis ("Vertical");

            // Store whether there was horizontal or vertical input before.
            bool noHorizontalInputPreviously = Mathf.Abs (m_LastHorizontalValue) < float.Epsilon;
            bool noVerticalInputPreviously = Mathf.Abs(m_LastVerticalValue) < float.Epsilon;

            // The last horizontal values are now the current ones.
            m_LastHorizontalValue = horizontal;
            m_LastVerticalValue = vertical;

            // If there is positive vertical input now and previously there wasn't the swipe is up.
            if (vertical > 0f && noVerticalInputPreviously)
                return SwipeDirection.UP;

            // If there is negative vertical input now and previously there wasn't the swipe is down.
            if (vertical < 0f && noVerticalInputPreviously)
                return SwipeDirection.DOWN;

            // If there is positive horizontal input now and previously there wasn't the swipe is right.
            if (horizontal > 0f && noHorizontalInputPreviously)
                return SwipeDirection.RIGHT;

            // If there is negative horizontal input now and previously there wasn't the swipe is left.
            if (horizontal < 0f && noHorizontalInputPreviously)
                return SwipeDirection.LEFT;

            // If the swipe meets none of these requirements there is no swipe.
            return SwipeDirection.NONE;
        }
        

        protected void OnDestroy()
        {
            // Ensure that all events are unsubscribed when this is destroyed.
            OnSwipe = null;
            OnClick = null;
            OnDoubleClick = null;
            OnDown = null;
            OnUp = null;
            OnHold = null;
        }


        
    }
}