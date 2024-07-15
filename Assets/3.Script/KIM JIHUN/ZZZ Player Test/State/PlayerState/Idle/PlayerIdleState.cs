using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        switch (playerModel.currentState)
        {
            case EPlayerState.Idle:
                playerController.PlayAnimation("Idle");
                break;
            case EPlayerState.IdleAFK:
                playerController.PlayAnimation("IdleAFK");
                break;
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
            //Debug.Log($"Combo : {playerModel.currentNormalAttakIndex}");
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            //Debug.Log("Idle -> Evade Back");
            playerController.SwitchState(EPlayerState.EvadeBack);
            return;
        }
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(EPlayerState.Walk);
            return;
            //playerController.SwitchState(EPlayerState.RunStart);
        }

        switch (playerModel.currentState)
        {
            case EPlayerState.Idle:
                if (playerModel.currentState == EPlayerState.Idle && statePlayingTime > 4)
                {
                    playerController.SwitchState(EPlayerState.IdleAFK);
                    Debug.Log("IdleAFK");
                }
                break;
            case EPlayerState.IdleAFK:
                if (IsAnimationEnd())
                    playerController.SwitchState(EPlayerState.Idle);
                break;
        }
    }
}
