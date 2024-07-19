using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Walk");
      //  monsterController.monsterModel.nmagent.isStopped = false;
        //monsterController.monsterModel.nmagent.speed = 1.0f; // ��ũ �ӵ� ����
    }

    public override void Update()
    {
        base.Update();
        monsterController.monsterModel.animator.SetFloat("MoveSpeed", .05f) ;
        if (monsterController.Distance >= 5.0f)
        {
            monsterController.SwitchState(MonsterState.Run);
        }

       else if (monsterController.Distance <= 2.0f)
        {
            monsterController.SwitchState(MonsterState.AttackType_01);  // ���߿� �����ϴ°ŷ� ���� �ٲܰ�

        }

    }


}




