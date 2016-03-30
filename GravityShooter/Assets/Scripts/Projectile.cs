using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    void Start()
    {
        switch (type)
        {
            case TYPE.LASER:
                FindObjectOfType<AudioManager>().PlayLaserAudio();
                break;
            case TYPE.ROCKET:
                FindObjectOfType<AudioManager>().PlayRocketAudio();
                break;
        }
        isEnemy = true;
    }

    void Update()
    {
        if(transform.position.x < ScreenBorders.m_bottomLeft.x - 10 ||
           transform.position.y < ScreenBorders.m_bottomLeft.y - 10 ||
           transform.position.x > ScreenBorders.m_topRight.x   + 10 ||
           transform.position.y > ScreenBorders.m_topRight.y   + 10)
        {
            Destroy(gameObject);
        }
     }

    public enum TYPE
    {
        LASER,
        ROCKET,
    }

    public TYPE type;
    public int damage;
    public float speed;
    public bool isEnemy;
}
