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

        //�ñر�
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }
        //��Ÿ
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            playerModel.currentNormalAttakIndex++;
            if (playerModel.currentNormalAttakIndex
                > playerModel.characterInfo.normalAttackDamageMultiple.Length)
            {
                playerModel.currentNormalAttakIndex = 1;
            }
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }
        //��ų
        if (playerController.playerInputSystem.Player.Skill.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackSkill);
            return;
        }
        //ȸ��
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            playerController.SwitchState(EPlayerState.EvadeBack);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }
        //�̵�
        if (playerController.inputMoveVec2 != Vector2.zero && statePlayingTime > 0.2f)
        {
            playerController.SwitchState(EPlayerState.Walk);
            //playerController.SwitchState(EPlayerState.RunStart);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }
        //�ִϸ��̼� ����
        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.Idle);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }

    }
}
