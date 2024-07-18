using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Walk");
    }

    public override void Update()
    {
        base.Update();

        if (monsterController.Distance >= 5.0f)
        {
            monsterController.SwitchState(MonsterState.Run);
        }

        if (monsterController.Distance <= 2.0f)
        {
            monsterController.SwitchState(MonsterState.AttackType_01);  // ���߿� �����ϴ°ŷ� ���� �ٲܰ�
            Waiting_co(5.0f);
            monsterController.SwitchState(MonsterState.Idle);
        }

    }

    private IEnumerator Waiting_co(float second)
    {
        Debug.Log("���䰡?1");
        yield return new WaitForSeconds(second);
    }
}




