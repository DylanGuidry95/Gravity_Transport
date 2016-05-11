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

    protected static FSM<GAMESTATE> _fsm = new FSM<GAMESTATE>();

    private GameStates Instance; //Used to get the instance of the Gamestate
    public GameStates _instance
    {
        get
        {
            return Instance;
        }
    }

    private static Player player; //Refrence to the player
    private static GravityWell gravityWell; //Refrence to the gravitywel
    private static EntityManager WaveSpawner; //Refrece to the wave spawner
    private static string SpawnerName = "Spawner"; //Name used to load the Spawner into the game
    private static string PlayerName = "Player"; //Name used to load the Player into the game
    private static string GravityWellName = "GravityWell"; //Name used to load the GravityWell into the game

    public static bool ExitGamePlay = false; //Check for if we are exiting the game

    protected override void Awake()
    {
        base.Awake();
        if(_fsm == null)
            _fsm = new FSM<GAMESTATE>();
        AddState();
        AddTransiton();
        Instance = this;
    }

    void Start()
    {
        StateProperties();
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
    /// Defines all actions that can happen depending the state the game is in
    /// </summary>
    static void StateProperties()
    {
        ExitGamePlay = false;
        switch(_fsm.state)
        {
            case GAMESTATE.init:
                _fsm.Transition(_fsm.state, GAMESTATE.mainMenu);
                break;
            case GAMESTATE.mainMenu:
                Time.timeScale = 1;
                if (player != null)
                    Destroy(player.gameObject);
                if (gravityWell != null)
                    Destroy(gravityWell.gameObject);
                break;
            case GAMESTATE.gamePlay:
                Time.timeScale = 1;
                player = Instantiate(Resources.Load(PlayerName, typeof(Player))) as Player;
                gravityWell = Instantiate(Resources.Load(GravityWellName, typeof(GravityWell))) as GravityWell;
                break;
            case GAMESTATE.pauseMenu:
                break;
            case GAMESTATE.gameOver:
                Destroy(player.gameObject);
                Destroy(gravityWell.gameObject);
                GUIMenuManager.GameOver();
                break;
            case GAMESTATE.exit:
                break;
        }
    }

    //Transitions the game to the game over state
    void GameOver()
    {
        _fsm.Transition(_fsm.state, GAMESTATE.gameOver);
        StateProperties();
    }

    //Changes the current state of the game
    public static void ChangeState(string GameState)
    {
        switch (GameState)
        {
            case "MainMenu":
                LevelLoader.LoadLevel("Main_Menu");
                _fsm.Transition(_fsm.state, GAMESTATE.mainMenu);
                break;
            case "Game":
                LevelLoader.LoadLevel("Level_One");
                _fsm.Transition(_fsm.state, GAMESTATE.gamePlay);
                break;
            case "GameOver":
                LevelLoader.LoadLevel("GameOver");
                _fsm.Transition(_fsm.state, GAMESTATE.gameOver);
                break;
            default:
                break;
        }
        StateProperties();
    }

    void Update()
    {

        if (_fsm.state == GAMESTATE.init)
            _fsm.Transition(_fsm.state, GAMESTATE.mainMenu);

        if (Input.GetKeyDown(KeyCode.T))
        {
            _fsm.Transition(_fsm.state, GAMESTATE.gamePlay);
            StateProperties();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    //Pause the game 
    public static void PauseGame()
    {
        if (Time.timeScale != 0)
        {
            Time.timeScale = 0;
            _fsm.Transition(_fsm.state, GAMESTATE.pauseMenu);
            GUIMenuManager.PauseButton();
        }
        else
        {
            Time.timeScale = 1;
            GUIMenuManager.ResumeButton();
            _fsm.Transition(_fsm.state, GAMESTATE.gamePlay);
        }
    }
}
