using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossIdle : BossStateBase
{
    private float CurrentTime = 0;
    private float IdleTime=0.5f;

    public override void Enter()
    {
        base.Enter();
        CurrentTime = 0;

        bossController.PlayAnimation("Idle");

    }

    public override void Update()
    {
        base.Update();
        if (bossController.bossModel.isGroggy) bossController.SwitchState(BossState.StunStart);
        CurrentTime += Time.deltaTime;
        if (CurrentTime >= IdleTime)
        {
            bossController.SwitchState(BossState.Walk);
        }
    }

    public override void Exit()
    {
        base.Exit();
        CurrentTime = 0;
      
    }
}
