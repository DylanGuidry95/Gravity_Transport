//using System;
using UnityEngine;
using System.Collections;

public class SmallEnemy : MonoBehaviour, EnemyManager
{
    private GameObject player;
    public Rigidbody2D BulletPreb;
    Rigidbody2D rb;
    public int BulletSpeed;
    public int amo;
    public float delay;
    private float timer;
    private int count;

    void Start ()
    {
        count = 0;
        player = GameObject.Find("Player");
    }

    public void Fire()
    {
        Vector2 accuracy = new Vector2(0, Random.Range(-0.1f, 0.1f));
        Rigidbody2D bullet = Instantiate(BulletPreb) as Rigidbody2D;
        bullet.transform.position = transform.position;
        Vector2 playerDir = player.transform.position.normalized;
        bullet.velocity = bullet.velocity + playerDir.normalized * BulletSpeed;
    }

    public bool aim()
    {
        return true;
    }

    public void movement()
    {
        transform.right = transform.position - player.transform.position;   
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        movement();

        timer += Time.deltaTime;
        if (count < amo)
        {
            if (timer > delay)
            {
                Fire();
                timer = 0;
                count++;
            }
        }
        
	}

    
}
