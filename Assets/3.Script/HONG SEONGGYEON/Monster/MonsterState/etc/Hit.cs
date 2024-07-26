using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.GraphView.GraphView;

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

        // 공격을 받은 경우 Hit 상태로 다시 전환
        if (monsterController.monsterModel.isAttacked)
        {
            monsterController.monsterModel.isAttacked = false; // 플래그 초기화
            RestartHitAnimation();
            // return; // 상태 전환 후 더 이상 Update를 진행하지 않음
        }

        // 애니메이션이 끝났는지 확인
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
        // 애니메이션을 처음부터 다시 재생
        //monsterController.PlayAnimation("Hit");
        monsterModel.animator.CrossFadeInFixedTime("Hit", 0.25f, -1, 0f);
    }
}