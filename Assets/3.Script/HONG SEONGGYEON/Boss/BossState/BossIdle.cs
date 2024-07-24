using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BossStateBase
{
    private float CurrentTime=0;
    private float IdleTime;
    private int attacktype = Random.Range(0, 6);

    public override void Enter()
    {
        base.Enter();
        bossController.PlayAnimation("Idle");

    }

    public override void Update()
    {
        base.Update();
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= IdleTime) ;
        {
            switch(attacktype)
            {
                case 0:
                    bossController.SwitchState(BossState.Idle);
                    break;
            }
        }

    }

    public override void Exit()
    {
        base.Exit();
        CurrentTime = 0;
    }
}
