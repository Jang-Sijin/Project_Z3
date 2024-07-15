using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        try
        {
            playerController.PlayAnimation("Run_End");
        }
        catch
        {
            switch (playerModel.foot)
            {
                case EModelFoot.Right:
                    playerController.PlayAnimation("Run_End_R", 0.1f);
                    break;
                case EModelFoot.Left:
                    playerController.PlayAnimation("Run_End_L", 0.1f);
                    break;
            }
        }
    }

    public override void Update()
    {
        base.Update();
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }

        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            //Debug.Log("Run End -> Evade Back");
            playerController.SwitchState(EPlayerState.EvadeBack);
            return;
        }

        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(EPlayerState.Walk);
            return;
            //playerController.SwitchState(EPlayerState.RunStart);
        }

        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.Idle);
        }
    }
}
