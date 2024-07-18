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
        //뛰어가다가 워킹거리로 좁혀졌을 때
        if(monsterController.Distance > 2.0f && monsterController.Distance<5.0f)
        {
            monsterController.SwitchState(MonsterState.Walk);  // 나중에 걸어가는거로 조건 바꿀것
        }
        
        if(monsterController.Distance<=2.0f)
        {
            monsterController.SwitchState(MonsterState.AttackType_01);  // 나중에 공격하는거로 조건 바꿀것
            Waiting_co();
            monsterController.SwitchState(MonsterState.Idle);
        }

    }

   private IEnumerator Waiting_co()
    {
        Debug.Log("여긴가?");
        yield return new WaitForSeconds(5.0f);
    }

}
