using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.SceneManagement;

public class GameStates : Singleton<GameStates>
{
    protected enum GAMESTATE
    {
        init,
        mainMenu,
        gamePlay,
        pauseMenu,
        gameOver,
        exit,
        count
    }

    protected FSM<GAMESTATE> _fsm;

    private GameStates Instance;
    public GameStates _instance
    {
        get
        {
            return Instance;
        }
    }

    private Player player;
    private GravityWell gravityWell;
    private string PlayerName = "Player";
    private string GravityWellName = "GravityWell";

    protected override void Awake()
    {
        base.Awake();
        _fsm = new FSM<GAMESTATE>();
        AddState();
        AddTransiton();
        Instance = this;
    }

    void Start()
    {
        _fsm.Transition(_fsm.state ,GAMESTATE.mainMenu);
    }

    /// <summary>
    /// Adds all the possible state the game can be in
    /// </summary>
    void AddState()
    {
        foreach(int i in Enum.GetValues(typeof(GAMESTATE)))
        {
            if((GAMESTATE)i != GAMESTATE.count)
            {
                _fsm.AddState((GAMESTATE)i);
            }
        }
    }

    /// <summary>
    /// Adds all the valid state transitions the game can make
    /// </summary>
    void AddTransiton()
    {
        ////Transitions from the init
        //init -> mianMenu
        _fsm.AddTransition(GAMESTATE.init, GAMESTATE.mainMenu, false);

        ////Transitons from the mainMenu
        //mainMenu -> gamePlay
        _fsm.AddTransition(GAMESTATE.mainMenu, GAMESTATE.gamePlay, false);
        //mainMenu -> exit
        _fsm.AddTransition(GAMESTATE.mainMenu, GAMESTATE.exit, false);

        ////Transitions from the gamePlay
        //gamePlay <-> pauseMenu
        _fsm.AddTransition(GAMESTATE.gamePlay, GAMESTATE.pauseMenu, true);
        //gamePlay <-> gameOver
        _fsm.AddTransition(GAMESTATE.gamePlay, GAMESTATE.gameOver, true);

        ////Transitions from the pauseMenu
        //pauseMenu -> mainMenu
        _fsm.AddTransition(GAMESTATE.pauseMenu, GAMESTATE.mainMenu, false);
        //pauseMenu -> exit
        _fsm.AddTransition(GAMESTATE.pauseMenu, GAMESTATE.exit, false);

        ////Transitions from the endGame
        //endGame -> mainMenu
        _fsm.AddTransition(GAMESTATE.gameOver, GAMESTATE.mainMenu, false);
        //endGame -> exit
        _fsm.AddTransition(GAMESTATE.gameOver, GAMESTATE.exit, false);
    }

    void StateProperties()
    {
        switch(_fsm.state)
        {
            case GAMESTATE.init:
                _fsm.Transition(_fsm.state, GAMESTATE.mainMenu);
                break;
            case GAMESTATE.mainMenu:
                break;
            case GAMESTATE.gamePlay:
                player = Instantiate(Resources.Load(PlayerName, typeof(Player))) as Player;
                gravityWell = Instantiate(Resources.Load(GravityWellName, typeof(GravityWell))) as GravityWell;
                break;
            case GAMESTATE.pauseMenu:
                break;
            case GAMESTATE.gameOver:
                Destroy(player.gameObject);
                Destroy(gravityWell.gameObject);
                break;
            case GAMESTATE.exit:
                break;
        }
        Debug.Log(_fsm.state);
    }

    void GameOver()
    {
        _fsm.Transition(_fsm.state, GAMESTATE.gameOver);
        StateProperties();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T) && FindObjectOfType<Player>() == null)
        {
            _fsm.Transition(_fsm.state, GAMESTATE.gamePlay);
            StateProperties();
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("Main");
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale != 0)
            {
                Time.timeScale = 0;
                _fsm.Transition(_fsm.state, GAMESTATE.pauseMenu);
            }            
            else
            {
                Time.timeScale = 1;
                _fsm.Transition(_fsm.state,GAMESTATE.gamePlay);
            }

        }
    }
}
