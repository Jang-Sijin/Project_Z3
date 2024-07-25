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

        playerModel.LookEnemy();

        playerController.PlayAnimation($"Attack_Normal_{playerModel.currentNormalAttakIndex}");
    }

    public override void Update()
    {
        base.Update();

        //���� ���� On
        if (GetNormalizedTime() >= 0.5f && playerController.playerInputSystem.Player.Fire.triggered)
        {
            enterNextAttack = true;
        }
        //�ñر�
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        //ȸ��
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            playerController.SwitchState(EPlayerState.EvadeBack);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }
        //��ų
        if (playerController.playerInputSystem.Player.Skill.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackSkill);
            return;
        }
        //�ִϸ��̼� ����
        if (IsAnimationEnd())
        {
            if (enterNextAttack)
            {
                playerModel.currentNormalAttakIndex++;
                if (playerModel.currentNormalAttakIndex > playerModel.characterInfo.normalAttackDamageMultiple.Length)
                {
                    playerModel.currentNormalAttakIndex = 1;
                }
                playerController.SwitchState(EPlayerState.NormalAttack);
                return;
            }
            else
            {
                playerController.SwitchState(EPlayerState.NormalAttackEnd);
                return;
            }
        }
    }
}
