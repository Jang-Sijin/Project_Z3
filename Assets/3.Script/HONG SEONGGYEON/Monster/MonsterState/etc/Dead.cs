using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonsterStateBase

{
    private bool hasDroppedItem = false; // 아이템 드랍 여부 확인 변수
    private bool isSuccesDrop;
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Dead");
        isSuccesDrop = monsterController.monsterModel.isItemDrop();
    }

    public override void Update()
    {
        base.Update();
        if (monsterController.IsAnimationFinished("Dead"))
        {
            if (isSuccesDrop && !hasDroppedItem)
            {
             monsterController.itemDropManager.DropItem(); // 아이템 떨구는 로직 추가할 것
            }
            hasDroppedItem = true;
            monsterController.SwitchState(MonsterState.None);
        }

    }

    public override void Exit()
    {
        base.Exit();
        monsterController.OnMonsterDead(); // 몬스터 사망 처리 및 삭제
    }
}
