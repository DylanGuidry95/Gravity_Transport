using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Base enemy class all enemy objects must inherit from
/// This class contains all function and cariables that define
/// what it is to be an enemy.
/// 
/// When inheriting if you need to get different behavior from
/// this script you can overide the function calls in your script 
/// by calling
///     protected override (function)
/// If you wanna keep the original behavior of the function but add on
/// to it is call base.(function name) inside of the function definition
/// If you wanna completly override the function with out keeping any of
/// it original deffinition you just dont call base.(function name)
/// 
/// Do not add any behaviors to this scrpit unless it is to be implemented 
/// across all enemies.
/// </summary>
public class EnemyBase : MonoBehaviour
{
    /// <summary>
    /// Enum for each of the possible states an enemy can be in
    /// </summary>
    protected enum ENEMYSTATES
    {
        init,
        spawn,
        idle,
        fly,
        special,
        dead,
        count
    }

    [SerializeField]
    protected float movementSpeed; //How fast the enemy moves in world space
    [SerializeField]
    protected float ammoCapacity; //Max ammo the enemy has
    [SerializeField]
    protected float ammoAvailiable;  //Ammo availiable to the enemy
    [SerializeField] 
    protected float fireDelay; //Time between shoots
    [SerializeField]
    protected float bulletSpeed; //speed at which bullets move when fired
    [SerializeField]
    protected int hp; //Hp the enemy has
    [SerializeField]
    protected int ScoreValue; //Points the player is rewareded with when enemy dies
    [SerializeField]
    protected Player player; //Refrence to the play object the enemy is locating
    protected GameObject bullet; //Refrence the bullet prefab the enemy will use to shoot
    [SerializeField]
    protected Vector3 SpawnPosition; //Location the enemy will fly towards when spawned into the game

    [SerializeField]
    protected float timer; //Used to time total time between shoots fired


    protected FSM<ENEMYSTATES> _fsm; //Instance of the FSM

    virtual protected void Awake()
    {
        _fsm = new FSM<ENEMYSTATES>();
        GenerateFSM(); 
    }

    virtual protected void Start()
    {
        player = FindObjectOfType<Player>(); //Locates the player
        _fsm.Transition(_fsm.state, ENEMYSTATES.spawn);
        ammoAvailiable = ammoCapacity;
    }

    /// <summary>
    /// Adds all states and valid state transitions to the fsm.
    /// Some enemies may need to define there own transitions so will
    /// need to override this function refer to the SmEnemy script as an example
    /// </summary>
    virtual protected void GenerateFSM()
    {
        foreach (int i in Enum.GetValues(typeof(ENEMYSTATES)))
        {
            if ((ENEMYSTATES)i != ENEMYSTATES.count)
            {
                _fsm.AddState((ENEMYSTATES)i);
            }
        }

        _fsm.AddTransition(ENEMYSTATES.init, ENEMYSTATES.spawn,false);

        _fsm.AddTransition(ENEMYSTATES.spawn, ENEMYSTATES.idle, false);

        _fsm.AddTransition(ENEMYSTATES.idle, ENEMYSTATES.fly, true);
        _fsm.AddTransition(ENEMYSTATES.idle, ENEMYSTATES.dead, false);

        _fsm.AddTransition(ENEMYSTATES.fly, ENEMYSTATES.dead, false);
    }

    /// <summary>
    /// Spawns the bullet prefab when called in and fires in in the direction of the player
    /// To get different bullet patterens overide the function.
    /// Look at the MdEnemy script for refrence.
    /// </summary>
    [ContextMenu("Fire")]
    virtual protected void Fire()
    {
        //Checks to see if enough time has passed between the last shoot and current time
        if (timer >= fireDelay && player != null && ammoAvailiable > 0)
        {
            //Instantiates the bullet prefab and infront of the barrels of the ship
            bullet = Instantiate(Resources.Load("Bullet"), transform.position + transform.right * -transform.localScale.x, transform.localRotation) as GameObject;

            //Fires the bullets in the direction of the player
            Vector2 Look_at_player = (player.transform.position - transform.position).normalized;
            //Applies a velocity to the the rigidbody of the bullet so it begins to move
            bullet.GetComponent<Rigidbody2D>().velocity = Look_at_player.normalized * bulletSpeed;
            //Subtracts one ammo from the current ammo availiable 
            ammoAvailiable--;
            //resets the timer
            timer = 0;
        }
    }

    /// <summary>
    /// Called when the enemy is hit it will deduct hp from the enemy
    /// and destroy it if the hp is equal to zero.
    /// </summary>
    /// <param name="c"></param>
    virtual protected void OnTriggerEnter2D(Collider2D c)
    {
        //Checks to see if the object collided with the enemey is a projectile and it
        //was not fired by an allied enemy ship
        if(c.GetComponent<Projectile>() && c.GetComponent<Projectile>().isEnemy == false && _fsm.state != ENEMYSTATES.spawn)
        {
            Instantiate(Resources.Load("MultiExsplosion"), c.transform.position, c.transform.localRotation);
            //Destroys the bullet
            Destroy(c.gameObject);
            //Subtracts one hp from the enemy current hp
            hp--;
            //Checks if the hp is equal to zero
            if(hp == 0)
            {
                if(gameObject.GetComponent<SmEnemy>())
                {
                    Instantiate(Resources.Load("SmallExsplosion"), transform.position, transform.localRotation);
                }
                else if (gameObject.GetComponent<MdEnemy>())
                {
                    Instantiate(Resources.Load("BigExsplosion"), transform.position, transform.localRotation);
                    int i = UnityEngine.Random.Range(1, 10);
                    if (i >= 1)
                    {
                        GameObject s = Instantiate(Resources.Load("ShieldItem")) as GameObject;
                        s.transform.position = Vector3.zero;
                    }
                }
                else if(gameObject.GetComponent<LgEnemy>())
                {
                    Instantiate(Resources.Load("BigExsplosion"), transform.position, transform.localRotation);
                }
                //Calls score functions to increase current score
                //Destorys the enemy
                ScoreManager.IncreasScoreBy(ScoreValue);
                Destroy(this.gameObject);
                //Plays the explosion audio
                FindObjectOfType<AudioManager>().PlayExplodeAudio();
            }
        }
    }

    /// <summary>
    /// When the enemy is spawned he will begin to fly toward the spawn position declared.
    /// This should not need to be overrided but if it does just follow the same guidelines for any other function.
    /// </summary>
    virtual protected void EnemySpawn()
    {
        //Checks the distance between the current position and the spawn position
        if (Vector3.Distance(transform.position, SpawnPosition) > .1f)
        {
            //If the enemy is further that .1 unities from the spawn point it will continue to move
            //toward the desired location
            transform.position += (SpawnPosition - transform.position) * (Time.deltaTime * 1);
        }
        else
        {
            //When he reaches the spawnposition he will be set to the idle state awaiting
            //for the player to spawn and then he will begin to fire
            _fsm.Transition(_fsm.state, ENEMYSTATES.idle);
        }
    }
}
