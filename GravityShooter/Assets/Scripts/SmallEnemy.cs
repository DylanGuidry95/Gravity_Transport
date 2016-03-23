//using System;
using UnityEngine;
using System.Collections;

public class SmallEnemy : MonoBehaviour, EnemyManager
{
    private GameObject player;
    public GameObject BulletPreb;
    public GameObject RestPoint;

    public int BulletSpeed;
    public float EnemySpeed;
    public int amo;
    public float delay;
    private float timer;

    private Vector3 EnemyPos; 

    bool on;
    

    void Start ()
    {
        EnemyPos = transform.position;
        StartCoroutine(findPlayer());
        on = true;
    }

    IEnumerator findPlayer()
    {
        while (player == null)
        {
            player = GameObject.Find("Player");
            yield return null;
        }
    }

    public void Fire()
    {
        if (player != null)
        {
            GameObject bullet = Instantiate(BulletPreb) as GameObject;
            bullet.transform.position = transform.position + transform.right * -2;

            Vector2 Look_at_player = (player.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = Look_at_player.normalized * BulletSpeed;
        }
    }

    public bool aim()
    {
        return true;
    }

    public void movement()
    {
        if (player != null)
        {
            transform.right = (transform.position - player.transform.position).normalized;
        }
    }

    void MoveTowardPlayer()
    {
        if (player != null)
        {
            Vector2 playerDir = (player.transform.position - EnemyPos).normalized;
            gameObject.GetComponent<Rigidbody2D>().velocity += playerDir.normalized * EnemySpeed;
        }
    }

    void FixedUpdate ()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            if (amo == 0)
            {
                on = false;
                MoveTowardPlayer();
            }
            else
            {
                amo--;
                Fire();
                timer = 0;
            }
        }

        if (on == true)
        {
            movement();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Projectile>())
        {
            Destroy(other.gameObject);
            Destroy(gameObject);
        }
    }
}
