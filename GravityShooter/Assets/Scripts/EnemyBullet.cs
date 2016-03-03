using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    Enemy en;
    Vector3 pos = new Vector3(0, 0, 0);
	void Start ()
    {
    }
	
	void Update ()
    {
        //transform.position = pos;
        transform.position += Vector3.right * Time.deltaTime * 10  ;
    }
}
