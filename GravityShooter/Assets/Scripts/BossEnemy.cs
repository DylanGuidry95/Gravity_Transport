using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BossEnemy : EnemyBase
{
    public GameObject LaserEnd;
    public float laserSpeed;

    float reloadSpeed;
    public int MultiShoot;

    [SerializeField]
    private BossGUI BossUI;

	// Use this for initialization
	protected override void Start ()
    {
        GUIManager.instance.Activate("UIBoss",true);
        if (BossUI == null)
            BossUI = FindObjectOfType<BossGUI>();
        hp = 15;
        base.Start();   
	}

    protected override void GenerateFSM()
    {
        base.GenerateFSM();
    }

    void CheckState()
    {
        switch (_fsm.state)
        {
            case ENEMYSTATES.spawn:
                EnemySpawn();
                break;
            case ENEMYSTATES.idle:
                Fire();
                _fsm.Transition(_fsm.state, ENEMYSTATES.fly);
                break;
            case ENEMYSTATES.fly:
                Fire();
                break;
            case ENEMYSTATES.special:
                Fire();
                break;
            case ENEMYSTATES.dead:
                Destroy(this.gameObject);
                break;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        if(player == null)
            player = FindObjectOfType<Player>(); //Locates the player
        timer += Time.deltaTime;
        CheckState();

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
                    hit.collider.GetComponent<Player>().PlayerDamage();
                }
            }
        }

    }

    protected override void OnTriggerEnter2D(Collider2D c)
    {
        if (c.GetComponent<Projectile>() && c.GetComponent<Projectile>().isEnemy == false)
        {
            //Destroys the bullet
            Destroy(c.gameObject);
            //Subtracts one hp from the enemy current hp
            hp--;
            BossUI.HPChange(1);
            //Checks if the hp is equal to zero
            if (hp == 0)
            {
                BossUI.ToggleBossGUI(false);
                //Calls score functions to increase current score
                //Destorys the enemy
                ScoreManager.IncreasScoreBy(ScoreValue);
                Destroy(this.gameObject);
                //Plays the explosion audio
                FindObjectOfType<AudioManager>().PlayExplodeAudio();
            }
        }
    }
}
