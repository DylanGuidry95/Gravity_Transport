using UnityEngine;
using System.Collections;

public class SmEnemy : EnemyBase
{
	// Use this for initialization
	protected override void Start()
    {
        base.Start();
        ScoreValue = 5;
	}

    protected override void GenerateFSM()
    {
        base.GenerateFSM();
        _fsm.AddTransition(ENEMYSTATES.idle, ENEMYSTATES.special, false);
        _fsm.AddTransition(ENEMYSTATES.fly, ENEMYSTATES.special, false);
        _fsm.AddTransition(ENEMYSTATES.special, ENEMYSTATES.dead, false);
    }

    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime;
        if(player == null)
            player = FindObjectOfType<Player>();
        CheckState();
        
        //if (transform.position.x < ScreenBorders.m_bottomLeft.x - 10)    // Check to see if they are still on the screen
        //{                                                                       // if they're not...
        //    Destroy(gameObject);             // Destroy them
        //}
    }

    void CheckState()
    {
        switch (_fsm.state)
        {
            case ENEMYSTATES.spawn:
                EnemySpawn();
                break;
            case ENEMYSTATES.idle:
                _fsm.Transition(_fsm.state, ENEMYSTATES.fly);
                break;
            case ENEMYSTATES.fly:
                Movement();
                Fire();
                break;
            case ENEMYSTATES.special:
                Special();
                break;
            case ENEMYSTATES.dead:
                Destroy(this.gameObject);
                break;
        }
    }

    protected override void Fire()
    {
        base.Fire();
    }

    void Special()
    {
            float enemyAngel = transform.eulerAngles.z * Mathf.Deg2Rad;
            transform.position -= new Vector3(Mathf.Cos(enemyAngel), Mathf.Sin(enemyAngel), 0) * (Time.deltaTime * movementSpeed);
    }

    void Movement()
    {
        bool on_movement = true;
        if (on_movement)
        { transform.right = (transform.position - player.transform.position).normalized; }

        if (ammoAvailiable <= 0)
        {
            on_movement = false;
            transform.right = (transform.position - player.transform.position).normalized;
            if (timer > fireDelay)
            { _fsm.Transition(_fsm.state, ENEMYSTATES.special); timer = 0; }
        }
    }

    public void SetSpawnPosition(Vector3 pos)
    {
        SpawnPosition = pos;
    }
}
