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
                // Hit 상태에서 다음 상태로 전환 (예: Idle 상태)
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
