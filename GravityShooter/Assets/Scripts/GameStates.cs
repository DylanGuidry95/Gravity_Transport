using UnityEngine;
using System.Collections.Generic;
using System;
using System.Collections;

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
        GameStateListen();
    }

    void Start()
    {
        _fsm.Transition(_fsm.state ,GAMESTATE.mainMenu);
        DefineControls();
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

    /// <summary>
    /// Defines all the messages we are listening for
    /// </summary>
    void GameStateListen()
    {
        Messenger.AddListener("Player has died",GameOver); //Broadcasted from the Player
    }

    void StateProperties()
    {
        switch(_fsm.state)
        {
            case GAMESTATE.init:
                _fsm.Transition(_fsm.state, GAMESTATE.mainMenu);
                Messenger.Broadcast<string>("Entering the main menu", "GameStae:MainMenu"); //Listened to by the GUI
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
                //Messenger.Broadcast("Player has been defeated");
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

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            _fsm.Transition(_fsm.state, GAMESTATE.gamePlay);
            StateProperties();
        }
    }

    /// <summary>
    /// Called when the game is starting up
    /// </summary>
    void DefineControls()
    {
        ////Player Movement controls
        Messenger.Broadcast<KeyCode, string, INPUT_DEVICE>("Adding Control", KeyCode.W, "Player:Movement_Up", INPUT_DEVICE.KEYBOARD); //Listened to by the InputHandler
        Messenger.Broadcast<KeyCode, string, INPUT_DEVICE>("Adding Control", KeyCode.S, "Player:Movement_Down", INPUT_DEVICE.KEYBOARD); //Listened to by the InputHandler
        Messenger.Broadcast<KeyCode, string, INPUT_DEVICE>("Adding Control", KeyCode.A, "Player:Movement_Left", INPUT_DEVICE.KEYBOARD); //Listened to by the InputHandler
        Messenger.Broadcast<KeyCode, string, INPUT_DEVICE>("Adding Control", KeyCode.D, "Player:Movement_Right", INPUT_DEVICE.KEYBOARD); //Listened to by the InputHandler
    }
}
