using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttackState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation($"Attack_Normal_{playerModel.currentNormalAttakIndex}");
    }

    public override void Update()
    {
        base.Update();
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        if (IsAnimationEnd())
            playerController.SwitchState(EPlayerState.NormalAttakEnd);
    }
}
