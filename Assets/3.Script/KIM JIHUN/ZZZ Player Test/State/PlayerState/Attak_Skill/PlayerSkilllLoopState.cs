using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkilllLoopState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        playerController.PlayAnimation("Attack_Skill_Loop");

    }

    public override void Update()
    {
        base.Update();

        //입력이 멈추면 Ex, End로 넘어감
        if (!playerController.playerInputSystem.Player.Skill.IsPressed())
        {
            if (playerModel.hasSkillExtra)
            {
                playerController.SwitchState(EPlayerState.AttackSkillEx);
                return;
            }
            playerController.SwitchState(EPlayerState.AttackSkillEnd);
            return;
        }

        //루프 4초 미만이고 애니메이션 끝났다면 다시
        if (statePlayingTime < 4 && IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.AttackSkillLoop);
            return;
        }

        //루프 4초 이상일시 ex로 넘어감
        if (statePlayingTime >= 4)
        {
            if (playerModel.hasSkillExtra)
            {
                playerController.SwitchState(EPlayerState.AttackSkillEx);
                return;
            }
            playerController.SwitchState(EPlayerState.AttackSkillEnd);
            return;
        }
    }
}
