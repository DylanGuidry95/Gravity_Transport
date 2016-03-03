using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour
{
    /// <summary>
    /// Enum values used to represent each of the states the
    /// player can be in while the game is running
    /// </summary>
    protected enum PLAYERSTATES
    {
        init,
        idle,
        flying,
        dead,
        destroyed,
        count
    }

    private PLAYERSTATES _cState; //the current state of the player
    protected PLAYERSTATES cState //gets the current state of the player
    {
        get
        {
            return _cState;
        }
    }

    private FSM<PLAYERSTATES> _fsm; //Used a refrence to an instance of the state machine for the player

    [SerializeField]
    private float accelerationRate; //How fast the player will speed up 
    [SerializeField]
    private float maxHealth; //Max amount of health the player can have at any time during the game
    [SerializeField]
    private float currentHealth; //Currrent health the player has
    [SerializeField]
    private int maxLives; //Max amount of lives the player can have at any time during the game
    [SerializeField]
    private int livesRemaining; //Curremt lives the player has remainng before game over

    [SerializeField]
    private float movementSpeed; //Speed at which the player is accelerating at
    private Vector3 velocity; //Speed at which the player is moving the scene
    private Vector3 acceleration; //Rate at which the player is gaining speed towards its max velocity
    [SerializeField]
    private float maxVelocity; //Fastest the player can be moving

    private float buttonDownTime; //Used to move the player faster of slower depending on the time between key pressed and key up
    
    //Power ups will go here later on in development

    /// <summary>
    /// Function Calls
    /// AddToFSM()
    /// PlayerListen()
    /// </summary>
    void Awake()
    {
        _fsm = new FSM<PLAYERSTATES>();
        AddToFSM();
        PlayerListen();
    }

    /// <summary>
    /// Function calls
    /// PlayerBroadcast()
    /// </summary>
    void Start()
    {
        PlayerBroadcast();
    }

    /// <summary>
    /// Braodcasts messages from the player to be listened to some other object
    /// </summary>
    void PlayerBroadcast()
    {
        Messenger.Broadcast<KeyCode, string, INPUT_DEVICE>("Adding Control", KeyCode.W, "Player:Movement_Up", INPUT_DEVICE.KEYBOARD); //Listened to by the InputHandler
        Messenger.Broadcast<KeyCode, string, INPUT_DEVICE>("Adding Control", KeyCode.S, "Player:Movement_Down", INPUT_DEVICE.KEYBOARD); //Listened to by the InputHandler
        Messenger.Broadcast<KeyCode, string, INPUT_DEVICE>("Adding Control", KeyCode.A, "Player:Movement_Left", INPUT_DEVICE.KEYBOARD); //Listened to by the InputHandler
        Messenger.Broadcast<KeyCode, string, INPUT_DEVICE>("Adding Control", KeyCode.D, "Player:Movement_Right", INPUT_DEVICE.KEYBOARD); //Listened to by the InputHandler
    }

    /// <summary>
    /// Sets the messages the player will be listening for
    /// all messages recived will be sent into the PlayerActionTriggers function as arguments
    /// </summary>
    void PlayerListen()
    {
        Messenger.AddListener<string>("User triggered the", PlayerActionTriggers); //Braodcasted from the InputHandler
        Messenger.AddListener<string>("Player in taking damage", PlayerActionTriggers); //Braodcasted from an object that wil cause the player to take damage
    }

    /// <summary>
    /// Adds all the states and defines all the valid transitions to the FSM
    /// </summary>
    private void AddToFSM()
    {
        foreach(int i in Enum.GetValues(typeof(PLAYERSTATES)))
        {
            if((PLAYERSTATES)i != PLAYERSTATES.count)
            {
                _fsm.AddState((PLAYERSTATES)i);
            }
        }

        //init -> idle
        _fsm.AddTransition(PLAYERSTATES.init, PLAYERSTATES.idle, false);

        //idle <-> fly
        _fsm.AddTransition(PLAYERSTATES.idle, PLAYERSTATES.flying, true);
        //idle <-> dead
        _fsm.AddTransition(PLAYERSTATES.idle, PLAYERSTATES.dead, true);

        //fly -> dead
        _fsm.AddTransition(PLAYERSTATES.flying, PLAYERSTATES.dead, false);

        //dead -> destroyed
        _fsm.AddTransition(PLAYERSTATES.dead, PLAYERSTATES.destroyed, false);
    }

    /// <summary>
    /// Handles all the behavior that needs to happed every frame
    /// </summary>
    void Update()
    {
        ///////////////////////////////////////////////
        /////Math for movement
        //Checks to see if the player is not accelerating faster than the max acceleration rate set
        if (acceleration.magnitude > 5)
        {
            //If it tries to accelerate faster than the max acceleration rate it will normalize the acceleration
            //to slow it down
            acceleration = acceleration.normalized; 
        }
        //Sets the velocity = current velocity + acceleration
        velocity = velocity + acceleration;
        //Check to see if the player is exceeding its max velocity
        if (velocity.magnitude > maxVelocity)
        {
            //If it exceeds the max velocity set the velocity = the normalized velocity
            velocity = velocity.normalized;
        }
        //To move the object we take its current position and add it to the velocity * how long any of the movement keys have been held down for
        transform.position += velocity * buttonDownTime;
        //Sets the buttonDownTime to 0 to stop the player from moving while no movement inputs are happening
        buttonDownTime = 0;
        ///////////////////////////////////////////////
    }

    /// <summary>
    /// Called from the PlayerActionTriggers function
    /// Calculates the acceleration of the player based on the 
    /// value of the string being passed in
    /// </summary>
    /// <param name="dir">
    /// s should be a direction we want the player to move in
    /// </param>
    void PlayerMovement(string dir)
    {
        //When this function is called the timer for how long a key has been pressed will start
        buttonDownTime = Time.deltaTime * movementSpeed;


        if (dir == "Up")
        {
            //If true accelerates the player in the positive y
            acceleration += new Vector3(0, 1, 1) * movementSpeed;
        }
        if (dir == "Down")
        {
            //If true accelerates the player in the negative y
            acceleration += new Vector3(0, -1, 1) * movementSpeed;
        }
        if (dir == "Left")
        {
            //If true accelerates the player in the negative x
            acceleration += new Vector3(-1, 0, 1) * movementSpeed;
        }
        if (dir == "Right")
        {
            //If true accelerates the player in the positive x
            acceleration += new Vector3(1, 0, 1) * movementSpeed;
        }
    }

    /// <summary>
    /// When the player hears a message it is listening for this function is called
    /// It will parse the msg and depending on the message it recives different functions
    /// will be called
    /// </summary>
    /// <param name="msg">
    /// msg is the message the player was listening for
    /// </param>
    void PlayerActionTriggers(string msg)
    {
        //parses through the message and divides it twice once at the ':' and once and the '_'
        string[] temp = msg.Split(':','_');
        //Checks to see if the string at the index after the first split which is after the ':' character
        if (temp[1] == "Movement")
        {
            //If the string at the second index is equal "Movement" we will call PlayerMovement  and pass the string at the third index as an arguement
            //into the function call
            PlayerMovement(temp[2]);
        }
        else if (temp[1] == "") ;
    }

    void PlayerTakeDamage()
    {

    }
}
