using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRun : BossStateBase
{
    public override void Enter()
    {
        base.Enter();
        bossController.PlayAnimation("Run");

    }

    public override void Update()
    {
        base.Update();

    }

    public override void Exit()
    {
        base.Exit();

    }
}
