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

        gameObject.GetComponent<LineRenderer>().SetPosition(0, transform.position);
        gameObject.GetComponent<LineRenderer>().SetPosition(1, LaserEnd.transform.position);
        gameObject.GetComponent<LineRenderer>().SetWidth(LaserEnd.transform.localScale.y, LaserEnd.transform.localScale.y);
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
                a.transform.position += new Vector3(transform.position.x + transform.right.x * -transform.localScale.x, y_offset, 0);
                y_offset +=  a.transform.localScale.y;
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
        if(ammoAvailiable <= 0 && player != null && cycle == false)
        {
            StartCoroutine(LaserCycle());
        }
    }

    private bool Retract = false;
    private bool cycle = false;
    IEnumerator LaserCycle()
    {
        cycle = true;
        yield return new WaitForSeconds(2f);
        while (LaserEnd.transform.position.x - ScreenBorders.m_bottomLeft.x > 1f && Retract == false)
        {
            CheckLaser();
            LaserEnd.transform.position += (new Vector3(ScreenBorders.m_bottomLeft.x, player.transform.position.y, 0) - LaserEnd.transform.position) * (Time.deltaTime * laserSpeed);
            yield return null;
        }
        Retract = true;
        yield return new WaitForSeconds(2f);
        while (Vector3.Distance(LaserEnd.transform.position, transform.position) > .1f && Retract == true)
        {
            CheckLaser();
            LaserEnd.transform.position += (transform.position - LaserEnd.transform.position).normalized * (Time.deltaTime * laserSpeed * 4);
            yield return null;
        }
        if(Retract == true)
        {
            ammoAvailiable = ammoCapacity;
            Retract = false;
            cycle = false;
        }
        StopCoroutine(LaserCycle());
    }

    void CheckLaser()
    {
        if (player != null)
        {
            Vector2 displacement = LaserEnd.transform.position - transform.position ;
            Vector2 direction = displacement.normalized;
            RaycastHit2D[] tophit = Physics2D.CircleCastAll(
                transform.position, 
                LaserEnd.transform.localScale.y * .5f, 
                direction,
                displacement.magnitude);
            foreach(RaycastHit2D hit in tophit)
            {
                if (hit.collider != null && hit.collider.GetComponent<Player>() != null)
                {
                    LaserEnd.gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.black;
                }
                else if(hit.collider != null && hit.collider.GetComponent<Player>() == null)
                {
                    LaserEnd.gameObject.GetComponent<MeshRenderer>().materials[0].color = Color.white;
                }
            }
        }

    }
}
