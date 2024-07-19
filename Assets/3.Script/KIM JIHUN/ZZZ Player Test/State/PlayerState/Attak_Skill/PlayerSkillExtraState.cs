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
        //애니메이션 종료
        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.AttackSkillEnd);
        }
    }

}
