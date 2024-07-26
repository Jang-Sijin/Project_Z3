using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonsterStateBase

{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Dead");
        Debug.Log(monsterController.monsterModel.isItemDrop());
    }

    public override void Update()
    {
        base.Update();
        if (monsterController.IsAnimationFinished("Dead"))
        {
            if (monsterController.monsterModel.isItemDrop())
            {
            // monsterController.itemDropManager.DropItem();  아이템 떨구는 로직 추가할 것
            }

            monsterController.SwitchState(MonsterState.None);
        }

    }

    public override void Exit()
    {
        base.Exit();

        monsterController.OnMonsterDead(); // 몬스터 사망 처리 및 삭제
    }
}
