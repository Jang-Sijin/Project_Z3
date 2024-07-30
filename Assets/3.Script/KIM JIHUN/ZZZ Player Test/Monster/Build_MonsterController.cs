using System.Collections;
using System.Collections.Generic;
using JSJ;
using UnityEngine;

public enum EMonsterState
{
    Idle,
    Spawn,
    Run,
    Attack,
    Hit,
    Die
}

public class Build_MonsterController : MonoBehaviour, IStateMachineOwner
{
    [HideInInspector] public Build_MonsterModel monsterModel;

    private StateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new StateMachine(this);
        monsterModel = GetComponent<Build_MonsterModel>();
    }

    private void OnEnable()
    {
        SwitchState(EMonsterState.Spawn);
    }

    private void OnDisable()
    {
        SwitchState(EMonsterState.Idle);
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
                case EMonsterState.Hit:
                stateMachine.EnterState<MonsterHitState>();
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

    public void TakeDamage(float playerDamage, Transform playerTransform)
    {
        //  Debug.Log("TakeDamage: 몬스터 대미지 피해 입음");
        if (monsterModel.currentState == EMonsterState.Die)
        {
            return;
        }

        if (monsterModel.monsterStatus.CurrentHealth > 0)
        {
            // 몬스터 공격 받음(Hit) 상태로 변경
            SwitchState(EMonsterState.Hit);

            monsterModel.monsterStatus.CurrentHealth -= playerDamage;
            // EnemyUIController.RefreshHealth(_currentHealth, MaxHealth);
        }
        else
        {
            Debug.Log($"{gameObject.name}: 몬스터 사망");
            //monsterModel.isDead = true;
            SwitchState(EMonsterState.Die);
        }
    }
}
