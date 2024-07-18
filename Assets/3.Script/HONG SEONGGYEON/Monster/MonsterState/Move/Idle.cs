using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Idle");

    }
    public override void Update()
    {
        base.Update();

        if (monsterController.Distance >= 5.0f)
        {
            monsterController.SwitchState(MonsterState.Run);
        }

        if (monsterController.Distance < 5.0f&& monsterController.Distance > 2.0f)
        {
            monsterController.SwitchState(MonsterState.Walk);
        }
    }
}
