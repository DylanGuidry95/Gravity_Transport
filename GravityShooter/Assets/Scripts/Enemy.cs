using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject Bullet;
    public GameObject ball;
    public float bulletSpeed;
    //public GameObject playerrotate;

    float delay, timer;
	void Start ()
    {
        delay = 3.0f;
    }

	void Update ()
    {
        if (Mathf.Abs(transform.position.y - ball.transform.position.y) < 1.0f)
        {
            timer += Time.deltaTime;
            if (timer > delay)
            {
                GameObject bullet = Instantiate(Bullet) as GameObject;
                bullet.transform.position = transform.position;
                bullet.GetComponent<Rigidbody2D>().AddForce(transform.up * bulletSpeed);
                timer = 0;
            }
        }
    }
}
