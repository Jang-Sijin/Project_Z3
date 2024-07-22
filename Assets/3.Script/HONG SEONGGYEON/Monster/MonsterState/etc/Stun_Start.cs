using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun_Start : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Stun_Start");

    }

    public override void Update()
    {
        base.Update();

         if (monsterController.IsAnimationFinished("Stun_Start"))
        {
            monsterController.SwitchState(MonsterState.Stun);
        }

        //���� �߿� �ǰ� ��ȿ �߰��Ұ�
    }

}
