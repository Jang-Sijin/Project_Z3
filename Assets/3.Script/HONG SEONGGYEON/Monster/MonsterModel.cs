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
    AttackType_02
}

public class MonsterModel : MonoBehaviour
{
    public Animator animator;
    public MonsterState state;
    [SerializeField] Transform Target;
    public float Distance;

    public NavMeshAgent nmagent;

    private void Start()
    {
        nmagent = GetComponent<NavMeshAgent>();
        animator.applyRootMotion = true; // 루트 모션 적용
    }

    public void RotateTowards(Vector3 targetPosition)
    {
        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // 수평 회전만 고려
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
        }
    }

    private void Update()
    {

        if (state != MonsterState.AttackType_01)
        {
            RotateTowards(Target.position);
        }

     //   animator.SetFloat("MoveSpeed", nmagent.velocity.magnitude);

    }
}
