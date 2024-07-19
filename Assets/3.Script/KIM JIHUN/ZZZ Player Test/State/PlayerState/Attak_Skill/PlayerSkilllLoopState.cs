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

        //�Է��� ���߸� Ex, End�� �Ѿ
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

        //���� 4�� �̸��̰� �ִϸ��̼� �����ٸ� �ٽ�
        if (statePlayingTime < 4 && IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.AttackSkillLoop);
            return;
        }

        //���� 4�� �̻��Ͻ� ex�� �Ѿ
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
