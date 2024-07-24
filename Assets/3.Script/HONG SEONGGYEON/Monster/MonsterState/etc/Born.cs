using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Born : MonsterStateBase
{ public override void Enter()
{
    base.Enter();
    monsterController.PlayAnimation("Born");

}

public override void Update()
{
    base.Update();
        if (monsterController.IsAnimationFinished("Born"))
        {
            monsterController.SwitchState(MonsterState.Idle);
        }

}

public override void Exit()
{
    base.Exit();

}
}
