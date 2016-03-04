using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour {

    public GameObject BulletPreb; // bullet prefab 
    public GameObject ball; // player
    public float bulletSpeed;

    float delay, timer;
	void Start ()
    {
        delay = 2.0f;
    }

	void FixedUpdate ()
    {
        EnemyMovement.movementSpeed = 0.002f;
        EnemyMovement.smallEnemyMovement(gameObject.GetComponent<Rigidbody2D>());

        if (Mathf.Abs(transform.position.y - ball.transform.position.y) < 1.0f)
        {
            //Vector3 playernormal = ball.transform.position.normalized;
            // fire bullet
            timer += Time.deltaTime;
            if (timer > delay)
            {
                GameObject bullet = Instantiate(BulletPreb) as GameObject;
                bullet.transform.position = transform.position;
                bullet.GetComponent<Rigidbody2D>().velocity += new Vector2(-1,0) * bulletSpeed;
                timer = 0;
            }
        }
    }
}
