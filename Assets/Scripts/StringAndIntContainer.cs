using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// Written by William Holman in October-November 2018.
/// 
/// This is a simple container class for a string and an int.
/// 
/// </summary>

public class StringAndIntContainer {

    public int amountInspeted = 0;
    public string nameOfObject = "Blank";

    public StringAndIntContainer(string newName, int newAmount)
    {
        nameOfObject = newName;
        amountInspeted = newAmount;
    }


}
