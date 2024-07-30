using System.Collections;
using System.Collections.Generic;
using JSJ;
using UnityEngine;

public class Build_MonsterController : MonoBehaviour, IStateMachineOwner
{
    [HideInInspector] public Build_MonsterModel monsterModel;

    private StateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new StateMachine(this);
        monsterModel = GetComponent<Build_MonsterModel>();
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
                stateMachine.EnterState<MonsterSpawnState>();
                break;
            case EMonsterState.Run:
                stateMachine.EnterState<MonsterRunState>();
                break;
            case EMonsterState.Attack:
                stateMachine.EnterState<MonsterAttackState>(true);
                break;
            case EMonsterState.Die:
                stateMachine.EnterState<MonsterDieState>();
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
