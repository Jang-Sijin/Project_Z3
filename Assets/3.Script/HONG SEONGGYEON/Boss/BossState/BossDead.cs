using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDead : BossStateBase
{
    public override void Enter()
    {
        base.Enter();
        bossController.PlayAnimation("Dead");
        Debug.Log(bossController.bossModel.isItemDrop());

    }

    public override void Update()
    {
        base.Update();
        if (bossController.IsAnimationFinished("Dead"))
        {
            if (bossController.bossModel.isItemDrop())
            {
                // 아이템 종류 골라서 드롭하는거 넣어
            }
            MonoBehaviour.Destroy(bossController.bossModel.gameObject);
        }

    }

    public override void Exit()
    {
        base.Exit();
    }
}
