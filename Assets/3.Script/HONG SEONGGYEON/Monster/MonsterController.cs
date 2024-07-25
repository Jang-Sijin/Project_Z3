using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour, IstateMachineOwner
{
    public MonsterModel monsterModel;
    protected Transform Target;
    protected float Distance;
    protected Animator ani;
    public MonCol_Control mon_CO;

    protected stateMachine statemachine;
    protected NavMeshAgent nmagent;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        statemachine = new stateMachine(this);
        mon_CO = GetComponent<MonCol_Control>();

        nmagent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
     
        SwitchState(MonsterState.Born);
    }
    public bool IsAnimationFinished(string animationName)
    {
        // 지정된 애니메이션 상태가 끝났는지 확인
        AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1.0f;
    }

    public void SwitchState(MonsterState monsterState)
    {
        switch (monsterState)
        {
            case MonsterState.Born:
                statemachine.EnterState<Born>();
                break;
            case MonsterState.Idle:
                statemachine.EnterState<Idle>();
                break;
            case MonsterState.Run:
                statemachine.EnterState<Run>();
                break;
            case MonsterState.Walk:
                statemachine.EnterState<Walk>();
                break;
            case MonsterState.AttackType_01:
                statemachine.EnterState<AttackType_01>();
                break;
            case MonsterState.AttackType_02:
                statemachine.EnterState<AttackType_02>();
                break;    
            case MonsterState.AttackType_03_Start:
                statemachine.EnterState<AttackType_03_Start>();
                break;
            case MonsterState.AttackType_03:
                statemachine.EnterState<AttackType_03>();
                break;
            case MonsterState.Stun_Start:
                statemachine.EnterState<Stun_Start>();
                break;
            case MonsterState.Stun:
                statemachine.EnterState<Stun>();
                break;
            case MonsterState.Stun_End:
                statemachine.EnterState<Stun_End>();
                break;
            case MonsterState.Dead:
                statemachine.EnterState<Dead>();
                break;
            case MonsterState.Hit:
                statemachine.EnterState<Hit>();
                break;

        }
        monsterModel.state = monsterState;

    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        monsterModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    private void Update()
    {
     
        
    }

    private void OnEnable() { }

}
