using UnityEngine;
using UnityEngine.VR;

namespace VRStandardAssets.Utils
{
    // This class makes sure the head tracking behaves correctly when the application is paused.
    public class VRTrackingReset : MonoBehaviour
    {
        private void OnApplicationPause(bool pauseStatus)
        {
            UnityEngine.XR.InputTracking.Recenter();
        }
    }
}