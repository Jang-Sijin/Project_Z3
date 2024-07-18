using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run :MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();

        monsterController.PlayAnimation("Run");
    }
    public override void Update()
    {
        base.Update();
        //�پ�ٰ� ��ŷ�Ÿ��� �������� ��
        if(monsterController.Distance > 2.0f && monsterController.Distance<5.0f)
        {
            monsterController.SwitchState(MonsterState.Walk);  // ���߿� �ɾ�°ŷ� ���� �ٲܰ�
        }
        
        if(monsterController.Distance<=2.0f)
        {
            monsterController.SwitchState(MonsterState.AttackType_01);  // ���߿� �����ϴ°ŷ� ���� �ٲܰ�
            Waiting_co();
            monsterController.SwitchState(MonsterState.Idle);
        }

    }

   private IEnumerator Waiting_co()
    {
        Debug.Log("���䰡?");
        yield return new WaitForSeconds(5.0f);
    }

}
