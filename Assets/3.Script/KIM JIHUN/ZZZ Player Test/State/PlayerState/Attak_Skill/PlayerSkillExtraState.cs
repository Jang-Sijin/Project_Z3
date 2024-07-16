using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillExtraState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();
        try
        {
            playerController.PlayAnimation("Attack_Skill_Ex");
        }
        catch
        {
            playerController.SwitchState(EPlayerState.AttackSkillEnd);
        }
    }

    public override void Update()
    {
        base.Update();
        //�ִϸ��̼� ����
        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.AttackSkillEnd);
        }
    }

}
