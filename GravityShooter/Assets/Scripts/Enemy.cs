using UnityEngine;
using System.Collections;

[System.Serializable]
public class Enemy : MonoBehaviour {

    public GameObject Bullet;
    public GameObject ball;
    public float bulletSpeed;
    public float spawnRate;

    float delay, timer;
	void Start ()
    {
        delay = 2.0f;
    }

	void Update ()
    {
        if (Mathf.Abs(transform.position.y - ball.transform.position.y) < 1.0f)
        {
            Vector3 playernormal = ball.transform.position.normalized;
            // fire bullet
            timer += Time.deltaTime;
            if (timer > delay)
            {
                GameObject bullet = Instantiate(Bullet) as GameObject;
                bullet.transform.position = transform.position;
                bullet.GetComponent<Rigidbody2D>().velocity += new Vector2(1,0) * bulletSpeed;
                timer = 0;
            }
        }
    }
}
