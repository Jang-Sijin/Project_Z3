using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDead : BossStateBase
{
    private bool hasDroppedItem = false; // 아이템 드랍 여부 확인 변수
    private bool isSuccesDrop;
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
            if (isSuccesDrop && !hasDroppedItem)
            {
                bossController.itemDropManager.DropItem(); // 아이템 떨구는 로직 추가할 것
            }
            hasDroppedItem = true;
            bossController.SwitchState(BossState.None);
        }

    }

    public override void Exit()
    {
        base.Exit();
        bossController.OnMonsterDead(); // 몬스터 사망 처리 및 삭제
    }
}
