using UnityEngine;
using System.Collections.Generic;

public class InputHandler : Singleton<InputHandler>
{
    protected enum E_CONTOLLER
    {
        e_Controller,
        e_KeyBoard,
        e_Count
    }

    protected GameStates GameFlow;

    protected E_CONTOLLER controlType = E_CONTOLLER.e_Count;

	// Use this for initialization
	protected void Start ()
    {
        controlType = CheckControls();
        if(FindObjectOfType<GameStates>() != null)
        {
            GameFlow = FindObjectOfType<GameStates>();
        }
        else
        {
            Debug.LogError("No Object found of type GameStates");
        }
	}

    protected void GameFlowControls(E_CONTOLLER c)
    {
        switch(c)
        {
            case E_CONTOLLER.e_KeyBoard:
                {
                    
                }
                break;
        }
    }

    // Update is called once per frame
    protected void Update ()
    {
	    
	}

    protected E_CONTOLLER CheckControls()
    {
        string[] controls = Input.GetJoystickNames();
        if (controls[0] == "")
        {
            return E_CONTOLLER.e_KeyBoard;
        }
        else
        {
            return E_CONTOLLER.e_Controller;
        }
    }
}
