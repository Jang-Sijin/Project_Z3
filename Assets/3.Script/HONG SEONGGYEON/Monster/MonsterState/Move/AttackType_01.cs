using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType_01 : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();

        monsterController.PlayAnimation("AttackType_01");
    }
    public override void Update()
    {
        base.Update();
    }
}

