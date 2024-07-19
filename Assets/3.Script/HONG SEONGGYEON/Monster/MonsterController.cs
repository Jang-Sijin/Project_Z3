using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MonsterController : MonoBehaviour, IstateMachineOwner
{
    public MonsterModel monsterModel;
    public Transform Target;
    public float Distance;

    private stateMachine stateMachine;
    private NavMeshAgent nmagent;

    private void Awake()
    {
        stateMachine = new stateMachine(this);
        nmagent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        Debug.Log("들어오나?");
        SwitchState(MonsterState.Idle);
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
                
;
        }
        monsterModel.state = monsterState;

        // 공격 상태로 전환 시 NavMeshAgent 멈추기
        //if (monsterState == MonsterState.AttackType_01)
        //{
        //    nmagent.isStopped = true;
        //    nmagent.updatePosition = false;
        //    nmagent.updateRotation = false;
        //}
    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        monsterModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    private void Update()
    {
        Distance = Vector3.Distance(transform.position, Target.position);
        //    monsterModel.nmagent.SetDestination(Target.position);
        

    }

    private void OnEnable() { }
}
