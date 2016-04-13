using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System;

public class LgEnemy : EnemyBase
{
    Vector3 move;
    protected override void Start()
    {
        move = new Vector3(0, 0.1f, 0);
        hp = 2;
        ScoreValue = 25;
        StartCoroutine(Special());
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

    void changeDirection(Vector3 direction)
    {
        bool timerRun;
        timerRun = true;
        if (timerRun)
        {
            timer += Time.deltaTime;
        }

        movementSpeed = 0; // stop the enemy
        if (timer > fireDelay)
        {
            fireDelay = Random.Range(1, 4);  // random number for how long the enemy need to wait
            move = direction;
            movementSpeed = 10;
            timer = 0;
            timerRun = false;
        }
    }

    void Movement()
    {
        transform.position += move * Time.deltaTime * movementSpeed;

        if (transform.position.y > ScreenBorders.m_topLeft.y + 1.5f)
        {
            changeDirection(new Vector3(0, -0.1f, 0));       
        }

        if (transform.position.y < ScreenBorders.m_bottomLeft.y - 1.5f)
        { 
            changeDirection(new Vector3(0, 0.1f, 0));
        }
    }


    protected override void Fire()
    {
        base.Fire();
    }

    IEnumerator Special()
    {
        while (hp == 1 || hp < 3)
        {
            yield return new WaitForSeconds(10);
            hp++; 
        }
    }
}
