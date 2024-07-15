using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        switch(playerModel.currentState)
        {
            case EPlayerState.EvadeFrontEnd:
                playerController.PlayAnimation("Evade_Front_End");
                break;
            case EPlayerState.EvadeBackEnd:
                playerController.PlayAnimation("Evade_Back_End");
                break;
        }
    }

    public override void Update()
    {
        base.Update();
        if(playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }

        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(EPlayerState.Walk);
            return;
            //playerController.SwitchState(EPlayerState.RunStart);
        }

        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            if (playerController.evadeTimer >= playerController.evadeCoolTime)
            {
                playerController.SwitchState(EPlayerState.EvadeBack);
                return;
            }
        }

        if (stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
        {
            playerController.SwitchState(EPlayerState.Idle);
            return;
        }
    }
}
