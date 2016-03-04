using UnityEngine;
using System.Collections.Generic;
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

    protected enum PLAYERACTIONS
    {
        die,
        spawn,
        takeDamage,
        none
    }

    private PLAYERACTIONS _cAction;
    protected PLAYERACTIONS cAction
    {
        get
        {
            return _cAction;
        }
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

    [Header("Lives and Health")]
    [SerializeField]
    private int maxHealth; //Max amount of health the player can have at any time during the game
    [SerializeField]
    private int currentHealth; //Currrent health the player has
    [SerializeField]
    private int maxLives; //Max amount of lives the player can have at any time during the game
    [SerializeField]
    private int livesRemaining; //Curremt lives the player has remainng before game over

    [Header("Movement")]
    [SerializeField]
    private float movementSpeed; //Speed at which the player is accelerating at
    [SerializeField]
    private float maxVelocity; //Fastest the player can be moving

    private Vector3 velocity; //Speed at which the player is moving the scene
    private Vector3 acceleration; //Rate at which the player is gaining speed towards its max velocity

    [SerializeField]
    private Vector3 startPosition = new Vector3(-8, 1, 0);
    [SerializeField]
    private Vector3 spawnPosition = new Vector3(-10, 1, 0);

    private float buttonDownTime; //Used to move the player faster of slower depending on the time between key pressed and key up

    [Header("Margins")]
    [SerializeField]
    private float topBorder;
    [SerializeField]
    private float botBorder;
    [SerializeField]
    private float leftBorder;
    [SerializeField]
    private float rightBorder;

    private bool atTop;
    private bool atBot;
    [SerializeField]
    private bool atLeft;
    private bool atRight;

    [SerializeField]
    private GravityWell well;

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
        CheckPlayerBounds();
        currentHealth = maxHealth;
        livesRemaining = maxLives;
        well = FindObjectOfType<GravityWell>();
        gameObject.GetComponent<SpringJoint2D>().connectedAnchor = well.transform.position;
        well.transform.position = new Vector3(spawnPosition.x - 2, spawnPosition.y, spawnPosition.z);
        transform.position = spawnPosition;
        _fsm.Transition(_fsm.state, PLAYERSTATES.dead);
        //_fsm.Transition(_fsm.state, PLAYERSTATES.idle);
    }

    /// <summary>
    /// Braodcasts messages from the player to be listened to some other object
    /// when the player is created
    /// </summary>
    void PlayerBroadcast()
    {
        Messenger.Broadcast<int>("Player Created", maxHealth); //Listend to by the GUI
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
        _fsm.AddTransition(PLAYERSTATES.init, PLAYERSTATES.dead, false);
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
        PlayerSpawn();

        if(_fsm.state != PLAYERSTATES.dead)
        {
            if (buttonDownTime == 0)
                _fsm.Transition(_fsm.state, PLAYERSTATES.idle);
            ///////////////////////////////////////////////
            /////Math for movement
            //Checks to see if the player is not accelerating faster than the max acceleration rate set
            if (acceleration.magnitude > 5)
            {
                //If it tries to accelerate faster than the max acceleration rate it will normalize the acceleration
                //to slow it down
                acceleration = acceleration.normalized;
            }
            CheckPlayerBounds();
            //Sets the velocity = current velocity + acceleration
            velocity = velocity + acceleration;
            //Check to see if the player is exceeding its max velocity
            if (velocity.magnitude > maxVelocity)
            {
                //If it exceeds the max velocity set the velocity = the normalized velocity
                velocity = velocity.normalized;
            }
            if (atTop && velocity.y > 0)
                velocity.y = 0;
            if (atBot && velocity.y < 0)
                velocity.y = 0;
            if (atLeft && velocity.x < 0)
                velocity.x = 0;
            if (atRight && velocity.x > 0)
                velocity.x = 0;
            //To move the object we take its current position and add it to the velocity * how long any of the movement keys have been held down for
            transform.position += velocity * buttonDownTime;
            //Sets the buttonDownTime to 0 to stop the player from moving while no movement inputs are happening
            buttonDownTime = 0;
            ///////////////////////////////////////////////
        }
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
        //Checks to see if the string at the index after the first split which is after the ':' character equals "Movement"
        if (temp[1] == "Movement")
        {
            //If true we will call PlayerMovement  and pass the string at the third index as an arguement
            //into the function call
            PlayerMovement(temp[2]);
            _fsm.Transition(_fsm.state, PLAYERSTATES.flying);
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.GetComponent<Projectile>() != null)
        {
            PlayerDamage();
            Destroy(c.gameObject);
        }
    }

    /// <summary>
    /// Changes the player health when he is hit by a bullet or an enemy
    /// and also does the checks to see how much health the player has left
    /// along with how many lives it has remaning.
    /// </summary>
    void PlayerDamage()
    {
        _cAction = PLAYERACTIONS.takeDamage;
        currentHealth -= 1;
        Messenger.Broadcast<int>("Player took damage", currentHealth); //Listened to by the GUI
        if(currentHealth == 0)
        {
            _fsm.Transition(_fsm.state, PLAYERSTATES.dead);
            livesRemaining -= 1;
            if (livesRemaining >= 0)
            {
                _cAction = PLAYERACTIONS.die;
                GetComponent<MeshRenderer>().enabled = false;
                transform.position = spawnPosition;
                PlayerSpawn();
                currentHealth = maxHealth;
                PlayerBroadcast();
            }
            else if (livesRemaining < 0)
            {
                _fsm.Transition(_fsm.state, PLAYERSTATES.destroyed);
                Messenger.Broadcast("Player has died"); //Listened to by the GameState manager
                Destroy(this.gameObject);
                Destroy(well.gameObject);
            }
        }
    }

    /// <summary>
    /// Handles the transitions between the player animations
    /// Will be completed once we have animations to work with
    /// </summary>
    void PlayerAnimation()
    {
        switch(_cAction)
        {
            case PLAYERACTIONS.takeDamage:
                //Plays the damage animation
                break;
            case PLAYERACTIONS.die:
                //Plays the death animation
                break;
            case PLAYERACTIONS.spawn:
                //Plays the spawn animation
                break;

        }
    }

    [ContextMenu("Spawn")]
    void PlayerSpawn()
    {
        GetComponent<MeshRenderer>().enabled = true;
        if (Vector3.Distance(transform.position, startPosition) > .1 && _fsm.state == PLAYERSTATES.dead)
        {
            transform.position += new Vector3(1, 0, 0) * (Time.deltaTime * movementSpeed);
        }
        else
        {
            _fsm.Transition(_fsm.state, PLAYERSTATES.idle);
        }

    }

    /// <summary>
    /// Checks to see if the player is staying with in 
    /// it movement bounds
    /// </summary>
    void CheckPlayerBounds()
    {
        if (_fsm.state != PLAYERSTATES.dead)
        {
            if (transform.position.x <= leftBorder)
            {
                atLeft = true;
            }
            else
            {
                atLeft = false;
            }
            if (transform.position.x >= rightBorder)
            {
                atRight = true;
            }
            else
            {
                atRight = false;
            }
            if (transform.position.y <= botBorder)
            {
                atBot = true;
            }
            else
            {
                atBot = false;
            }
            if (transform.position.y >= topBorder)
            {
                atTop = true;
            }
            else
            {
                atTop = false;
            }
        } 
    }
}
