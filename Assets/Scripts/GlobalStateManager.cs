using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//enumerator for game states
public enum GameState
{
    Menu, 
    Loading,
    InGame,
    Inspecting,
    GeneralConcernInput,
    Finished,

}

//enumator for difficulty
public enum Difficulty
{
    Assisted,
    UnAssisted
}

/// <summary>
/// 
/// 
/// Written by William Holman in October-November 2018.
/// 
/// this is a global static class used for the global difficulty and state
/// used for allowing/not allowing things to be called, and keeps track of all objects that have been inspected and how many times.
/// 
/// 
/// </summary>


public static class GlobalStateManager {

    public static GameState curState; //the current state 
    public static GameState prevState; //the previous state
    public static Difficulty curDifficulty; //the current difficulty
    public static event System.Action<GameState> onStateChanged; //event called when the state has been changed

    public static List<StringAndIntContainer> listOfObjectsAndAmountInspected = new List<StringAndIntContainer>(); //list of all objects that have been inspected if they are tracked.


    //Change the global state 
    public static void ChangeState(GameState newState)
    {
        //assign the previous and new state
        prevState = curState;
        curState = newState;

        //if the state changed delegate has any listeners, invoke them.
        if (onStateChanged != null)
        onStateChanged(newState);
    }

    //when an object is inspected and its tracked, it calls this to add its name
    public static void AddInspection(string nameOfObject)
    {
        //does this object already exist in the list?
        bool alreadyExists = false;
        //foreach string and int container (literally just a string and an int container class) 
        //the list is a list of objects names and the amount of times they've been inspected
        foreach (StringAndIntContainer saic in listOfObjectsAndAmountInspected)
        {
            //if the object already exists in the list
            if (saic.nameOfObject.ToLower() == nameOfObject.ToLower())
            {
                //set already exists to be true
                alreadyExists = true;
                //increase the amount of times its been viewed
                saic.amountInspeted++;
            }
        }

        //if it doesn't already exist
        if (alreadyExists == false)
        {
            //create a new instance of the object name and amount of times viewed in the list
            StringAndIntContainer tempSAIC = new StringAndIntContainer(nameOfObject, 1);
            listOfObjectsAndAmountInspected.Add(tempSAIC);
        }


    }

}
