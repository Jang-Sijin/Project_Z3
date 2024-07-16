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

        //궁극기
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }
        //평타
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            playerModel.currentNormalAttakIndex++;
            if (playerModel.currentNormalAttakIndex
                > playerModel.skillConfig.normalAttackDamageMultiple.Length)
            {
                playerModel.currentNormalAttakIndex = 1;
            }
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }
        //스킬
        if (playerController.playerInputSystem.Player.Skill.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackSkill);
            return;
        }
        //회피
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            playerController.SwitchState(EPlayerState.EvadeBack);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }
        //이동
        if (playerController.inputMoveVec2 != Vector2.zero && statePlayingTime > 0.2f)
        {
            playerController.SwitchState(EPlayerState.Walk);
            //playerController.SwitchState(EPlayerState.RunStart);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }
        //애니메이션 종료
        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.Idle);
            playerModel.currentNormalAttakIndex = 1;
            return;
        }

    }
}
