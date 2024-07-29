using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : EnemyStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Idle");
    }

    public override void Update()
    {
        base.Update();
        //플레이어 인식시 상태 변하기
        if (monsterModel.IsPlayerInAttackRange())
        {
            monsterController.SwitchState(EMonsterState.Attack);
        }
        if (monsterModel.IsPlayerInSight())
        {
            monsterController.SwitchState(EMonsterState.Run);
        }
    }
}
