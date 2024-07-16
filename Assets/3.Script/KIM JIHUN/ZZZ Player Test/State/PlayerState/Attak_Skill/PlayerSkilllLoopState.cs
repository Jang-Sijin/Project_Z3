using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkilllLoopState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        try
        {
            playerController.PlayAnimation("Attack_Skill_Loop");
        }
        catch
        {
            playerController.SwitchState(EPlayerState.AttackSkillEx);
        }
    }

    public override void Update()
    {
        base.Update();

        //�Է��� ���߸� Ex�� �Ѿ
        if(!playerController.playerInputSystem.Player.Skill.IsPressed())
        {
            playerController.SwitchState(EPlayerState.AttackSkillEx);
        }

        //���� 4�� �̸��̰� �ִϸ��̼� �����ٸ� �ٽ�
        if(statePlayingTime < 4 && IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.AttackSkillLoop);
        }

        //���� 4�� �̻��Ͻ� ex�� �Ѿ
        if(statePlayingTime >= 4)
        {
            playerController.SwitchState(EPlayerState.AttackSkillEx);
        }
    }
}
