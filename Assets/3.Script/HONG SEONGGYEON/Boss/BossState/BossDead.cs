using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDead : BossStateBase
{
    private bool hasDroppedItem = false; // ������ ��� ���� Ȯ�� ����
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
                bossController.itemDropManager.DropItem(); // ������ ������ ���� �߰��� ��
            }
            hasDroppedItem = true;
            bossController.SwitchState(BossState.None);
        }

    }

    public override void Exit()
    {
        base.Exit();
        bossController.OnMonsterDead(); // ���� ��� ó�� �� ����
    }
}
