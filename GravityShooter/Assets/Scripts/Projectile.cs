using UnityEngine;
using System.Collections;
[RequireComponent (typeof(Rigidbody2D))]

public class Projectile : MonoBehaviour
{
    void Start()
    {
        FindObjectOfType<AudioManager>().PlayLaserAudio();
        isEnemy = true;
    }

    public int damage;
    public float speed;
    public bool isEnemy;
}
