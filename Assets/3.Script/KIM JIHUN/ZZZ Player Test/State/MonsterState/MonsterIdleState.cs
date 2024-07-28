using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterIdleState : EnemyStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("MonsterIdle");
    }

    public override void Update()
    {
        base.Update();
    }
}
