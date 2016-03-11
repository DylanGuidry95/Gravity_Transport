using UnityEngine;
using System.Collections;
using System;

public class SmallEnemy : MonoBehaviour, Enemymanager
{
    public GameObject player;

    void Start ()
    {
        player = GameObject.Find("Player");
        //movement();
	}

    public bool aim()
    {
        throw new NotImplementedException();
    }

    public void movement()
    {
        Vector3 point = new Vector3(player.transform.position.x, transform.position.y, 0);
        float hyp = (transform.position - player.transform.position).magnitude;
        float adj = (transform.position - point).magnitude; 
        float angle = Mathf.Cos(adj / hyp) * 180 / Mathf.PI;
        
        if(player.transform.position.y > transform.position.y)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90 - angle));
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90 + angle));
        }

        if (Mathf.Abs(player.transform.position.y - transform.position.y) < 1)
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 90));
        }
        
    }

    // Update is called once per frame
    void FixedUpdate ()
    {
        movement();
	}
}
