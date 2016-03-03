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

    FSM<PLAYERSTATES> _fsm; //Used a refrence to an instance of the state machine for the player

    [SerializeField]
    private float movementSpeed;
    [SerializeField]
    private float maxHealth;
    [SerializeField]
    private float currentHealth;

    [SerializeField]
    float mass;

    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private Vector3 acceleration;

    [SerializeField]
    private float maxVelocity;

    public float buttonDownTime;
    public float decell;
    /// <summary>
    /// Power ups will be added later on
    /// </summary>

    void Awake()
    {
        _fsm = new FSM<PLAYERSTATES>();
        AddToFSM();
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

    void Update()
    {
        Movement();        
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
            Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A))
            buttonDownTime = Time.deltaTime * movementSpeed;
        else if(buttonDownTime > 0)
        {
            buttonDownTime -= Time.deltaTime * (velocity.magnitude * buttonDownTime);
            if (buttonDownTime < 0)
                buttonDownTime = 0;
        }

        if (Input.GetKey(KeyCode.W))
        {
            acceleration += new Vector3(0, 1, 1) * movementSpeed;
        }
        if (Input.GetKey(KeyCode.S))
        {
            acceleration += new Vector3(0, -1, 1) * movementSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            acceleration += new Vector3(-1, 0, 1) * movementSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            acceleration += new Vector3(1, 0, 1) * movementSpeed;
        }

        if(acceleration.magnitude > 5)
        {
            acceleration = acceleration.normalized;
        }
        velocity = velocity.normalized + acceleration;
        if(velocity.magnitude > maxVelocity)
        {
            velocity = velocity.normalized;
        }
        transform.position += velocity * buttonDownTime;
        //buttonDownTime = 0;
    }

}
