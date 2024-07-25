using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();

        monsterController.PlayAnimation("Hit");

    }

    public override void Update()
    {
        base.Update();
        if (monsterController.IsAnimationFinished("Hit"))
        {
          //if (monsterController.monsterModel.isAttacked)
          //{
          //    monsterController.SwitchState(MonsterState.Hit);
          //}
          //else
          //{
                // Hit ���¿��� ���� ���·� ��ȯ (��: Idle ����)
                monsterController.SwitchState(MonsterState.Idle);
         // }
        }
    }

    public override void Exit()
    {
        base.Exit();
        monsterController.monsterModel.isAttacked = false;

    }
}
