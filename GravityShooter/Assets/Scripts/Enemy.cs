using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    public GameObject Bullet;
    //public GameObject playerrotate;
	void Start ()
    {
       
    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(Bullet) as GameObject;
            bullet.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            bullet.transform.rotation = Quaternion.Euler(new Vector3(bullet.transform.rotation.x, bullet.transform.rotation.y + transform.rotation.y, 0));
            //bullet.AddForce(transform.right * 500);
        }

    }
}
