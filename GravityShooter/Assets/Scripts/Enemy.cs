using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject Bullet;
    public GameObject enemy;

    float delay, Timer;
    Vector3 pos = new Vector3(1, 0, 0);
	void Start ()
    {
        delay = 3.0f;
	}
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Timer = 0;
            GameObject bullet = Instantiate(Bullet, enemy.transform.position, enemy.transform.rotation) as GameObject;
            bullet.transform.parent = transform;
        }
    }
}
