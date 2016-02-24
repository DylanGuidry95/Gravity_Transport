using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject Bullet;
    float delay, Timer;
	void Start ()
    {
        delay = 3.0f;
        
	}
	
	void Update ()
    {
        Timer += Time.deltaTime;
        if (Timer > delay)
        {
            Timer = 0;
            GameObject bullet = Instantiate(Bullet) as GameObject;
            bullet.transform.parent = transform;
        }
    }
}
