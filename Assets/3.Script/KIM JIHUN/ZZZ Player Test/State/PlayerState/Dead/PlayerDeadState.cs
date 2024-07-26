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
            // ���� ĳ���ͷ� ��ȯ
            playerController.SwitchNextModel(true);
        }
    }
}
