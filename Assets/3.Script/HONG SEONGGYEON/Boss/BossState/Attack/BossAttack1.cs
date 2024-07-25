using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1 : BossStateBase
{

    public override void Enter()
    {
        base.Enter();
        bossController.PlayAnimation("Attack1");
        bossController.bossModel.RotateTowards(bossController.bossModel.Target.transform.position);

    }

    public override void Update()
    {
        if(bossController.bossModel.isGroggy) bossController.SwitchState(BossState.StunStart);
        if (bossController.IsAnimationFinished("Attack1"))
        {
            bossController.SwitchState(BossState.Idle);
        }


    }

    public override void Exit()
    {
        base.Exit();

    }
}
