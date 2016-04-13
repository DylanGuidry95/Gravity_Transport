using UnityEngine;
using System.Collections;

public class SmEnemy : EnemyBase
{
	// Use this for initialization
	protected override void Start()
    {
        base.Start();
        ScoreValue = 5;
	}

    protected override void GenerateFSM()
    {
        base.GenerateFSM();
        _fsm.AddTransition(ENEMYSTATES.idle, ENEMYSTATES.special, false);
        _fsm.AddTransition(ENEMYSTATES.fly, ENEMYSTATES.special, false);
        _fsm.AddTransition(ENEMYSTATES.special, ENEMYSTATES.dead, false);
    }

    // Update is called once per frame
    void Update ()
    {
        timer += Time.deltaTime;
        if(player == null)
            player = FindObjectOfType<Player>();
        CheckState();
  
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
                break;
            case ENEMYSTATES.fly:
                break;
            case ENEMYSTATES.special:
                Special();
                break;
            case ENEMYSTATES.dead:
                Destroy(this.gameObject);
                break;
        }
    }

    protected override void Fire()
    {
        base.Fire();
        if(ammoAvailiable <= 0)
        {
            transform.right = (transform.position - player.transform.position).normalized;
            _fsm.Transition(_fsm.state, ENEMYSTATES.special);
        }
    }

    void Special()
    {
        float enemyAngel = transform.eulerAngles.z * Mathf.Deg2Rad;
        transform.position -= new Vector3(Mathf.Cos(enemyAngel), Mathf.Sin(enemyAngel), 0) * (Time.deltaTime * movementSpeed);
    }
}
