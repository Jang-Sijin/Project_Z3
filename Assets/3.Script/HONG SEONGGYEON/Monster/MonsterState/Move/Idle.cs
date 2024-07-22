using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonsterStateBase
{
    private float idleTime = 2.0f; // Idle 상태 유지 시간
    private float currentIdleTime = 0.0f;

    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Idle");
     //   monsterController.monsterModel.nmagent.isStopped = true; // Idle 상태에서는 이동 멈춤
        currentIdleTime = 0.0f; // 타이머 초기화
    }

    public override void Update()
    {
        base.Update();
        currentIdleTime += Time.deltaTime;
        if(monsterController.monsterModel.isDead)
        {
            monsterController.SwitchState(MonsterState.Dead);
        }
        else if (monsterController.monsterModel.isGroggy)
        {
            monsterController.SwitchState(MonsterState.Stun_Start);
        }
        else
        {
            // Idle 상태를 2초간 유지한 후 상태 전환을 시작
            if (currentIdleTime >= idleTime)
            {
                if (monsterController.monsterModel.Distance >= 7.0f)
                {
                    monsterController.SwitchState(MonsterState.Run);
                }
                else if (monsterController.monsterModel.Distance > 3.0f&&monsterController.monsterModel.Distance < 7.0f)
                {
                    monsterController.SwitchState(MonsterState.Walk);
                }
                else if (monsterController.monsterModel.Distance > 2 && monsterController.monsterModel.Distance < 3)
                {
                    //Debug.Log("아이들");
                    monsterController.SwitchState(MonsterState.AttackType_02);
                }

                else if (monsterController.monsterModel.Distance > 4.0f && monsterController.monsterModel.Distance < 7.0f)
                {
                    monsterController.SwitchState(MonsterState.AttackType_03_Start);
                }
                else if (monsterController.monsterModel.Distance < 2.0f)
                {
                    monsterController.SwitchState(MonsterState.AttackType_01);
                }
            }
        }
    }
}
