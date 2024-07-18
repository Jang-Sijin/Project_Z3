using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelleIdleState : BelleStateBase
{
    public override void Enter()
    {
        base.Enter();

        belleController.PlayAnimation("Idle");
    }

    public override void Update()
    {
        base.Update();

        //¿Ãµø
        if (belleController.inputMoveVec2 != Vector2.zero)
        {
            belleController.SwitchState(EBelleState.Run);
            return;
        }
    }
}
