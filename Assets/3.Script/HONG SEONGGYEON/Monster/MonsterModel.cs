using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    None,
    Born,
    Idle,
    Run,
    Walk,
    AttackType_01,
    AttackType_02,
    AttackType_03_Start,
    AttackType_03,
    Stun_Start,
    Stun,
    Stun_End,
    Dead,
    Hit    
}

public class MonsterModel : MonoBehaviour
{
    public float MaxHealth;
    public float Attack2;
    public float Attack3;
    public Transform Target;
    public float StartGroggypoint;
    private EnemyUIController EnemyUIController;

    public Animator animator;
    [HideInInspector] public float Distance;
    [HideInInspector] public MonsterState state;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isGroggy = false;
    [HideInInspector] private float _currentHealth;
    [HideInInspector] public float CurrentGroggypoint = 0f;
    [HideInInspector] public bool isAttacked = false;

    public float CurrentHealth
    {
        get { return _currentHealth; }
        set { _currentHealth = value; }
    }

        [SerializeField] public MonsterAttributes monster; // 몬스터 속성 추가

    private void Start()
    {
        EnemyUIController = GetComponentInChildren<EnemyUIController>();
        animator.applyRootMotion = true; // 루트 모션 적용
        _currentHealth = MaxHealth;
    }

    public void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // 수평 회전만 고려
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 3.0f);
        }
    }

    private void Update()
    {
        Distance = Vector3.Distance(transform.position, Target.position);

        if (state != MonsterState.AttackType_01 && state != MonsterState.Idle
            && state != MonsterState.Stun && state != MonsterState.Dead
            && state != MonsterState.Born&&state!=MonsterState.Hit)
        {
            RotateTowards(Target.position);
        }

        if (CurrentGroggypoint >=StartGroggypoint)
        {
            isGroggy = true;
        }

        //if (_currentHealth <= 0)
        //{
        //    isDead = true;
        //}

        //if (Input.GetKeyDown(KeyCode.K))
        //{
        //    _currentHealth = 0;
        //}


    }

    public MonsterAttributes Attributes => monster; // 속성 접근자 추가

    public bool isItemDrop()
    {
        int DropSucces = Random.Range(0, 2);
        if (DropSucces == 1)
        {
            Debug.Log("아이템 드롭 성공");
            return true;
        }
        Debug.Log("실패");
        return false;
    }

    //public void TakeDamage(float playerDamage)
    //{
    //    Debug.Log("TakeDamage: 몬스터 대미지 피해 입음");

    //    _currentHealth -= playerDamage;
    //    //   EnemyUIController.RefreshHealth(_currentHealth, MaxHealth);
        
    //    if(_currentHealth <= 0)
    //    {
    //        Debug.Log($"{gameObject.name}: 몬스터 사망");
    //        isDead = true;
    //        state = MonsterState.Dead;
    //    }
    //}
}
