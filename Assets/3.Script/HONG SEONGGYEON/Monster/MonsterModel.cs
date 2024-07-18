using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterState
{
    Idle,
    Run,
    Walk,
    AttackType_01

}

public class MonsterModel : MonoBehaviour
{
   public Animator animator;
   public   MonsterState state;
   [SerializeField] Transform Target;

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
        RotateTowards(Target.position);
    }
}
