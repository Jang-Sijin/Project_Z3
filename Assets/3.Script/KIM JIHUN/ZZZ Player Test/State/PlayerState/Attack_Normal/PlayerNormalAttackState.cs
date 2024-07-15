using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttackState : PlayerStateBase
{
    private bool enterNextAttack;
    public override void Enter()
    {
        base.Enter();

        enterNextAttack = false;

        if(playerController.closestEnemy != null)
        {
            Quaternion targetQua = Quaternion.LookRotation(playerController.directionToEnemy);
            playerModel.transform.rotation = targetQua;
        }

        playerController.PlayAnimation($"Attack_Normal_{playerModel.currentNormalAttakIndex}");
    }

    public override void Update()
    {
        base.Update();

        if(stateInfo.normalizedTime >= 0.5f && playerController.playerInputSystem.Player.Fire.triggered)
        {
            enterNextAttack = true;
        }

        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        if (IsAnimationEnd())
        {
            if (enterNextAttack)
            {
                playerModel.currentNormalAttakIndex++;
                if (playerModel.currentNormalAttakIndex > playerModel.skillConfig.normalAttackDamageMultiple.Length)
                {
                    playerModel.currentNormalAttakIndex = 1;
                }
                playerController.SwitchState(EPlayerState.NormalAttack);
                return;
            }
            else
            {
                playerController.SwitchState(EPlayerState.NormalAttakEnd);
                return;
            }
        }

        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            playerController.SwitchState(EPlayerState.EvadeBack);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }
    }
}
