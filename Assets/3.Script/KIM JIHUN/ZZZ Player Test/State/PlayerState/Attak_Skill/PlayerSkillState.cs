using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        playerModel.LookEnemy();
        playerController.PlayAnimation("Attack_Skill");
    }

    public override void Update()
    {
        base.Update();
        //애니메이션 종료
        if (IsAnimationEnd())
        {
            if (playerController.playerInputSystem.Player.Skill.IsPressed())
            {
                if (playerModel.hasSkillLoop)
                {
                    playerController.SwitchState(EPlayerState.AttackSkillLoop);
                    return;
                }
            }
            if (playerModel.hasSkillExtra)
            { 
                playerController.SwitchState(EPlayerState.AttackSkillEx);
                return;
            }
            playerController.SwitchState(EPlayerState.AttackSkillEnd);
        }
    }
}
