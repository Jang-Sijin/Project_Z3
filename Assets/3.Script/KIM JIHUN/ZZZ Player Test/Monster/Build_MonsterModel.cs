using System;
using System.Collections;
using System.Collections.Generic;
using ExternalPropertyAttributes;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class Build_MonsterStatus
{
    private float _maxHealth;
    private float _defaultAttackDamage;

    private float _currentHealth;

    public Build_MonsterStatus(float maxHealth, float defaultAttackDamage)
    {
        _maxHealth = maxHealth;
        _defaultAttackDamage = defaultAttackDamage;

        _currentHealth = maxHealth;
    }

    public float MaxHealth
    {
        get { return _maxHealth; }
        set { _maxHealth = value; }
    }

    public float DefaultAttackDamage
    {
        get { return _defaultAttackDamage; }
        set { _defaultAttackDamage = value; }
    }

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }
}
public class Build_MonsterModel : MonoBehaviour
{    
    [HideInInspector] public Animator animator;
    public EMonsterState currentState;
    [HideInInspector] public Build_MonsterStatus monsterStatus;
    [HideInInspector] public CharacterController characterController;
    [HideInInspector] public float gravity = -9.8f;
    public Build_MonsterInfo monsterInfo;
    public AnimatorStateInfo stateInfo;

    [Header("몬스터 타겟팅 거리")]
    public float attackRange;
    public float findRange;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        monsterStatus = new Build_MonsterStatus(monsterInfo.maxHealth, monsterInfo.defaultAttackDamage);
    }

    public bool IsAnimationEnd()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0);
    }

    /// <summary>
    /// 플레이어가 시야 거리에 있는지 확인
    /// </summary>
    /// <returns>시야 거리에 있다면 True, 없다면 False</returns>
    public bool IsPlayerInSight()
    {
        if (monsterStatus.CurrentHealth <= 0)
            return false;

        float distance = Vector3.Distance(
            PlayerController.INSTANCE.playerModel.transform.position,
            this.transform.position);

        if (distance <= findRange)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 플레이어가 공격 범위에 있는지 확인
    /// </summary>
    /// <returns>공격 거리에 있다면 True, 없다면 False</returns>
    public bool IsPlayerInAttackRange()
    {
        if (monsterStatus.CurrentHealth <= 0)
            return false;


        float distance = Vector3.Distance(PlayerController.INSTANCE.playerModel.transform.position, this.transform.position);

        if (distance <= attackRange)
            return true;
        else
            return false;
    }

    /// <summary>
    /// 몬스터 사망시 메서드
    /// </summary>
    public void MonsterDie()
    {
        Destroy(gameObject);
    }
}
