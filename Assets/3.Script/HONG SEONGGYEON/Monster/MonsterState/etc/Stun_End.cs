using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun_End : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Stun_End");

    }

    public override void Update()
    {
        base.Update();

        if (monsterController.IsAnimationFinished("Stun_End"))
        {
            monsterController.SwitchState(MonsterState.Idle);
            monsterController.monsterModel.isGroggy = false;
        }

        //���� �߿� �ǰ� ��ȿ �߰��Ұ�
    }

    public override void Exit()
    {
        base.Exit();
        monsterController.monsterModel.CurrentGroggypoint = 0;
    }
}