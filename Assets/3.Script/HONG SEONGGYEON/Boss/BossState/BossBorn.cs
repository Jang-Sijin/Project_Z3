using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBorn : BossStateBase

{
    public override void Enter()
    {
        base.Enter();
        bossController.PlayAnimation("Born");

    }

    public override void Update()
    {
        base.Update();
        if (bossController.IsAnimationFinished("Born"))
        {
            bossController.SwitchState(BossState.Idle);
        }

    }

    public override void Exit()
    {
        base.Exit();

    }
}

