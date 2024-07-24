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
        CurrentCoolTime = 0; // �ʱ�ȭ
        monsterController.PlayAnimation("Run");
    }

    public override void Update()
    {
        base.Update();
        CurrentCoolTime += Time.deltaTime; // Ÿ�̸� ������Ʈ

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
                // Debug.Log($"��Ÿ�� �ٵ�{CurrentCoolTime}");
                monsterController.SwitchState(MonsterState.AttackType_01);

            }

            if (CurrentCoolTime >= RunCoolTime)
            {
                if (distance <= attributes.attackRangeType1)    //2
                {
                    // Debug.Log($"��Ÿ�� �ٵ�{CurrentCoolTime}");
                    monsterController.SwitchState(MonsterState.AttackType_01);

                }
                else
                {
                    Debug.Log("�Ѿ��");
                    monsterController.SwitchState(MonsterState.Walk);

                }
            }
        }
    }

    public override void Exit()
    {
        base.Exit();
        CurrentCoolTime = 0; // ���� ���� �� Ÿ�̸� �ʱ�ȭ
     //   Debug.Log($"��Ÿ�� �ʱ�ȭ{CurrentCoolTime}");
    }

}
