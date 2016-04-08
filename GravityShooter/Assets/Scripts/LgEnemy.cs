using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LgEnemy : EnemyBase
{
    Vector3 maxBoud;
    Vector3 minBoud;

    protected override void Start()
    {
        ammoCapacity = 500;
        hp = 2;
        maxBoud = transform.position + new Vector3(0, 3, 0);
        minBoud = transform.position + new Vector3(0, -3, 0);
        base.Start();
    }

    protected override void GenerateFSM()
    {
        base.GenerateFSM();
        _fsm.AddTransition(ENEMYSTATES.idle, ENEMYSTATES.special, true);
        _fsm.AddTransition(ENEMYSTATES.fly, ENEMYSTATES.special, true);
        _fsm.AddTransition(ENEMYSTATES.special, ENEMYSTATES.dead, false);
        _fsm.AddTransition(ENEMYSTATES.spawn, ENEMYSTATES.fly, false);
    }

    void Update()
    {
        timer += Time.deltaTime;
        if (player == null)
            player = FindObjectOfType<Player>();
        CheckState();
    }

    void CheckState()
    {
        switch (_fsm.state)
        {
            case ENEMYSTATES.spawn:
                //EnemySpawn();
                _fsm.Transition(_fsm.state, ENEMYSTATES.fly);
                break;
            case ENEMYSTATES.idle:
                //Fire();
                //if (hp == 1)
                //    _fsm.Transition(_fsm.state, ENEMYSTATES.special);
                //_fsm.Transition(_fsm.state, ENEMYSTATES.fly);
                break;
            case ENEMYSTATES.fly:
                Movement();
                //Fire();
                //if (hp == 1)
                //    _fsm.Transition(_fsm.state, ENEMYSTATES.special);
                break;
            case ENEMYSTATES.special:
                break;
            case ENEMYSTATES.dead:
                Destroy(this.gameObject);
                break;
        }
    }
    
    void Movement()
    {
        //List<Vector3> point = new List<Vector3>();
        //Vector3 right = new Vector3(1, 0, 0);
        //Vector3 left = new Vector3(-1, 0, 0);
        //Vector3 up = new Vector3(0, 1, 0);
        //Vector3 down = new Vector3(0, -1, 0);

        if (transform.position.y < maxBoud.y)
        {
            transform.position += new Vector3(0, 0.1f, 0) * (Time.deltaTime * movementSpeed);
        }
    }

    void Special()
    {

    }

    protected override void Fire()
    {
        base.Fire();
    }
}
