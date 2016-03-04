using UnityEngine;
using System.Collections;


public class Enemy : MonoBehaviour {

    public GameObject BulletPreb; // bullet prefab 
    public GameObject m_player; // player
    public float bulletSpeed;
    float delay, timer;
    public int enemy_value;
    bool enemy_aim;

    public enum enemyMovementType { m_Boss, m_Large, m_Medium, m_Small };
    public enum enemyAiming { a_Boss, a_Large, a_Medium, a_Small };
    public enum enemyValue { v_Boss, v_Large, v_Medium, v_Small };
    public enemyValue e_value;
    public enemyAiming e_aim;
    public enemyMovementType e_movement; 

	void Start ()
    {
        delay = 2.0f;
    }

    void Fire()
    {
        GameObject bullet = Instantiate(BulletPreb) as GameObject;
        bullet.transform.position = transform.position;
        bullet.GetComponent<Rigidbody2D>().velocity += new Vector2(-1, 0) * bulletSpeed;
    }


	void FixedUpdate ()
    {
        switch (e_value)
        {
            case enemyValue.v_Boss:
                enemy_value = 100;
                break;
            case enemyValue.v_Large:
                enemy_value = 50;
                break;
            case enemyValue.v_Medium:
                enemy_value = 30;
                break;
            case enemyValue.v_Small:
                enemy_value = 20;
                break;
        }

        switch(e_aim)
        {
            case enemyAiming.a_Small:
                enemy_aim = EnemyAim.smallAim(m_player.transform.position, transform.position);
                break;
        }

        switch (e_movement)
        {
            case enemyMovementType.m_Small:
                EnemyMovement.movementSpeed = 0.002f;
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
