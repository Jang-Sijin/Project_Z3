using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Idle");
        monsterController.monsterModel.nmagent.isStopped = true; // Idle »óÅÂ¿¡¼­´Â ÀÌµ¿ ¸ØÃã

    }
    public override void Update()
    {
        base.Update();
        monsterController.monsterModel.animator.SetFloat("MoveSpeed", 0);
        if (monsterController.Distance >= 5.0f)
        {
            monsterController.SwitchState(MonsterState.Run);
        }

        else if (monsterController.Distance < 5.0f && monsterController.Distance > 2.0f)
        {
            monsterController.SwitchState(MonsterState.Walk);
        }

    }
}
