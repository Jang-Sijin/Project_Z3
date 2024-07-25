using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStunStart : BossStateBase
{

    public override void Enter()
    {
        base.Enter();
        bossController.PlayAnimation("StunStart");

    }

    public override void Update()
    {
        base.Update();

        if (bossController.IsAnimationFinished("StunStart"))
        {
            bossController.SwitchState(BossState.StunLoop);
        }

        //���� �߿� �ǰ� ��ȿ �߰��Ұ�
    }



}
