using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterDieState : EnemyStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Die");
    }
    public override void Update()
    {
        base.Update();
        if (IsAnimationEnd())
        {
            monsterController.monsterModel.MonsterDie();

            return;
        }
    }
}
