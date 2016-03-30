using UnityEngine;
using System.Collections;
using System;

public class EnemyBase : MonoBehaviour
{
    protected enum ENEMYSTATES
    {
        init,
        spawn,
        idle,
        fly,
        special,
        dead,
        count
    }

    [SerializeField]
    protected float movementSpeed;
    [SerializeField]
    protected float ammoCapacity;
    protected float ammoAvailiable;
    [SerializeField]
    protected float fireDelay;
    [SerializeField]
    protected float bulletSpeed;
    [SerializeField]
    protected int hp;

    protected Player player;
    protected GameObject bullet;
    [SerializeField]
    protected Vector3 SpawnPosition;

    [SerializeField]
    protected float timer;


    protected FSM<ENEMYSTATES> _fsm;

    virtual protected void Awake()
    {
        _fsm = new FSM<ENEMYSTATES>();
        GenerateFSM();
    }

    virtual protected void Start()
    {
        player = FindObjectOfType<Player>();
        _fsm.Transition(_fsm.state, ENEMYSTATES.spawn);
        ammoAvailiable = ammoCapacity;
    }

    virtual protected void GenerateFSM()
    {
        foreach (int i in Enum.GetValues(typeof(ENEMYSTATES)))
        {
            if ((ENEMYSTATES)i != ENEMYSTATES.count)
            {
                _fsm.AddState((ENEMYSTATES)i);
            }
        }

        _fsm.AddTransition(ENEMYSTATES.init, ENEMYSTATES.spawn,false);

        _fsm.AddTransition(ENEMYSTATES.spawn, ENEMYSTATES.idle, false);

        _fsm.AddTransition(ENEMYSTATES.idle, ENEMYSTATES.fly, true);
        _fsm.AddTransition(ENEMYSTATES.idle, ENEMYSTATES.dead, false);

        _fsm.AddTransition(ENEMYSTATES.fly, ENEMYSTATES.dead, false);
    }

    [ContextMenu("Fire")]
    virtual protected void Fire()
    {
        if (timer >= fireDelay && player != null && ammoAvailiable > 0)
        {
            bullet = Instantiate(Resources.Load("Bullet"), transform.position + transform.right * -transform.localScale.x, transform.localRotation) as GameObject;

            Vector2 Look_at_player = (player.transform.position - transform.position).normalized;
            bullet.GetComponent<Rigidbody2D>().velocity = Look_at_player.normalized * bulletSpeed;
            ammoAvailiable--;
            timer = 0;
        }
    }

    virtual protected void OnTriggerEnter2D(Collider2D c)
    {
        if(c.GetComponent<Projectile>() && c.GetComponent<Projectile>().isEnemy == false)
        {
            Destroy(c.gameObject);
            hp--;
            if(hp == 0)
            {
                Destroy(this.gameObject);
                FindObjectOfType<AudioManager>().PlayExplodeAudio();
            }
        }
    }

    virtual protected void EnemySpawn()
    {
        if (Vector3.Distance(transform.position, SpawnPosition) > .1f)
        {

            transform.position += (SpawnPosition - transform.position) * (Time.deltaTime * 1);
        }
        else
        {
            _fsm.Transition(_fsm.state, ENEMYSTATES.idle);
        }
    }
}
