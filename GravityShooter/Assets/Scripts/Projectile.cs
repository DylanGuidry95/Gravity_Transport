/// ERIC MOULEDOUX
using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody2D))]

///<summary>
/// Defining class for all projectiles
///</summary>
public class Projectile : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayLaserAudio();  // On spawn play the "laser" sound
        isEnemy = true;                                     // All projectiles belong to enemies at start
    }

    void Update()
    {   
        if(transform.position.x < ScreenBorders.m_bottomLeft.x - 10 ||
           transform.position.y < ScreenBorders.m_bottomLeft.y - 10 ||
           transform.position.x > ScreenBorders.m_topRight.x   + 10 ||
           transform.position.y > ScreenBorders.m_topRight.y   + 10)    // If the projectile is off the screen
        {
            Destroy(gameObject);                                            // Destroy it
        }
     }

    public float speed;
    public bool isEnemy;
}
