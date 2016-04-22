using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MdEnemy : EnemyBase
{
    public int MultiShoot;
    bool switchDir = false;
    public float TravelDistance;
    float initDelay;
    // Use this for initialization
    protected override void Start()
    {
        ammoCapacity = 500;
        hp = 2;
        ScoreValue = 15;
        initDelay = fireDelay;
        base.Start();
    }

    protected override void GenerateFSM()
    {
        base.GenerateFSM();
        _fsm.AddTransition(ENEMYSTATES.idle, ENEMYSTATES.special, true);
        _fsm.AddTransition(ENEMYSTATES.fly, ENEMYSTATES.special, true);
        _fsm.AddTransition(ENEMYSTATES.special, ENEMYSTATES.dead, false);
    }
    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime;
        if (player == null)
            player = FindObjectOfType<Player>();
        CheckState();
    }

    /// <summary>
    /// Checks the current state the enemy is in and invoke specific methods based
    /// on the current state
    /// </summary>
    void CheckState()
    {
        switch (_fsm.state)
        {
            case ENEMYSTATES.spawn:
                EnemySpawn();
                break;
            case ENEMYSTATES.idle:
                Fire();
                if (hp == 1)
                    _fsm.Transition(_fsm.state, ENEMYSTATES.special);
                _fsm.Transition(_fsm.state, ENEMYSTATES.fly);
                break;
            case ENEMYSTATES.fly:
                Movement();
                Fire();
                if (hp == 1)
                    _fsm.Transition(_fsm.state, ENEMYSTATES.special);
                break;
            case ENEMYSTATES.special:
                Special();
                Movement();
                Fire();

                break;
            case ENEMYSTATES.dead:
                Destroy(this.gameObject);
                break;
        }
    }
    
    [ContextMenu("Fire")]
    protected override void Fire()
    {
        if (timer >= fireDelay && player != null && ammoAvailiable > 0)
        {
            float y_offset = -.5f;
            List<GameObject> shoots = new List<GameObject>();
            for(int i = 0; i < MultiShoot; i++)
            {
                GameObject a = Instantiate(Resources.Load("Bullet")) as GameObject;
                a.transform.position = transform.position + transform.right * -transform.localScale.x;
                a.transform.position += new Vector3(0, y_offset, 0);
                y_offset += .5f;
                shoots.Add(a);
            }
            foreach(GameObject a in shoots)
            {
                Vector2 Look_at_player = (player.transform.position - transform.position).normalized;
                a.GetComponent<Rigidbody2D>().velocity = Vector3.left * bulletSpeed;
                timer = 0;
            }
        }
    }

    /// <summary>
    /// Movement behavior for the enemy 
    /// </summary>
    void Movement()
    {
       
        Vector3 UpMax = new Vector3(SpawnPosition.x, SpawnPosition.y + TravelDistance, 0);
        Vector3 DownMax = new Vector3(SpawnPosition.x, SpawnPosition.y - TravelDistance, 0);
        
        if (Vector3.Distance(transform.position,UpMax) > .1f && switchDir != true)
        {
            transform.position += new Vector3(0, .1f, 0) * (Time.deltaTime * movementSpeed);
            if(transform.position.y > UpMax.y) { switchDir = true; }
        }
        else
        {
            switchDir = true;
        }

        if(Vector3.Distance(transform.position, DownMax) > .1f && switchDir == true)
        {
            transform.position -= new Vector3(0, .1f, 0) * (Time.deltaTime * movementSpeed);
            if(transform.position.y < DownMax.y) { switchDir = false; }
        }
        else
        {
            switchDir = false;
        }
    }

    /// <summary>
    /// Lowers the firedelay to make the enemy fire faster
    /// </summary>
    void Special()
    {
        if(fireDelay == initDelay)
        {
            fireDelay = 1;
        }
    }
}
