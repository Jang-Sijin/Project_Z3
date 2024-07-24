using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack1 : BossStateBase
{

    public override void Enter()
    {
        base.Enter();
        bossController.PlayAnimation("Attack1");

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
