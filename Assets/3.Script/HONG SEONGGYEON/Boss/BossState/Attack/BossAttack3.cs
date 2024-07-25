using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack3 : BossStateBase
{
    private float OriginAttack;
    public override void Enter()
    {
        base.Enter();
        bossController.PlayAnimation("Attack3");
        bossController.bossModel.RotateTowards(bossController.bossModel.Target.transform.position);
        OriginAttack = bossController.mon_CO.AttackPoint;
        bossController.mon_CO.AttackPoint *= bossController.bossModel.Attack3;

    }

    public override void Update()
    {
        if (bossController.bossModel.isGroggy) bossController.SwitchState(BossState.StunStart);
        if (bossController.IsAnimationFinished("Attack3"))
        {
            bossController.SwitchState(BossState.Idle);
        }


    }

    public override void Exit()
    {
        base.Exit();
        bossController.mon_CO.AttackPoint = OriginAttack;

    }
}


