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

    public GameObject skillUltStartShot; // 궁극기 시작 컷신
    public GameObject skillUltShot; // 궁극기 컷신

    private AnimatorStateInfo stateInfo;

    private WeaponCollider weaponCollider;
    [SerializeField] private VfxPlayer vfxPlayer;

    [HideInInspector] public EModelFoot foot = EModelFoot.Right;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    private void Start()
    {
        weaponCollider = GetComponentInChildren<WeaponCollider>();
    }


    public void Enter(Vector3 pos, Quaternion rot)
    {
        MonoManager.INSTANCE.RemoveUpdateAction(OnExit);

        Vector3 rightDirection = rot * Vector3.right;
        pos += rightDirection * 0.8f;

        Vector3 backDirection = rot * Vector3.back;
        pos += backDirection * 3f;

        characterController.Move(pos - transform.position);
        transform.rotation = rot;
    }

    public void Exit()
    {
        animator.CrossFadeInFixedTime("Switch_Out_Normal", 0.1f);
        MonoManager.INSTANCE.AddUpdateAction(OnExit);
    }

    public void OnExit()
    {
        if (IsAnimationEnd())
        {
            gameObject.SetActive(false);
            MonoManager.INSTANCE.RemoveUpdateAction(OnExit);
        }
    }

    public bool IsAnimationEnd()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
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

    /// <summary>
    /// 가장 가까운 Enemy 바라보기
    /// </summary>
    public void LookEnemy()
    {
        if (PlayerController.INSTANCE.closestEnemy != null)
        {
            Quaternion targetQua = Quaternion.LookRotation(PlayerController.INSTANCE.directionToEnemy);
            transform.rotation = targetQua;
        }
    }

    /// <summary>
    /// 해당 Trigger가 ON이라면 공격 판정 ON
    /// 해당 Trigger가 OFF이라면 공격 판정 OFF
    /// </summary>
    public void SetWeaponTrigger(int value)
    {
        bool trigger = value == 1;
        weaponCollider.SetShakeTrigger(trigger);
    }

    public void ShakeCamera()
    {
        PlayerController.INSTANCE.ShakeCamera(3f, 0.1f);
    }
    public void ShakeCameraForOneSec()
    {
        PlayerController.INSTANCE.ShakeCamera(1f, 1f);
    }

    public void PlayVFX(int vfxIndex)
    {
        vfxPlayer.PlayVFX(vfxIndex);
    }
}
