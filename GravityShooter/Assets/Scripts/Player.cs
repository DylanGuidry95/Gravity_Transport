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

    private Vector3 velocity;
    private Vector3 acceleration;

    public float buttonDownTime;

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
        if (Input.GetKey(KeyCode.W))
        {
            buttonDownTime = Time.deltaTime * movementSpeed;
            acceleration = new Vector3(0, 1, 1) * buttonDownTime;
            velocity += acceleration.normalized * buttonDownTime;
            transform.position += velocity.normalized * buttonDownTime;
        }
        if (Input.GetKey(KeyCode.S))
        {
            buttonDownTime = Time.deltaTime * movementSpeed;
            acceleration = new Vector3(0, -1, 1) * buttonDownTime;
            velocity += acceleration.normalized * buttonDownTime;
            transform.position += velocity.normalized * buttonDownTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            buttonDownTime = Time.deltaTime * movementSpeed;
            acceleration = new Vector3(-1, 0, 1) * buttonDownTime;
            velocity += acceleration.normalized * buttonDownTime;
            transform.position += velocity.normalized * buttonDownTime;
        }
        if (Input.GetKey(KeyCode.D))
        {
            buttonDownTime = Time.deltaTime * movementSpeed;
            acceleration = new Vector3(1, 0, 1) * buttonDownTime;
            velocity += acceleration.normalized * buttonDownTime;
            transform.position += velocity.normalized * buttonDownTime;
        }


    }

}
