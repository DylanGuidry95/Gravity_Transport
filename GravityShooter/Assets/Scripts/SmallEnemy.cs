//using System;
using UnityEngine;
using System.Collections;

public class SmallEnemy : MonoBehaviour, EnemyManager
{
    private GameObject player;
    public GameObject BulletPreb;
    Rigidbody2D rb;
    public int BulletSpeed;
    public int amo;
    public float delay;
    private float timer;
    private int count;

    void Start ()
    {
        count = 0;
        if (GameObject.Find("Player") != null)
        { player = GameObject.Find("Player"); }
    }

    public void Fire()
    {
        //Vector2 accuracy = new Vector2(0, Random.Range(-0.1f, 0.1f));
        GameObject bullet = Instantiate(BulletPreb) as GameObject;
        bullet.transform.position = transform.position;

        Vector2 playerDir = player.transform.position.normalized;
        bullet.GetComponent<Rigidbody2D>().velocity += playerDir.normalized * BulletSpeed;
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
