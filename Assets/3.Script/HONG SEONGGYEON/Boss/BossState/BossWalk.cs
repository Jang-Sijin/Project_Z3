using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : BossStateBase
{
    public override void Enter()
    {
        base.Enter();
        bossController.PlayAnimation("Walk");

    }

    public override void Update()
    {
        base.Update();
        bossController.bossModel.RotateTowards(bossController.bossModel.Target.position);

    }

    public override void Exit()
    {
        base.Exit();

    }
}
