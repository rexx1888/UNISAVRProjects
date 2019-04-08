using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This an interace which comes along with the VRInteractiveItem class, for documentation on what I've come to learn about how it works, check the programmer readme in the github repo.
/// 
/// </summary>

public interface IInteractable {
    void Interact();
    void OnHoverEnter();
    void OnHoverExit();
}
