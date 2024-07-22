using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType_01 : MonsterStateBase
{

    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("AttackType_01");
        monsterController.mon_CO.AttackingDisable();

    }

    public override void Update()
    {
        base.Update();

        if (monsterController.IsAnimationFinished("AttackType_01"))

            monsterController.SwitchState(MonsterState.Idle);

    }

    public override void Exit()
    {
        base.Exit();
        monsterController.mon_CO.AttackingEnable();
    }

}
