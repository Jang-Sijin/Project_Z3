using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonsterStateBase  // 스킬화할것
{
    private float RunCoolTme = 2.0f;
    private float CurrentCoolTime;
    public override void Enter()
    {
        base.Enter();
        CurrentCoolTime += Time.deltaTime;
        monsterController.PlayAnimation("Run");
        //  monsterController.monsterModel.nmagent.isStopped = false;
        //   monsterController.monsterModel.nmagent.speed = 3f; // 워크 속도 설정
    }
    public override void Update()
    {
        base.Update();
        //    monsterController.monsterModel.animator.SetFloat("MoveSpeed", monsterController.monsterModel.nmagent.velocity.magnitude);

        //뛰어가다가 워킹거리로 좁혀졌을 때
        if (CurrentCoolTime >= RunCoolTme)
        {
            if (monsterController.Distance <= 2.0f)
            {
                monsterController.SwitchState(MonsterState.AttackType_01);
                return;
                //   Waiting_co();
                //monsterController.SwitchState(MonsterState.Idle);
            }
            else
            {
                monsterController.SwitchState(MonsterState.Walk);
                return;
            }

        }
    }

    public override void Exit()
    {
        base.Exit();
        CurrentCoolTime = 0;

    }
    private IEnumerator Waiting_co()
    {
        Debug.Log("여긴가?");
        yield return new WaitForSeconds(5.0f);
    }

}
