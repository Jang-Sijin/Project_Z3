using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterHitState : EnemyStateBase
{

    public override void Enter()
    {
        base.Enter();

        // 몬스는 플레이어의 방향을 보도록 설정한다.
        monsterModel.transform.LookAt(PlayerController.INSTANCE.playerModel.transform);

        // 히트 넉백 애니메이션 출력        
        monsterController.PlayAnimation("Hit");
    }

    public override void Update()
    {
        base.Update();

        // 히트 애니메이션이 종료되었을 경우
        if (IsAnimationEnd())
        {
            if (monsterModel.IsPlayerInSight()) // 시야 범위 내 존재할 경우
            {
                monsterController.SwitchState(EMonsterState.Attack);
                return;
            }

            monsterController.SwitchState(EMonsterState.Run);
            return;
        }
    }
}
