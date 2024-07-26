using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    Born,
    Idle,
    Walk,
    Run,
    Attack1,
    Attack2,
    Attack3,
    Attack4,
    Attack5,
    StunStart,
    StunLoop,
    StunEnd,
    Dead
}

public class BossModel : MonoBehaviour
{
    public float MaxHealth;
    public float Attack2;
    public float Attack3;
    public float Attack4;
    public float Attack5;
    public Transform Target;
    public float StartGroggypoint;

    private Navmesh BossNavi;
    public Animator animator;
    [HideInInspector] public float Distance;
    [HideInInspector] public BossState state;
    [HideInInspector] public bool isDead = false;
    [HideInInspector] public bool isGroggy = false;
    [HideInInspector] public float CurrentHealth;
    [HideInInspector] public float CurrentGroggypoint;

    // [SerializeField] public MonsterAttributes monster; // ���� �Ӽ� �߰�

    private void Start()
    {
        animator.applyRootMotion = true; // ��Ʈ ��� ����
        CurrentHealth = MaxHealth;
        BossNavi = GetComponent<Navmesh>();
    }

    public void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // ���� ȸ���� ���
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 10.0f);
        }
    }

    private void Update()
    {
        Distance = Vector3.Distance(transform.position, Target.position);
        if (state == BossState.Idle || state == BossState.Dead ||
            state == BossState.StunLoop || state == BossState.Born)
        {
            BossNavi.nmagent.updateRotation = false;
        }
        //  if (state != MonsterState.AttackType_01 && state != MonsterState.Idle
        //      && state != MonsterState.Stun && state != MonsterState.Dead
        //      && state != MonsterState.Born) ;
        //  {
        //      RotateTowards(Target.position);
        //  }

        if (CurrentGroggypoint >= StartGroggypoint)
        {
            isGroggy = true;
        }

        if (CurrentHealth <= 0)
        {
            isDead = true;
        }

     //  if (Input.GetKeyDown(KeyCode.K))
     //  {
     //      CurrentGroggypoint = 100;
     //  }


    }

    //  public MonsterAttributes Attributes => monster; // �Ӽ� ������ �߰�

    public bool isItemDrop()
    {
        int DropSucces = Random.Range(0, 2);
        if (DropSucces == 1)
        {
            Debug.Log("������ ��� ����");
            return true;
        }
        Debug.Log("����");
        return false;
    }


}
