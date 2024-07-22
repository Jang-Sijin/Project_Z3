using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour, IstateMachineOwner
{
    public MonsterModel monsterModel;
    public Transform Target;
    public float Distance;
    private Animator ani;
    public MonCol_Control mon_CO;

    private stateMachine stateMachine;
 //   private NavMeshAgent nmagent;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        stateMachine = new stateMachine(this);
        mon_CO = GetComponent<MonCol_Control>();

     //   nmagent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
     
        SwitchState(MonsterState.Idle);
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
            case MonsterState.Idle:
                stateMachine.EnterState<Idle>();
                break;
            case MonsterState.Run:
                stateMachine.EnterState<Run>();
                break;
            case MonsterState.Walk:
                stateMachine.EnterState<Walk>();
                break;
            case MonsterState.AttackType_01:
                stateMachine.EnterState<AttackType_01>();
                break;
            case MonsterState.AttackType_02:
                stateMachine.EnterState<AttackType_02>();
                break;    
            case MonsterState.AttackType_03_Start:
                stateMachine.EnterState<AttackType_03_Start>();
                break;
            case MonsterState.AttackType_03:
                stateMachine.EnterState<AttackType_03>();
                break;
            case MonsterState.Stun_Start:
                stateMachine.EnterState<Stun_Start>();
                break;
            case MonsterState.Stun:
                stateMachine.EnterState<Stun>();
                break;
            case MonsterState.Stun_End:
                stateMachine.EnterState<Stun_End>();
                break;
            case MonsterState.Dead:
                stateMachine.EnterState<Dead>();
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
