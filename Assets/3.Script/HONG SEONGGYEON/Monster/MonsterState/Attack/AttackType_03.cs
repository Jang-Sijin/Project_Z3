using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType_03 : MonsterStateBase
{
    private float Origin;
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("AttackType_03");
        Origin = monsterController.mon_CO.AttackPoint;
        monsterController.mon_CO.AttackPoint *= monsterController.monsterModel.Attack2;

    }

    public override void Update()
    {
        base.Update();
        if (monsterController.monsterModel.isAttacked)
        {
            monsterController.SwitchState(MonsterState.Hit);
        }
        if (monsterController.IsAnimationFinished("AttackType_03"))
        {
            monsterController.SwitchState(MonsterState.Idle);
        }
    }

    public override void Exit()
    {
        base.Exit();
        monsterController.mon_CO.AttackPoint = Origin;
    }

}
