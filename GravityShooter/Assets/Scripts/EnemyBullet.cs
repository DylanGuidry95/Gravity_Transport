using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {


	void Start ()
    {
       
	}
	
	void Update ()
    {
        transform.position += Vector3.right * Time.deltaTime * 10;
	}
}
