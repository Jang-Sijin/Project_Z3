using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Run : MonsterStateBase
{
    private float RunCoolTime = 2.0f;
    private float CurrentCoolTime;

    public override void Enter()
    {
        base.Enter();
        CurrentCoolTime = 0; // 초기화
        monsterController.PlayAnimation("Run");
    }

    public override void Update()
    {
        base.Update();
        CurrentCoolTime += Time.deltaTime; // 타이머 업데이트

        if (monsterController.monsterModel.isGroggy)
        {
            monsterController.SwitchState(MonsterState.Stun_Start);
        }
        else
        {
            var attributes = monsterController.monsterModel.monster;
            var distance = monsterController.monsterModel.Distance;
            if (distance <= attributes.attackRangeType1)     // 2
            {
                // Debug.Log($"쿨타임 다됨{CurrentCoolTime}");
                monsterController.SwitchState(MonsterState.AttackType_01);

            }

            if (CurrentCoolTime >= RunCoolTime)
            {
                if (distance <= attributes.attackRangeType1)    //2
                {
                    // Debug.Log($"쿨타임 다됨{CurrentCoolTime}");
                    monsterController.SwitchState(MonsterState.AttackType_01);

                }
                else
                {
                    Debug.Log("넘어와");
                    monsterController.SwitchState(MonsterState.Walk);

                }
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        CurrentCoolTime = 0; // 상태 종료 시 타이머 초기화
     //   Debug.Log($"쿨타임 초기화{CurrentCoolTime}");
    }

}
