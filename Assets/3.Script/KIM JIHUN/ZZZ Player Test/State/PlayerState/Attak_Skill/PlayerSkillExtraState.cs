using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillExtraState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("Attack_Skill_Ex");
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
