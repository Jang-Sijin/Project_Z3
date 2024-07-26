using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public enum EModelFoot
{
    Right,
    Left
}

public enum ECharacter
{
    Corin,
    Longinus,
    Anbi,
}

/// <summary>
/// �÷��̾��� �������ͽ� Ŭ����
/// ü��, ��Ƽ�� ��ų�� �� ĳ���ʹ� �Ҵ��̹Ƿ� ����
/// �� �������� ��� ĳ���Ͱ� �����ؼ� ���� -> playerController��
/// </summary>
public class PlayerStatus
{
    private float maxHealth; // �ִ� ü��
    private float currentHealth; //���� ü��
    private float maxSkillPoint; // �ִ� ��Ƽ�� ��ų ����Ʈ -> �ִ� 100�̶�� 50�� ��� -> �� 2�� ��ų ��� ����
    private float currentSkillPoint; // ���� ��Ƽ�� ��ų ����Ʈ

    public PlayerStatus(float maxHealth, float currentHealth, float maxSkillPoint, float currentSkillPoint)
    {
        this.maxHealth = maxHealth;
        this.currentHealth = currentHealth;
        this.maxSkillPoint = maxSkillPoint;
        this.currentSkillPoint = currentSkillPoint;
    }

    public float MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    public float CurrentHealth
    {
        get { return currentHealth; }
        set { currentHealth = Mathf.Clamp(value, 0, maxHealth); }
    }

    public float MaxSkillPoint
    {
        get { return maxSkillPoint; }
        set { maxSkillPoint = value; }
    }

    public float CurrentSkillPoint
    {
        get { return currentSkillPoint; }
        set { currentSkillPoint = Mathf.Clamp(value, 0, maxSkillPoint); }
    }
}
public class PlayerModel : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    public EPlayerState currentState;
    [HideInInspector] public CharacterController characterController;
    public ECharacter eCharacter { get; private set; }

    public float gravity = -9.8f;
    public CharacterInfo characterInfo;
    [HideInInspector] public int currentNormalAttakIndex = 1;

    public GameObject skillUltStartShot; // �ñر� ���� �ƽ�
    public GameObject skillUltShot; // �ñر� �ƽ�

    private AnimatorStateInfo stateInfo;

    private WeaponCollider weaponCollider;
    [SerializeField] private EffectPlayer effectPlayer;

    [HideInInspector] public EModelFoot foot = EModelFoot.Right;

    public bool hasSkillLoop = false;
    public bool hasSkillExtra = false;

    [HideInInspector] public PlayerStatus playerStatus;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
        playerStatus = new PlayerStatus(characterInfo.maxHealth, characterInfo.maxHealth, characterInfo.maxSkillPoint, 0f);
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
        //
        //Vector3 backDirection = rot * Vector3.back;
        //pos += backDirection * 3f;

        //Debug.Log($"Prev Pos : {pos - transform.position} Rot : {rot}");

        //gameObject.transform.position = pos - transform.position;
        //characterController.transform.position = pos-transform.position;
        characterController.Move(pos - transform.position);

        //Debug.Log($"Post Pos : {transform.position} Rot : {transform.rotation}");
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
    /// ĳ���� �� �޹� ����
    /// </summary>
    public void SetOutLeftFoot()
    {
        foot = EModelFoot.Left;
    }

    /// <summary>
    /// ĳ���� �� ������ ����
    /// </summary>
    public void SetOutRightFoot()
    {
        foot = EModelFoot.Right;
    }

    /// <summary>
    /// ���� ����� Enemy �ٶ󺸱�
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
    /// �ش� Trigger�� ON�̶�� ���� ���� ON
    /// �ش� Trigger�� OFF�̶�� ���� ���� OFF
    /// </summary>
    public void SetWeaponTrigger(int value)
    {
        bool trigger = value == 1;
        weaponCollider.SetShakeTrigger(trigger);
    }

    public void ShakeCamera(float time = 0.1f)
    {
        PlayerController.INSTANCE.ShakeCamera(3f, time);
    }
    public void StopShakeCamera()
    {
        PlayerController.INSTANCE.StopShakeCamera();
    }

    public void PlayEffect(int effectIndex)
    {
        effectPlayer.PlayEffect(effectIndex);
    }
    public void StopEffect(int effectIndex)
    {
        effectPlayer.StopEffect(effectIndex);
    }

    public void PlayParticle(int particleIndex)
    {
        effectPlayer.PlayParticle(particleIndex);
    }

    public void StopParticle(int particleIndex)
    {
        effectPlayer.StopParticle(particleIndex);
    }
}
