using UnityEngine;
using System.Collections;

public class LgEnemy : EnemyBase
{
    protected override void Start()
    {
        ammoCapacity = 500;
        hp = 2;
        base.Start();
    }

    protected override void GenerateFSM()
    {
        base.GenerateFSM();
        _fsm.AddTransition(ENEMYSTATES.idle, ENEMYSTATES.special, true);
        _fsm.AddTransition(ENEMYSTATES.fly, ENEMYSTATES.special, true);
        _fsm.AddTransition(ENEMYSTATES.special, ENEMYSTATES.dead, false);
    }

    void CheckState()
    {
        switch (_fsm.state)
        {
            case ENEMYSTATES.spawn:
                EnemySpawn();
                break;
            case ENEMYSTATES.idle:
                //Fire();
                //if (hp == 1)
                //    _fsm.Transition(_fsm.state, ENEMYSTATES.special);
                //_fsm.Transition(_fsm.state, ENEMYSTATES.fly);
                break;
            case ENEMYSTATES.fly:
                Movement();
                //Fire();
                //if (hp == 1)
                //    _fsm.Transition(_fsm.state, ENEMYSTATES.special);
                break;
            case ENEMYSTATES.special:
                break;
            case ENEMYSTATES.dead:
                Destroy(this.gameObject);
                break;
        }
    }
    
    void Movement()
    {

    }

    void Special()
    {

    }

    protected override void Fire()
    {
        base.Fire();
    }
}
