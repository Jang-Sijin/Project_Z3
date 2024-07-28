using System;
using System.Collections;
using System.Collections.Generic;
using ExternalPropertyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Build_MonsterStatus
{
    private float _maxHealth;
    private float _defaultAttackDamage;
    public float[] _attackDamageMultiple;

    private float _currentHealth;

    public Build_MonsterStatus(float maxHealth, float defaultAttackDamage, float[] attackDamageMultiple)
    {
        _maxHealth = maxHealth;
        _defaultAttackDamage = defaultAttackDamage;

        _attackDamageMultiple = new float[attackDamageMultiple.Length];
        Array.Copy(attackDamageMultiple, _attackDamageMultiple, _attackDamageMultiple.Length);

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
    private AnimatorStateInfo stateInfo;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        monsterStatus = new Build_MonsterStatus(monsterInfo.maxHealth, monsterInfo.defaultAttackDamage, monsterInfo.attackDamageMultiple);
    }

    public bool IsAnimationEnd()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0);
    }
}
