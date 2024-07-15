using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerNormalAttackState : PlayerStateBase
{
    private bool enterNextAttack;
    public override void Enter()
    {
        base.Enter();
        playerModel.SetWeapon(true);

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

        //다음 공격 On
        if(stateInfo.normalizedTime >= 0.5f && playerController.playerInputSystem.Player.Fire.triggered)
        {
            enterNextAttack = true;
        }
        //궁극기
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerModel.SetWeapon(false);
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        //회피
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            playerModel.SetWeapon(false);
            playerController.SwitchState(EPlayerState.EvadeBack);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }
        //스킬
        if (playerController.playerInputSystem.Player.Skill.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackSkill);
            return;
        }
        //애니메이션 종료
        if (IsAnimationEnd())
        {
            if (enterNextAttack)
            {
                playerModel.currentNormalAttakIndex++;
                if (playerModel.currentNormalAttakIndex > playerModel.skillConfig.normalAttackDamageMultiple.Length)
                {
                    playerModel.currentNormalAttakIndex = 1;
                }
                playerModel.SetWeapon(false);
                playerController.SwitchState(EPlayerState.NormalAttack);
                return;
            }
            else
            {
                playerModel.SetWeapon(false);
                playerController.SwitchState(EPlayerState.NormalAttakEnd);
                return;
            }
        }
    }
}
