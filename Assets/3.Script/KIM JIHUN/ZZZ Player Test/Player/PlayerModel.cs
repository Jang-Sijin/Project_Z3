using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EModelFoot
{
    Right,
    Left
}
public class PlayerModel : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    public EPlayerState currentState;
    [HideInInspector] public CharacterController characterController;

    public float gravity = -9.8f;
    public SkillConfig skillConfig;
    public int currentNormalAttakIndex = 1;
    public GameObject skillUltStartShot;
    public GameObject skillUltShot;

    private AnimatorStateInfo stateInfo;

    [HideInInspector] public WeaponCollider weapon;

    [HideInInspector] public EModelFoot foot = EModelFoot.Right;
    private void Awake()
    {
        weapon = GetComponentInChildren<WeaponCollider>();
        SetWeapon(false);
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Update()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
    }

    public void Enter()
    {
        MonoManager.INSTANCE.RemoveUpdateAction(OnExit);
    }

    public void Exit()
    {
        animator.CrossFadeInFixedTime("SwitchOutNormal", 0.1f);
        MonoManager.INSTANCE.AddUpdateAction(OnExit);
    }

    public void OnExit()
    {
        if(IsAnimationEnd())
        {
            gameObject.SetActive(false);
            MonoManager.INSTANCE.RemoveUpdateAction(OnExit);
        }
    }

    public bool IsAnimationEnd()
    {
        return stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0);
    }

    /// <summary>
    /// 캐릭터 모델 왼발 해제
    /// </summary>
    public void SetOutLeftFoot()
    {
        foot = EModelFoot.Left;
    }

    /// <summary>
    /// 캐릭터 모델 오른발 해제
    /// </summary>
    public void SetOutRightFoot()
    {
        foot = EModelFoot.Right;
    }

    public void SetWeapon(bool value)
    {
        weapon.enabled = value;
    }
}
