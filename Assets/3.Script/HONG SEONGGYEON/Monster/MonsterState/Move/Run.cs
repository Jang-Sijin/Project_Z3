using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run :MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();

        monsterController.PlayAnimation("Run");
        monsterController.monsterModel.nmagent.isStopped = false;
        monsterController.monsterModel.nmagent.speed = 3f; // 워크 속도 설정
    }
    public override void Update()
    {
        base.Update();
        monsterController.monsterModel.animator.SetFloat("MoveSpeed", monsterController.monsterModel.nmagent.velocity.magnitude);
        //뛰어가다가 워킹거리로 좁혀졌을 때
        if (monsterController.Distance > 2.0f && monsterController.Distance<5.0f)
        {
            monsterController.SwitchState(MonsterState.Walk);
            return;
        }
        
      else if(monsterController.Distance<=2.0f)
        {
            monsterController.SwitchState(MonsterState.AttackType_02);
            return;
            //   Waiting_co();
            //monsterController.SwitchState(MonsterState.Idle);
        }

    }

   private IEnumerator Waiting_co()
    {
        Debug.Log("여긴가?");
        yield return new WaitForSeconds(5.0f);
    }

}
