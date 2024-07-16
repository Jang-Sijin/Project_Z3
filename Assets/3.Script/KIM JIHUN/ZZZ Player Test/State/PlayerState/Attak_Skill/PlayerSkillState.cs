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
        //�ִϸ��̼� ����
        if (IsAnimationEnd())
        {
            if (playerController.playerInputSystem.Player.Skill.IsPressed())
            {
                playerController.SwitchState(EPlayerState.AttackSkillLoop);
                return;
            }
            
            playerController.SwitchState(EPlayerState.AttackSkillEx);
        }
    }
}
