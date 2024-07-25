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
                // ������ ���� ��� ����ϴ°� �־�
            }
            MonoBehaviour.Destroy(monsterController.monsterModel.gameObject);
        }

    }

    public override void Exit()
    {
        base.Exit();    
    }
}
