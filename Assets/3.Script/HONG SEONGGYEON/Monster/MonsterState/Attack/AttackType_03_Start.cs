using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType_03_Start : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("AttackType_03_Start");

    }

    public override void Update()
    {
        base.Update();


        if(monsterController.monsterModel.Distance < 4.0f)
        {
            monsterController.SwitchState(MonsterState.AttackType_03);
        }

        else if (monsterController.IsAnimationFinished("AttackType_03_Start"))
        {
            monsterController.SwitchState(MonsterState.AttackType_03);
        }

        //공격 중에 피격 무효 추가할것
    }

}
