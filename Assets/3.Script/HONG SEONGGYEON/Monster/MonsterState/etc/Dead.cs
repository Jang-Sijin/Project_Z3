using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead :MonsterStateBase

{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Dead");

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
