using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class LgEnemy : EnemyBase
{
    Vector3 move;
    protected override void Start()
    {
        move = new Vector3(0, 0.1f, 0);
        hp = 3;
        base.Start();
    }

    protected override void GenerateFSM()
    {
        base.GenerateFSM();
        _fsm.AddTransition(ENEMYSTATES.fly, ENEMYSTATES.dead, false);
        //_fsm.AddTransition(ENEMYSTATES.fly, ENEMYSTATES.special, false);
        //_fsm.AddTransition(ENEMYSTATES.special, ENEMYSTATES.dead, false);
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
                
                break;
            case ENEMYSTATES.fly:
                Movement();
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
        transform.position += move * Time.deltaTime * movementSpeed;
        if (transform.position.y > ScreenBorders.m_topLeft.y)
        {
            move = new Vector3(0, -0.1f, 0);
        }

        if (transform.position.y < ScreenBorders.m_bottomLeft.y)
        {
            move = new Vector3(0, 0.1f, 0);
        }
    }


    void Special()
    {

    }
}
