using System.Collections;
using System.Collections.Generic;
using JSJ;
using UnityEngine;

public class Build_MonsterController : SingleMonoBase<Build_MonsterController>, IStateMachineOwner
{
    public Build_MonsterModel monsterModel;

    private StateMachine stateMachine;

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);
    }

    private void Start()
    {
        SwitchState(EMonsterState.Spawn);
    }

    public void SwitchState(EMonsterState monsterState)
    {
        monsterModel.currentState = monsterState;
        switch (monsterState)
        {
            case EMonsterState.Idle:
                stateMachine.EnterState<MonsterIdleState>();
                break;
            case EMonsterState.Spawn:
                break;
            case EMonsterState.Run:
                break;
            case EMonsterState.Walk:
                break;
            case EMonsterState.Attack:
                break;
            case EMonsterState.Die:
                break;
        }
    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        monsterModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration, float fixedTimeOffset)
    {
        monsterModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration, 0, fixedTimeOffset);
    }
}
