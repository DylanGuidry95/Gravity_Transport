﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
//using System;

public class LgEnemy : EnemyBase
{
    Vector3 move; // to change the enemy direction
    bool timerRun; // when delta time should run

    protected override void Start()
    {
        move = new Vector3(0, 0.1f, 0);
        hp = 2;
        ScoreValue = 25;
        //StartCoroutine(Special());
        base.Start();
    }

    protected override void GenerateFSM()
    {
        base.GenerateFSM();
        _fsm.AddTransition(ENEMYSTATES.fly, ENEMYSTATES.special, false);
        _fsm.AddTransition(ENEMYSTATES.special, ENEMYSTATES.dead, false);
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
                EnemySpawn();
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

    /// <summary>
    /// Stop this object movement from a random number between 1-4 sec before its start to move again.
    /// Change movement direction
    /// </summary>
    /// <param name="direction">Value to change movement direction</param>
    void changeDirection(Vector3 direction)
    {
        timerRun = true;
        if (timerRun)
        {
            timer += Time.deltaTime;
        }
        
        movementSpeed = 0; // stop the enemy
        if (timer > fireDelay)
        {
            // random number for how long the enemy need to wait
            fireDelay = Random.Range(1, 4); 
            move = direction;
            movementSpeed = 10;
            timer = 0;
            timerRun = false;
        }
    }

    bool intial = true;
    /// <summary>
    /// Randomly movement up or down when spawn.
    /// Moving up and down with the boundary of the screen min and max height
    /// </summary>
    void Movement()
    {
        if(intial == true)
        {
            int i = Random.Range(1,10);
            move = i > 5 ? move : -move;
        }

        transform.position += move * Time.deltaTime * movementSpeed;

        if (transform.position.y > ScreenBorders.m_topLeft.y - 1.5f)
        {
              changeDirection(new Vector3(0, -0.1f, 0));
        }

        if (transform.position.y < ScreenBorders.m_bottomLeft.y + 1.5f)
        {
              changeDirection(new Vector3(0, 0.1f, 0));
        }
        if (intial != false)
            intial = false;
    }


    protected override void Fire()
    {
        base.Fire();
    }

    /// <summary>
    /// when this object health is less then its max health.
    /// After 10 secs health increase by 1
    /// </summary>
    /// <returns></returns>
    IEnumerator Special()
    {
        while (hp == 1 || hp < 3)
        {
            yield return new WaitForSeconds(10);
            hp++; 
        }
    }

    protected override void EnemySpawn()
    {
        base.EnemySpawn();
        _fsm.Transition(_fsm.state, ENEMYSTATES.fly);
    }

    public void CallForHelp()
    {
        int yoffset = 1;
        SmEnemy = new List<GameObject>();
        for (int i = 0; i < 2; i++)
        {
            GameObject enemySpawn = Instantiate(Resources.Load("Enemy1_Small")) as GameObject;
            EntityManager.Entities.Add(enemySpawn);
            enemySpawn.transform.position = new Vector3(ScreenBorders.m_topRight.x - 2, 0, 0);
            Vector3 Spawn = new Vector3(Random.Range(ScreenBorders.m_topRight.x / 2, ScreenBorders.m_topRight.x), Random.Range(0, ScreenBorders.m_topRight.y), 0);
            enemySpawn.GetComponent<SmEnemy>().SetSpawnPosition(Spawn);
            yoffset -= 1;
            if (yoffset <= -1)
                yoffset = 1;
        }
    }

    //protected override void OnTriggerEnter2D(Collider2D c)
    //{
    //    //Checks to see if the object collided with the enemey is a projectile and it
    //    //was not fired by an allied enemy ship
    //    if (c.GetComponent<Projectile>() && c.GetComponent<Projectile>().isEnemy == false && _fsm.state != ENEMYSTATES.spawn)
    //    {
    //        //Destroys the bullet
    //        Destroy(c.gameObject);
    //        //Subtracts one hp from the enemy current hp
    //        hp--;
    //        //Checks if the hp is equal to zero
    //        if (hp == 0)
    //        {
    //            foreach(GameObject g in SmEnemy)
    //            {
    //                Destroy(g);
    //            }
    //            //Calls score functions to increase current score
    //            //Destorys the enemy
    //            ScoreManager.IncreasScoreBy(ScoreValue);
    //            Destroy(this.gameObject);
    //            //Plays the explosion audio
    //            FindObjectOfType<AudioManager>().PlayExplodeAudio();
    //        }
    //    }
    //}

    public List<GameObject> SmEnemy;
}
