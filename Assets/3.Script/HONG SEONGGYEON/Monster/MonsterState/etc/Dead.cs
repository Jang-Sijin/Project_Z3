using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonsterStateBase

{
    private bool hasDroppedItem = false; // ������ ��� ���� Ȯ�� ����
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
             monsterController.itemDropManager.DropItem(); // ������ ������ ���� �߰��� ��
            }
            hasDroppedItem = true;
            monsterController.SwitchState(MonsterState.None);
        }

    }

    public override void Exit()
    {
        base.Exit();
        monsterController.OnMonsterDead(); // ���� ��� ó�� �� ����
    }
}
