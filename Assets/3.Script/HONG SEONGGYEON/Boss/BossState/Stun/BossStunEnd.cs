using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStunEnd : BossStateBase
{

    public override void Enter()
    {
        base.Enter();
        bossController.PlayAnimation("StunEnd");

    }

    public override void Update()
    {
        base.Update();

        if (bossController.IsAnimationFinished("StunEnd"))
        {
            bossController.SwitchState(BossState.Idle);
            bossController.bossModel.isGroggy = false;
        }

        //���� �߿� �ǰ� ��ȿ �߰��Ұ�
    }

    public override void Exit()
    {
        base.Exit();
        bossController.bossModel.CurrentGroggypoint = 0;
    }

}
