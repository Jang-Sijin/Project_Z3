using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelleRunEndState : BelleStateBase
{
    public override void Enter()
    {
        base.Enter();
        switch (belleModel.foot)
        {
            case EModelFoot.Right:
                belleController.PlayAnimation("Run_End_R", 0.1f);
                break;
            case EModelFoot.Left:
                belleController.PlayAnimation("Run_End_L", 0.1f);
                break;
        }
    }

    public override void Update()
    {
        base.Update();

        //이동
        if (belleController.inputMoveVec2 != Vector2.zero)
        {
            if (!belleController.toggleWalk)
            {
                belleController.SwitchState(EBelleState.RunStart);
                return;
            }
            belleController.SwitchState(EBelleState.WalkStart);
            return;
        }
        //애니메이션 종료
        if (IsAnimationEnd())
        {
            belleController.SwitchState(EBelleState.Idle);
        }
    }
}
