using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    /// <summary>
    /// when player collide with this object. 
    /// This object will terminate itself.
    /// Shield is added to player.
    /// </summary>
    /// <param name="c"> any object with a collider </param>
    void OnTriggerEnter2D(Collider2D c)
    {
        if(c.GetComponent<Projectile>() && c.GetComponent<Projectile>().isEnemy == false || c.GetComponent<Player>())
        {
            Destroy(gameObject);
            Player.AddShield(true);
        }
    }
}
