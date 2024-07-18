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
            monsterController.SwitchState(MonsterState.AttackType_01);  // 나중에 공격하는거로 조건 바꿀것
            Waiting_co(5.0f);
            monsterController.SwitchState(MonsterState.Idle);
        }

    }

    private IEnumerator Waiting_co(float second)
    {
        Debug.Log("여긴가?1");
        yield return new WaitForSeconds(second);
    }
}




