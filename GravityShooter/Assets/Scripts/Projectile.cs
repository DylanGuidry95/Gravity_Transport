using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity =
            new Vector2(-Time.deltaTime, 0) * m_speed;
    }

    public float m_damage = 1;
    public float m_speed = 1;
}
