using System.Collections;
using System.Collections.Generic;
using System.Threading;
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
    private Rigidbody _rigidbody;

    private void Awake()
    {
        ani = GetComponent<Animator>();
        statemachine = new stateMachine(this);
        mon_CO = GetComponent<MonCol_Control>();

        nmagent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {     
        SwitchState(MonsterState.Born);
    }
    public bool IsAnimationFinished(string animationName)
    {
        Debug.Log($"{animationName} 몬스터 애니메이션 길이가 끝에 도달함.");

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
            case MonsterState.None:
                statemachine.Stop();
                break;

        }
        monsterModel.state = monsterState;

    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        monsterModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    public void RePlayAnimation(string animationName, float fixedTransitionDuration = 0.25f, int layer = -1, int single = -1)
    {
        monsterModel.animator.CrossFadeInFixedTime("Hit", 0.25f, layer, single);
    }

    public void OnMonsterDead()
    {
        Destroy(gameObject);        
    }

    public void TakeDamage(float playerDamage, Transform playerTransform)
    {
        Debug.Log("TakeDamage: 몬스터 대미지 피해 입음");

        if (monsterModel.CurrentHealth > 0)
        {
            // 몬스터 공격 받음(Hit) 상태로 변경
            SwitchState(MonsterState.Hit);

            monsterModel.CurrentHealth -= playerDamage;
            // EnemyUIController.RefreshHealth(_currentHealth, MaxHealth);
        }
        else
        {
            Debug.Log($"{gameObject.name}: 몬스터 사망");
            monsterModel.isDead = true;
            SwitchState(MonsterState.Dead);
        }
    }
}
