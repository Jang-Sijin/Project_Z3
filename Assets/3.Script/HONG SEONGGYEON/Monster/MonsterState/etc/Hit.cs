using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Hit");
        monsterController.monsterModel.isAttacked = false;
    }

    public override void Update()
    {
        base.Update();

        // ������ ���� ��� Hit ���·� �ٽ� ��ȯ
        if (monsterController.monsterModel.isAttacked)
        {
            monsterController.monsterModel.isAttacked = false; // �÷��� �ʱ�ȭ
            RestartHitAnimation();
            // return; // ���� ��ȯ �� �� �̻� Update�� �������� ����
        }

        // �ִϸ��̼��� �������� Ȯ��
        if (monsterController.IsAnimationFinished("Hit"))
        {
            monsterController.SwitchState(MonsterState.Idle);
        }

        if (monsterController.monsterModel.isDead)
        {
            monsterController.SwitchState(MonsterState.Dead);
        }
    }

    public override void Exit()
    {
        base.Exit();
        monsterController.monsterModel.isAttacked = false;
    }

    private void RestartHitAnimation()
    {
        // �ִϸ��̼��� ó������ �ٽ� ���
        //monsterController.PlayAnimation("Hit");
        monsterModel.animator.CrossFadeInFixedTime("Hit", 0.25f, -1, 0f);
    }
}