using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour {

    public GameObject BulletPreb; // bullet prefab 
    public GameObject m_player; // player
    public float bulletSpeed;
    public float delay;
    float timer;
    public int enemy_value;
    bool enemy_aim;
    public float accuracy;

    public enum enemyType { Boss, Large, Medium, Small };
    public enemyType enemy; 

	void Start ()
    {
        m_player = GameObject.Find("Player");
    }

    void Fire()
    {
        GameObject bullet = Instantiate(BulletPreb) as GameObject;
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity += new Vector2(-1, Random.Range(-accuracy, accuracy)) * bulletSpeed;
    }


	void FixedUpdate ()
    {
        if (gameObject.GetComponent<Rigidbody2D>() == null)
        {
            gameObject.AddComponent<Rigidbody2D>();
            gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        }

        switch (enemy)
        {
            case enemyType.Boss:
                enemy_value = 100;
                break;

            case enemyType.Large:
                enemy_value = 50;
                break;

            case enemyType.Medium:
                enemy_value = 30;
                break;

            case enemyType.Small:
                enemy_value = 20;
                enemy_aim = EnemyAim.smallAim(m_player.transform.position, transform.position);

                EnemyMovement.movementSpeed = 0.001f;
                EnemyMovement.smallEnemyMovement(gameObject.GetComponent<Rigidbody2D>());
                break;
        }

        if (enemy_aim)
        {
            // fire bullet
            timer += Time.deltaTime;
            if (timer > delay)
            {
                Fire();
                timer = 0;
            }
        }
    }
}
