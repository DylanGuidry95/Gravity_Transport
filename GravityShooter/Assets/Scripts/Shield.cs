using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.GetComponent<Projectile>() && c.GetComponent<Projectile>().isEnemy == false || c.GetComponent<Player>())
        {
            Destroy(gameObject);
            Player.AddShield(true);
        }
    }
}
