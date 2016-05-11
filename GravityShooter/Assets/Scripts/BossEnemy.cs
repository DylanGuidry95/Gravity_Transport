using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class BossEnemy : EnemyBase
{
    public GameObject LaserEnd; //Refrence to the end of the laser
    public float laserSpeed; //How fast the laser moves

    float reloadSpeed; //time it takes to reload primary weapon
    public int MultiShoot; //Amount of shoots fired at once

    [SerializeField]
    private BossGUI BossUI; //Refrence to the BossUI element

	// Use this for initialization
	protected override void Start ()
    {
        GUIManager.instance.Activate("UIBoss",true);
        if (BossUI == null)
            BossUI = FindObjectOfType<BossGUI>();
        hp = 15;
        BossUI.HPChange(hp);
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

    /// <summary>
    /// Spawn the bullet prefab and applies a force to it to make it
    /// move in the target direction. Also deducts the amount of ammo
    /// availiable after each shot and once out of ammo will start the LaserCycle
    /// Coroutine
    /// </summary>
    [ContextMenu("Laser")]
    protected override void Fire()
    {
        if (timer >= fireDelay && player != null && ammoAvailiable > 0)
        {
            float y_offset = -GetComponent<SpriteRenderer>().bounds.size.y / 2;
            List<GameObject> shoots = new List<GameObject>();
            for (int i = 0; i < MultiShoot; i++)
            {
                GameObject a = Instantiate(Resources.Load("Bullet")) as GameObject;
                a.transform.position += new Vector3(transform.position.x + transform.right.x * -transform.localScale.x, y_offset, 0);
                y_offset += GetComponent<SpriteRenderer>().bounds.size.y / MultiShoot;
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
    /// <summary>
    /// Controls the movement of the laser
    /// Calls the CheckLaser function to check if the Laser has come in collision with
    /// the player
    /// </summary>
    /// <returns></returns>
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

    /// <summary>
    /// Checks to see if the player has come in collison with the laser
    /// and if it does damages the player
    /// </summary>
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

    /// <summary>
    /// Invokes methods that need to happen when something
    /// collides with this object
    /// </summary>
    /// <param name="c"></param>
    protected override void OnTriggerEnter2D(Collider2D c)
    {
        if (c.GetComponent<Projectile>() && c.GetComponent<Projectile>().isEnemy == false)
        {
            Instantiate(Resources.Load("MultiExsplosion"), c.transform.position, c.transform.localRotation);
            //Destroys the bullet
            Destroy(c.gameObject);
            //Subtracts one hp from the enemy current hp
            hp--;
            BossUI.HPChange(hp);
            //Checks if the hp is equal to zero
            if (hp == 0)
            {
                Instantiate(Resources.Load("BossExplosion"), transform.position, transform.localRotation);
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
