using UnityEngine;
using System.Collections;

public class EnemyBullet : MonoBehaviour {

    Enemy en;
    Vector3 pos = new Vector3(0, 0, 0);
	void Start ()
    {
        en = GetComponent<Enemy>();
        pos = en.enemy.transform.forward;
    }
	
	void Update ()
    {
        transform.position = pos;
        transform.position *= Time.deltaTime * 10;
    }
}
