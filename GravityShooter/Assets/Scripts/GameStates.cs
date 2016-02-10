using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

public class GameStates : Singleton<GameStates>
{
    enum E_GAMESTATE
    {
        e_Init,
        e_Play,
        e_Pause,
        e_Exit,
        e_Count
    }

    FSM<E_GAMESTATE> _fsm;

    private GameStates Instance;
    public GameStates _instance
    {
        get
        {
            return Instance;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        _fsm = new FSM<E_GAMESTATE>();
        AddState();
        AddTransiton();
        Instance = this;

    }

    void AddState()
    {
        foreach(int i in Enum.GetValues(typeof(E_GAMESTATE)))
        {
            if((E_GAMESTATE)i != E_GAMESTATE.e_Count)
            {
                _fsm.AddState((E_GAMESTATE)i);
            }
        }
    }

    void AddTransiton()
    {
        //From Init
        _fsm.AddTransition(E_GAMESTATE.e_Init, E_GAMESTATE.e_Play, false);

        //From Play
        _fsm.AddTransition(E_GAMESTATE.e_Play, E_GAMESTATE.e_Pause, true);
        _fsm.AddTransition(E_GAMESTATE.e_Play, E_GAMESTATE.e_Exit, false);

        //From Pause
        _fsm.AddTransition(E_GAMESTATE.e_Pause, E_GAMESTATE.e_Exit, false);
    }

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
	
	}
}
