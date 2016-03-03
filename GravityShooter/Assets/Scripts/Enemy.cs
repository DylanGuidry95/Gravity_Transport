using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject Bullet;
    //public GameObject playerrotate;

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
            GameObject bullet = Instantiate(Bullet) as GameObject;
            bullet.transform.position = transform.position + Vector3.right;
            bullet.transform.parent = transform;
        }
    }
}
