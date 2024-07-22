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
            // Idle ���¸� 2�ʰ� ������ �� ���� ��ȯ�� ����
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
                    //Debug.Log("���̵�");
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
