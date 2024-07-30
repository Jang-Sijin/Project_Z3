using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterRunState : EnemyStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Run");
    }

    public override void Update()
    {
        base.Update();

        Debug.Log(monsterModel.IsPlayerInAttackRange());
        Debug.Log(!monsterModel.IsPlayerInSight());
        if (monsterModel.IsPlayerInAttackRange())
        {
            Debug.Log("Attack");
            //Attack
            monsterController.SwitchState(EMonsterState.Attack);
            return;
        }

        if (!monsterModel.IsPlayerInSight())
        {
            //사거리에서 벗어났으므로 IDle
            monsterController.SwitchState(EMonsterState.Idle);
            return;
        }

        // 항상 플레이어를 바라보도록
        monsterModel.transform.LookAt(PlayerController.INSTANCE.playerModel.transform);
    }
}
