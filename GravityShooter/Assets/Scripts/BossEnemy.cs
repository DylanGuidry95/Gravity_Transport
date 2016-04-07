using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BossEnemy : EnemyBase
{
    public GameObject LaserEnd;
    public float laserSpeed;

    float reloadSpeed;
    public int MultiShoot;

	// Use this for initialization
	protected override void Start ()
    {
        base.Start();   
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        Fire();
    }

    [ContextMenu("Laser")]
    protected override void Fire()
    {
        if (timer >= fireDelay && player != null && ammoAvailiable > 0)
        {
            float y_offset = -transform.localScale.y ;
            List<GameObject> shoots = new List<GameObject>();
            for (int i = 0; i < MultiShoot; i++)
            {
                GameObject a = Instantiate(Resources.Load("Bullet")) as GameObject;
                a.transform.position = transform.position + transform.right * -transform.localScale.x;
                a.transform.position += new Vector3(0, y_offset, 0);
                y_offset += (transform.localScale.y / a.transform.localScale.y);
                shoots.Add(a);
            }
            foreach (GameObject a in shoots)
            {
                Vector2 Look_at_player = (player.transform.position - transform.position).normalized;
                a.GetComponent<Rigidbody2D>().velocity = Look_at_player.normalized * bulletSpeed;
                timer = 0;
            }
            ammoAvailiable--;
        }
        if(ammoAvailiable <= 0 && player != null && !Retract)
        {
            StartCoroutine(LaserCycle());
        }
    }

    public bool Retract = false;

    IEnumerator LaserCycle()
    {
        if(Retract == false)
        {
            while (LaserEnd.transform.position.x - ScreenBorders.m_bottomLeft.x > 1f)
            {
                LaserEnd.transform.position += (new Vector3(ScreenBorders.m_bottomLeft.x, player.transform.position.y, ScreenBorders.m_bottomLeft.z) - LaserEnd.transform.position) * (Time.deltaTime * laserSpeed);
                yield return null;
            }
            Retract = true;
        }
        else
        {
            while (Vector3.Distance(LaserEnd.transform.position, transform.position) > .1f)
            {
                Debug.Log("Retract");
                LaserEnd.transform.position += (transform.position - LaserEnd.transform.position) * (Time.deltaTime * laserSpeed);
                yield return null;
            }
            Retract = false;
            ammoAvailiable = ammoCapacity;
            StopCoroutine(LaserCycle());
        }

    }
}
