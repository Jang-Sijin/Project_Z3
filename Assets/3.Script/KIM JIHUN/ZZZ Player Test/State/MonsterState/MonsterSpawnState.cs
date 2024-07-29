using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterSpawnState : EnemyStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Spawn");
    }

    public override void Update()
    {
        base.Update();
        if (monsterModel.IsPlayerInAttackRange())
        {
            monsterController.SwitchState(EMonsterState.Attack);
            return;
        }
        if (monsterModel.IsPlayerInSight())
        {
            monsterController.SwitchState(EMonsterState.Run);
            return;
        }
        if (IsAnimationEnd())
        {
            monsterController.SwitchState(EMonsterState.Idle);
            return;
        }
    }
}
