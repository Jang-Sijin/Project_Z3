using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossController : MonoBehaviour, IstateMachineOwner
{
    public BossModel bossModel;
    protected Transform Target;
    protected Animator ani;

    [HideInInspector] public MonCol_Control mon_CO;
    [HideInInspector] protected float Distance;
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

        SwitchState(BossState.Born);
    }
    public bool IsAnimationFinished(string animationName)
    {
        // 지정된 애니메이션 상태가 끝났는지 확인
        AnimatorStateInfo stateInfo = ani.GetCurrentAnimatorStateInfo(0);
        return stateInfo.IsName(animationName) && stateInfo.normalizedTime >= 1.0f;
    }

    public void SwitchState(BossState bossstate)
    {
        switch (bossstate)
        {
            case BossState.Born:
                statemachine.EnterState<BossBorn>();
                break;
            case BossState.Idle:
                statemachine.EnterState<BossIdle>();
                break;
            case BossState.Walk:
                statemachine.EnterState<BossWalk>();
                break;
            case BossState.Attack1:
                statemachine.EnterState<BossAttack1>();
                break;
            case BossState.Attack2:
                statemachine.EnterState<BossAttack2>();
                break;
            case BossState.Attack3:
                statemachine.EnterState<BossAttack3>();
                break;
            case BossState.Attack4:
                statemachine.EnterState<BossAttack4>();
                break;
            case BossState.Attack5:
                statemachine.EnterState<BossAttack5>();
                break;
            case BossState.StunStart:
                statemachine.EnterState<BossStunStart>();
                break;
            case BossState.StunLoop:
                statemachine.EnterState<BossStunLoop>();
                break;
            case BossState.StunEnd:
                statemachine.EnterState<BossStunEnd>();
                break;
            case BossState.Dead:
                statemachine.EnterState<BossDead>();
                break;




        }
        bossModel.state = bossstate;

    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        bossModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    private void Update()
    {


    }

    private void OnEnable() { }

}
