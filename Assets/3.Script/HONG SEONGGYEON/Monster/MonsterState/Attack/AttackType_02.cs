using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType_02 : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("AttackType_02");
        monsterController.mon_CO.AttackingDisable();

    }

    public override void Update()
    {
        base.Update();
        if (monsterController.IsAnimationFinished("AttackType_02"))

            monsterController.SwitchState(MonsterState.Idle);

    }

    public override void Exit()
    {
        base.Exit();
        monsterController.mon_CO.AttackingEnable();

    }
}
