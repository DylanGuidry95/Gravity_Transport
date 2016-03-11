using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = dir;
    }

    public Vector3 dir;
    public float m_damage = 1;
}
