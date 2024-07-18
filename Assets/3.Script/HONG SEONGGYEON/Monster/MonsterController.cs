using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour, IstateMachineOwner
{
    public MonsterModel monsterModel;
    public Transform Target;
    public float Distance;

    private stateMachine stateMachine;
    private void Awake()
    {
        stateMachine = new stateMachine(this);
    }

    private void Start()
    {
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
        }
        monsterModel.state = monsterState;
    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        // Debug.Log("플레이애니");
        monsterModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    private void Update()
    {
       Distance = Vector3.Distance(transform.position, Target.position);
    }


    private void OnEnable()
    {

    }

}
