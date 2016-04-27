using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*
    Generic Finite State Machine that can be used with just about anything that need to transition between 
    a finite amount of states
*/
public class FSM<T>
{
    List<T> States;  //List of states an object can be and and can be changed for user prefrence

    List<string> TransitionsList = new List<string>(); //list of all possible state transitions for a certain object

    public FSM() //Contructor for the FSM
    {
        States = new List<T>(); //creates a new list of states for each instance of the FSM
        //Debug.Log("State at creation  " + cState);
    }

    public void AddState(T state)
    {
        //Adds states to the list of states for each object
        States.Add(state);
    }
    public void AddTransition(T From, T To, bool backwards)
    {
        string name = From.ToString() + ">" + To.ToString();
        string rev = To.ToString() + ">" + From.ToString();

        if (!TransitionsList.Contains(name))
        {
            TransitionsList.Add(name);
            if (backwards == true && !TransitionsList.Contains(rev))
            {
                TransitionsList.Add(rev);
            }
        } 
    }

    private bool checkTransition(T from, T to)
   {
        //checks the transition an object is trying to make to see if it is valid by checking it against
        //items in the list of valid transitions
        string t = to.ToString();
        string f = from.ToString();
        string valid = f + ">" + t;
        if (TransitionsList.Contains(valid))
            return true;
        return false;
    }

    public bool Transition(T from, T to)
    {
        //If the transition is a valid one it sets the current state of the object to the transition
        //it was trying to transition too
        //If it wasn't the state of the object doesnt change
        //Debug.Log("making transition from " + from.ToString() + " to " + to.ToString());
        if (checkTransition(from, to))
        {
            //Debug.Log("valid transition from " + from.ToString() + " to " + to.ToString());
            cState = to;
            //Debug.Log("New State " + cState.ToString());
            return true;
        }
        else
        {
            //Debug.Log("INVALID TRANSITION NOT CHANGING STATE :: from " + from.ToString() + " to " + to.ToString());
            return false;
        }
    }

    private T cState; //current state

    public T state //gets the cState of the object with out modifying it out side of the FSM
    {
        get
        {
            return cState;
        }
    }

}
