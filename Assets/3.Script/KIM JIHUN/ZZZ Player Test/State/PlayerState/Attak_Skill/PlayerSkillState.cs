using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkillState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("Attack_Skill");
    }

    public override void Update()
    {
        base.Update();
        //�ִϸ��̼� ����
        if (IsAnimationEnd())
        {

            playerController.SwitchState(EPlayerState.AttackSkillEx);
        }
    }
}
