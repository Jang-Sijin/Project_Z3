using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterAttackState : EnemyStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterModel.transform.LookAt(PlayerController.INSTANCE.playerModel.transform);
        // 랜덤하게 둘 중 하나의 애니메이션 출력
        int randomIndex = Random.Range(0, 2);
        monsterController.PlayAnimation("Attack_" + randomIndex);
    }

    public override void Update()
    {
        base.Update();
        //플레이어 인식시 상태 변하기

        if (IsAnimationEnd())
        {
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
            monsterController.SwitchState(EMonsterState.Idle);
            return;
        }
    }
}
