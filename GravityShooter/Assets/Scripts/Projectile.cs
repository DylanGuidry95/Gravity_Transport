using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayLaserAudio();
    }

    public int damage;
    public float speed;
}
