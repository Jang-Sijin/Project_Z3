using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : MonsterStateBase
{
    private float idleTime = 2.0f; // Idle ���� ���� �ð�
    private float currentIdleTime = 0.0f;

    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Idle");
        //   monsterController.monsterModel.nmagent.isStopped = true; // Idle ���¿����� �̵� ����
        currentIdleTime = 0.0f; // Ÿ�̸� �ʱ�ȭ
    }

    public override void Update()
    {
        base.Update();
        currentIdleTime += Time.deltaTime;
        if (monsterController.monsterModel.isDead)
        {
            monsterController.SwitchState(MonsterState.Dead);
        }
        else if (monsterController.monsterModel.isGroggy)
        {
            monsterController.SwitchState(MonsterState.Stun_Start);
        }
        else
        {
            // Idle ���¸� 2�ʰ� ������ �� ���� ��ȯ�� ����
            if (currentIdleTime >= idleTime)
            {
                var attributes = monsterController.monsterModel.monster;
                var distance = monsterController.monsterModel.Distance;
                if (distance >= attributes.runRange)
                {
                    monsterController.SwitchState(MonsterState.Run);
                }
                else if (attributes.walkRange < distance && distance < attributes.runRange)
                {
                    monsterController.SwitchState(MonsterState.Walk);
                }
                else if (attributes.attackRangeType1 < distance && distance < attributes.attackRangeType3_Start)
                {
                    //Debug.Log("���̵�");
                    monsterController.SwitchState(MonsterState.AttackType_02);
                }

                else if (attributes.attackRangeType2 < distance && distance < attributes.runRange)
                {
                    monsterController.SwitchState(MonsterState.AttackType_03_Start);
                }
                else if (distance < attributes.attackRangeType1)
                {
                    monsterController.SwitchState(MonsterState.AttackType_01);
                }
            }
        }
    }
}
