using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttackEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        playerController.PlayAnimation($"Attack_Normal_{playerModel.currentNormalAttakIndex}_End");
    }

    public override void Update()
    {
        base.Update();
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            //Debug.Log($"Combo : {playerModel.currentNormalAttakIndex}");
            //Debug.Log($"Before : {playerModel.skillConfig.currentNoramalAttakIndex}");
            playerModel.currentNormalAttakIndex++;
            //Debug.Log($"After : {playerModel.skillConfig.currentNoramalAttakIndex}");
            if (playerModel.currentNormalAttakIndex
                > playerModel.skillConfig.normalAttackDamageMultiple.Length)
            {
                playerModel.currentNormalAttakIndex = 1;
            }
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            playerController.SwitchState(EPlayerState.EvadeBack);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }
        if (playerController.inputMoveVec2 != Vector2.zero && statePlayingTime > 0.2f)
        {
            playerController.SwitchState(EPlayerState.Walk);
            //playerController.SwitchState(EPlayerState.RunStart);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }

        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.Idle);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }

    }
}
