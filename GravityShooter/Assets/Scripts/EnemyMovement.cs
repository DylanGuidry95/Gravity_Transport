using UnityEngine;
using System.Collections;

static public class EnemyMovement
{
    static public float movementSpeed; 

    static public void smallEnemyMovement(Rigidbody2D enemy)
    {
        enemy.velocity += new Vector2(-1, 0) * movementSpeed;
    }
}
