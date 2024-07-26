using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDeadState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("Dead");
    }

    public override void Update()
    {
        base.Update();
        if(IsAnimationEnd())
        {
            // 다음 캐릭터로 전환
            playerController.SwitchNextModel(true);
        }
    }
}
