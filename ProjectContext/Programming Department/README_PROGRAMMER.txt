Programmer readme for Home Visit Simulation (VR Project for uniSA)

this is what I (William Holman, the programmer of the prototype team) have come to learn about how the VRStandardAssets from the Unity Asset Store works.
It will also be a description of how we have set the project up, and how some stuff works.


First of all, the VRStandardAssets.
The camera used in all the scenes for controlling the players camera movement is the camera that came as a prefab
in the VRStandardAssets. It consists of a lot of scripts, but the most important is the VR Input script, the  VRInteractive script and the VREyeRaycaster script.



The VRInput script.
This script is on the main camera.
This script detects input from the touchpad on the side of the GearVR.
It treats any touch on the touchpad as a mouse click. Therefore, a tap on the touchpad is a click, a double tap is clicking twice.
The way it detects swipes is by getting the initial 'click' (putting your finger on the pad) position, and getting
the position of when you lifted the finger (releasing the click). it gets the difference between the two tap positions
and checks the distance.
The easiest way to access this script is Camera.main.GetComponent<VRInput>();



The VRInteractiveItem script.
this script detects when an object is first looked at, when it is no longer looked at, when it is clicked on,
double clicked on, when the Fire1 input is pressed/released whilst looking at this object.
This script is attached to any object that should be interacted with.
it makes use of delegates that functions can be added and removed to easily. 
Example: 
So if the player is looking at an object that has this attached, and double taps the touchpad
it will then call the OnDoubleClick event.


The VREyeRaycaster script.
This is attached to the main camera, and shouldn't need to be changed at all but here's what I know if it.
This script is used in tangent with the VRInput script on the camera.
Every frame it performs a raycast and checks for a "VRInteractiveItem" component if that raycast hits anything.
if it does have that component, it keeps a reference of it. if there is any input, such as a double click, from the VRInput sript,
it calls the respective function on the VRInteractiveItem component.
If the object was just looked at, it calls the OnOver delegate event, and the OnOut if the object is no longer looked at but was previously.

That is all I have learnt about those three scripts. 







Now for how the project is set up.


The general gist is that objects in the 360 photo can be 'inspected'. they will then spawn an instance of that object
in front of the player, and the player can swipe to turn the object around and inspect it. 
To do this, there is a "Inspectable Object" class that I created, which will handle this. Put this on an object that has a trigger collider
and attempt to line up the trigger collider with the object from the cameras view. I do this by putting a temporary cube
on the object, rotating the main camera until its looking at the object in the photo to inspect, and trying to place
the cube in the middle of the reticle. Remove the cube, and it now invisible. Check the HouseScene for an example of how this is done.

These are all under a 'Skybox Dependent Object' as I like to call them. Since the above works per photo, it'll need to be done
per photo.
we have the functionality of changing the skybox material (the 360 photo) by clicking on a button/object, and it'll enable/disable
objects that are necessary for each skybox. This is for 'moving around the room' per say, by changing where the players perspective is.



Here's a brief explanation of each scene's intentions

Main Menu Scene:
the main menu consists of just logos and two buttons.
The Un-Assisted button and the Assisted button.
Clicking these buttons will use Load the tutorial scene, and depending on what button you clicked, will assign a difficulty.
the difficulty simply allows or does not allow indicators to pop up when hovering over an interactive object.


Introduction/Tutorial Scene
We wanted to teach the players how to interact with objects, and how to inspect stuff. It simply doesn't allow the continue button
to appear until the player has inspected all three items.
Double click on the items in the tutorial, and inspect them,and their tickbox will tick.
after all 3 have been inspected, the player may continue, as they've learnt how to interact with objects, and how to swipe to inspect.


HouseScene
This is a prototype scene in place of the real one.
It has the base Objects, which is just the camera etc.
It then has the PerSkyboxController.
as explained earlier, each 360 image will have its own set of interactable items. When 'moving around the room' (changing the skybox material),
a base object will be enabled for that specific skybox, and the others will be disabled. 

On top of this, there's also an experimental 'analytics' type thing we tried, its labelled as the "WallOfCubes" under the FinishStateItems object. 
it is, as the name implies, a wall of cubes that surround the player. when the finish button is pressed, these walls become visible, and show
how long the player has looked at that section of the room, and how may times they looked at it. 

Gameplay loop of the HouseScene:
player drops in, can look around. after seeing an object, can double tap on the object. (we only had a couple, as it is a prototype)
if it is inspectable, a canvas will pop up and an inspectable object as well. this object can be spun by swiping on the touchpad.
player can get out of the inspection, and the player can inspect anything repeatedly. 
player can also move between perspectives, changing the skybox material (360 photo) and inspect stuff closer per perspective.
after they are finished, they can click finish and view how long they looked at certain areas of the time, and how many times.



also, an important thing to note with building temporary builds to gearVR is that they require a temporary development key thing. I followed this guide
to set this up properly.

https://gamedevacademy.org/gear-vr-game-development/ (guide to setting it up)
https://dashboard.oculus.com/tools/osig-generator/ (signature for thing)




That's what we did in the 10 days working on the prototype in October-November 2018.
Goodluck future team(s), feel free to change any scripts you want, it's all yours. 

Written by William Holman, 9/11/2018. 
First Prototype team: Beck Rowse (Designer), Tyler Ellul (Artist), and yours truly, William Holman (Programmer)




