using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelleWalkEndState : BelleStateBase
{
    private Camera mainCamera;

    public override void Enter()
    {
        base.Enter();
        switch (belleModel.foot)
        {
            case EModelFoot.Right:
                belleController.PlayAnimation("Walk_End_R", 0.1f);
                break;
            case EModelFoot.Left:
                belleController.PlayAnimation("Walk_End_L", 0.1f);
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        if(belleController.inputMoveVec2 != Vector2.zero)
        {
            if (!belleController.toggleWalk)
            {
                belleController.SwitchState(EBelleState.RunStart);
                return;
            }

            belleController.SwitchState(EBelleState.WalkStart);
        }

        //애니메이션 종료
        if (IsAnimationEnd())
        {
            belleController.SwitchState(EBelleState.Idle);
        }
    }
}
