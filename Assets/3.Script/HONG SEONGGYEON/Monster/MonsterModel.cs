using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum MonsterState
{
    Idle,
    Run,
    Walk,
    AttackType_01,
    AttackType_02,
    AttackType_03_Start,
    AttackType_03,
    Stun_Start,
    Stun, Stun_End,
    Dead
}

public class MonsterModel : MonoBehaviour
{
    public Animator animator;
    public MonsterState state;
    [SerializeField] Transform Target;
    public float Distance;
    private float CurrentHealth = 100f;
    private float MaxHealth = 100f;
    public float Groggypoint = 0f;
    public bool isGroggy = false;
    public bool isDead = false;

 //   public NavMeshAgent nmagent;

    private void Start()
    {
   //     nmagent = GetComponent<NavMeshAgent>();
        animator.applyRootMotion = true; // 루트 모션 적용
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
            && state != MonsterState.Stun && state != MonsterState.Dead)
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

        if (Input.GetKeyDown(KeyCode.L))
        {
            Groggypoint = 100;
        }



    }
}
