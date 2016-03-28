//using System;
using UnityEngine;
using System.Collections;

public class SmallEnemy : MonoBehaviour, EnemyManager
{
    private GameObject player;
    public GameObject BulletPreb;

    public int BulletSpeed;
    public float EnemySpeed;
    public int amo;
    public float delay;
    private float timer;

    private Vector3 EnemyPos;

    bool turn_movement_on;
    bool found;
    

    void Start ()
    {
        StartCoroutine(findPlayer());
        turn_movement_on = true;
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
            EnemyPos = transform.position;
            Vector2 playerDir = player.transform.position - EnemyPos;
            Vector3 enemyToPlayer = player.transform.position - transform.position;

            Rigidbody2D enemyRB = GetComponent<Rigidbody2D>();
            //float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
            //Vector3 forceDir = enemyToPlayer.normalized;
            enemyRB.velocity = transform.forward * EnemySpeed;
            //enemyRB.AddForce(new Vector2(transform.forward.x, transform.forward.y)* EnemySpeed);
        }
    }

    void FixedUpdate ()
    {
        timer += Time.deltaTime;
        if (timer > delay)
        {
            if (amo == 0)
            {
                //MoveTowardPlayer();
                float enemyAngel = transform.eulerAngles.z * Mathf.Deg2Rad;
                transform.position -= new Vector3(Mathf.Cos(enemyAngel), Mathf.Sin(enemyAngel), 0) * EnemySpeed;
                turn_movement_on = false;
            }
            else
            {
                amo--;
                Fire();
                timer = 0;
            }
        }

        if (turn_movement_on == true)
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
