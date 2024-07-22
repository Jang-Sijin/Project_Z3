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
        }

        //공격 중에 피격 무효 추가할것
    }

    public override void Exit()
    {
        base.Exit();
        monsterController.monsterModel.isGroggy = false;
        monsterController.monsterModel.Groggypoint = 0;
    }
}