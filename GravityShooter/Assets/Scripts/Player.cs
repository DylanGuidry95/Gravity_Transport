using UnityEngine;
using System.Collections.Generic;
using System;

public class Player : Singleton<Player>
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

    List<KeyCode> PlayerControls = new List<KeyCode>();

    [Header("Lives and Health")]
    [SerializeField]
    private int maxHealth; //Max amount of health the player can have at any time during the game
    [SerializeField]
    private int currentHealth; //Currrent health the player has
    [SerializeField]
    private int maxLives; //Max amount of lives the player can have at any time during the game
    [SerializeField]
    private int livesRemaining; //Curremt lives the player has remainng before game over

    [SerializeField]
    static bool shield = false;

    [Header("Movement")]
    [SerializeField]
    private float movementSpeed; //Speed at which the player is accelerating at
    [SerializeField]
    private float maxVelocity; //Fastest the player can be moving

    private Vector3 velocity; //Speed at which the player is moving the scene
    private Vector3 acceleration; //Rate at which the player is gaining speed towards its max velocity

    [SerializeField]
    private Vector3 startPosition = new Vector3(-8, 0, 0); //Positon the player is spawned at when created
    [SerializeField]
    private Vector3 spawnPosition = new Vector3(-10, 0, 0); //Position the player flys toward when spawning in to the game scene

    private float buttonDownTime; //Used to move the player faster of slower depending on the time between key pressed and key up

    [SerializeField]
    private bool atTop; //Value to say if the player has reached the top of the screen
    [SerializeField]
    private bool atBot; //Value to say if the player has reached the bottom of the screen
    [SerializeField]
    private bool atLeft; //Value to say if the player has reached the left of the screen
    [SerializeField]
    private bool atRight; //Value to say if the player has reached the right of the screen

    [SerializeField]
    private GravityWell well; //Refrence to the gravity well the player tows around

    [SerializeField]
    private float padding = 0.5f;
    //Power ups will go here later on in development
    [SerializeField]
    public static  PlayerGUI playerGUI;
      

    /// <summary>
    /// Function Calls
    /// AddToFSM()
    /// PlayerListen()
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        _fsm = new FSM<PLAYERSTATES>();
        AddToFSM();
    }

    /// <summary>
    /// Function calls
    /// PlayerBroadcast()
    /// </summary>
    void Start()
    {
        gameObject.name = "Player"; //Changes the name of the object to Player
        CheckPlayerBounds(); //Checks to see if the player in within the game play area
        currentHealth = maxHealth; //Sets the current health equal to the maximumHealth
        livesRemaining = maxLives; //Sets the lives remaining equal the the max lives
        SetPlayerControls(); //Sets the controls the user uses to control the player
        well = FindObjectOfType<GravityWell>(); //Searchs for an object of type GravityWell and sets the return value equal to the well
        foreach(SpringJoint2D s in gameObject.GetComponents<SpringJoint2D>())
        {
            s.connectedBody = well.GetComponent<Rigidbody2D>(); //Sets the connected body of the springs attached to the player equal to the well's rigidbody
        }
        well.transform.position = new Vector3(spawnPosition.x - 2, spawnPosition.y, spawnPosition.z); //Sets the wells position to just behind the player
        transform.position = spawnPosition; //Sets the player's position to be just out side the play area
        _fsm.Transition(_fsm.state, PLAYERSTATES.dead); //Transitions the player to the dead state so it will start its spawning movement
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

    public bool GODMODE = false;

    /// <summary>
    /// Handles all the behavior that needs to happed every frame
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
            GODMODE = !GODMODE;
        gameObject.GetComponent<LineRenderer>().SetPosition(0, transform.position);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, well.transform.position);

        PlayerSpawn();

        if (_fsm.state != PLAYERSTATES.dead)
        {
            PlayerMouseMovement();

            PlayerMovement();
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
    void PlayerMovement()
    {
        foreach(KeyCode kc in PlayerControls)
        {
            if (Input.GetKey(kc))
            {
                if(_fsm.state != PLAYERSTATES.flying)
                {
                    _fsm.Transition(_fsm.state, PLAYERSTATES.flying);
                }
                buttonDownTime = Time.deltaTime * movementSpeed;
                switch (kc)
                {
                    case KeyCode.W:
                        acceleration += new Vector3(0, 1, 0);
                        break;
                    case KeyCode.S:
                        acceleration += new Vector3(0, -1, 0);
                        break;
                    case KeyCode.D:
                        acceleration += new Vector3(1, 0, 0);
                        break;
                    case KeyCode.A:
                        acceleration += new Vector3(-1, 0, 0);
                        break;
                }
            } 
            if(Input.GetKeyUp(kc))
            {
                buttonDownTime = 0;
                acceleration = Vector3.zero;
                _fsm.Transition(_fsm.state, PLAYERSTATES.idle);
            }         
        }
    }

    void PlayerMouseMovement()
    {
        if(Input.GetMouseButton(0))
        {
            buttonDownTime = Time.deltaTime * movementSpeed;
            Vector3 screenPoint = Input.mousePosition;
            screenPoint.z = 10;
            acceleration -= (transform.position - Camera.main.ScreenToWorldPoint(screenPoint)).normalized;
        }
        if(Input.GetMouseButtonUp(0))
        {
            buttonDownTime = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.GetComponent<Projectile>() != null || c.GetComponent<SmEnemy>() != null && _fsm.state != PLAYERSTATES.dead)
        {
            Instantiate(Resources.Load("MultiExsplosion"), c.transform.position, c.transform.localRotation);
            PlayerDamage();
            Destroy(c.gameObject);
        }
    }

    /// <summary>
    /// Changes the player health when he is hit by a bullet or an enemy
    /// and also does the checks to see how much health the player has left
    /// along with how many lives it has remaning.
    /// </summary>
    [ContextMenu("DMG")]
    public void PlayerDamage()
    {
        
        _cAction = PLAYERACTIONS.takeDamage;
        if (shield != true)
            currentHealth -= 1;
        else
        {
            shield = false;
            AddShield(shield);
        }

        if(playerGUI != null)
            playerGUI.HPChange(currentHealth);

        if (currentHealth == 0)
        {
            Instantiate(Resources.Load("BigExsplosion"), transform.position, transform.localRotation);
            EntityManager.ResetWave();
            acceleration = Vector3.zero;
            velocity = Vector3.zero;
            FindObjectOfType<AudioManager>().PlayExplodeAudio();
            _fsm.Transition(_fsm.state, PLAYERSTATES.dead);
            livesRemaining -= 1;
            if (livesRemaining >= 0)
            {
                LivesRemaining.RemoveLife();
                _cAction = PLAYERACTIONS.die;
                transform.position = spawnPosition;
                well.transform.position = spawnPosition;
                PlayerSpawn();
                currentHealth = maxHealth;
                if (GODMODE == true)
                    livesRemaining += 1;
            }
            else if (livesRemaining < 0)
            {
                _fsm.Transition(_fsm.state, PLAYERSTATES.destroyed);
                GameStates.ChangeState("GameOver");
            }
        }

    }

    /// <summary>
    /// Spawns the player into the scene and does its spawn animation
    /// </summary>
    void PlayerSpawn()
    {
        if(playerGUI == null)
            playerGUI = FindObjectOfType<PlayerGUI>();
        if(playerGUI != null)
            playerGUI.HPChange(currentHealth);
        if (Vector3.Distance(transform.position, startPosition) > .5 && _fsm.state == PLAYERSTATES.dead)
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
            if (transform.position.x >= ScreenBorders.m_topRight.x - padding)
            {
                atRight = true;
            }
            else
            {
                atRight = false;
            }
            if (transform.position.x <= ScreenBorders.m_bottomLeft.x + padding)
            {
                atLeft = true;
            }
            else
            {
                atLeft = false;
            }
            if (transform.position.y >= ScreenBorders.m_topRight.y - padding)
            {
                atTop = true;
            }
            else
            {
                atTop = false;
            }
            if (transform.position.y <= ScreenBorders.m_bottomLeft.y + padding)
            {
                atBot = true;
            }
            else
            {
                atBot = false;
            }
        } 
    }

    /// <summary>
    /// Defines the controls the player uses for various actions
    /// </summary>
    void SetPlayerControls()
    {
        PlayerControls.Add(KeyCode.W);
        PlayerControls.Add(KeyCode.S);
        PlayerControls.Add(KeyCode.D);
        PlayerControls.Add(KeyCode.A);
    }

    public static void AddShield(bool s)
    {
        shield = s;
        playerGUI.ShieldChange(shield);    
    }
}
