using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
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
    Dead
}

public class MonsterModel : MonoBehaviour
{
    public Animator animator;
    public MonsterState state;
    [SerializeField] public Transform Target;
    public float Distance;
    [SerializeField] private float CurrentHealth = 100f;
    [SerializeField] private float MaxHealth = 100f;
    public float Groggypoint = 0f;
    public bool isGroggy = false;
    public bool isDead = false;

    [SerializeField] public MonsterAttributes monster; // ���� �Ӽ� �߰�

    private void Start()
    {
        animator.applyRootMotion = true; // ��Ʈ ��� ����
    }

    public void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // ���� ȸ���� ���
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
            && state != MonsterState.Born) ;
        {
            RotateTowards(Target.position);
        }

        if (Groggypoint >= 100f)
        {
            isGroggy = true;
        }

        if (CurrentHealth <= 0)
        {
            isDead = true;
        }

        if(Input.GetKeyDown(KeyCode.K))
        {
            CurrentHealth = 0;
        }

       
    }

    public MonsterAttributes Attributes => monster; // �Ӽ� ������ �߰�

    public bool isItemDrop()
    {
        int DropSucces = Random.Range(0, 2);
        if(DropSucces==1)
        {
            Debug.Log("������ ��� ����");
            return true;
        }
        Debug.Log("����");
        return false;
    }


}
