using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateBase : StateBase
{
    protected Build_MonsterController monsterController;
    protected Build_MonsterModel monsterModel;
    private AnimatorStateInfo stateInfo;
    protected float statePlayingTime = 0;

    public override void Enter()
    {
        statePlayingTime = 0;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }

    public override void Init(IStateMachineOwner owner)
    {
        monsterController = (Build_MonsterController)owner;
        monsterModel = monsterController.monsterModel;
    }

    public override void LateUpdate()
    {
    }

    public override void UnInit()
    {
    }

    public override void Update()
    {
        statePlayingTime += Time.deltaTime;

        //체력이 0일때 사망 상태로 변경 -> Build_MonsterController 스크립터의 TakeDamage 메서드에서 처리중...
        //if (monsterModel.monsterStatus.CurrentHealth <= 0)
        //{
        //    monsterController.SwitchState(EMonsterState.Die);
        //    Debug.Log("monster Die");            
        //}
    }

    public bool IsAnimationEnd()
    {
        stateInfo = monsterModel.animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1.0f && !monsterModel.animator.IsInTransition(0);
    }

    public float GetNormalizedTime()
    {
        stateInfo = monsterModel.animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime;
    }
}
