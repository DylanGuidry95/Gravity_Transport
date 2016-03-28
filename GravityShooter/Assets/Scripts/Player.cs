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

    [SerializeField]
    private bool atTop;
    [SerializeField]
    private bool atBot;
    [SerializeField]
    private bool atLeft;
    [SerializeField]
    private bool atRight;

    [SerializeField]
    private GravityWell well;

    [SerializeField]
    private float padding = 0.5f;
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
    }

    /// <summary>
    /// Function calls
    /// PlayerBroadcast()
    /// </summary>
    void Start()
    {
        gameObject.name = "Player";
        CheckPlayerBounds();
        currentHealth = maxHealth;
        livesRemaining = maxLives;
        SetPlayerControls();
        well = FindObjectOfType<GravityWell>();
        foreach(SpringJoint2D s in gameObject.GetComponents<SpringJoint2D>())
        {
            s.connectedBody = well.GetComponent<Rigidbody2D>();
        }
        well.transform.position = new Vector3(spawnPosition.x - 2, spawnPosition.y, spawnPosition.z);
        transform.position = spawnPosition;
        _fsm.Transition(_fsm.state, PLAYERSTATES.dead);
        //_fsm.Transition(_fsm.state, PLAYERSTATES.idle);
        GUIManager.instance.ChangeHealth(currentHealth);
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
        if (c.GetComponent<Projectile>() != null || c.GetComponent<SmallEnemy>() != null)
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
    [ContextMenu("DMG")]
    void PlayerDamage()
    {
        _cAction = PLAYERACTIONS.takeDamage;
        currentHealth -= 1;
        if(currentHealth == 0)
        {
            _fsm.Transition(_fsm.state, PLAYERSTATES.dead);
            livesRemaining -= 1;
            if (livesRemaining >= 0)
            {
                _cAction = PLAYERACTIONS.die;
                transform.position = spawnPosition;
                well.transform.position = spawnPosition;
                PlayerSpawn();
                currentHealth = maxHealth;
            }
            else if (livesRemaining < 0)
            {
                _fsm.Transition(_fsm.state, PLAYERSTATES.destroyed);
            }
        }
        GUIManager.instance.ChangeHealth(currentHealth);
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

    /// <summary>
    /// Spawns the player into the scene and does its spawn animation
    /// </summary>
    void PlayerSpawn()
    {
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
}
