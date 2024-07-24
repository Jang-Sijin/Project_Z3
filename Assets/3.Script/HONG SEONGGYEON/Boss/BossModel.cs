using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BossState
{
    Born,
    Idle,
    Walk,
    Run,
    Attack1
}

public class BossModel : MonoBehaviour
{
    public Animator animator;
    public BossState state;
    [SerializeField] public Transform Target;
    public float Distance;
    [SerializeField] private float CurrentHealth = 500f;
    [SerializeField] private float MaxHealth = 500f;
    public float Groggypoint = 0f;
    public bool isGroggy = false;
    public bool isDead = false;

   // [SerializeField] public MonsterAttributes monster; // 몬스터 속성 추가

    private void Start()
    {
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

      //  if (state != MonsterState.AttackType_01 && state != MonsterState.Idle
      //      && state != MonsterState.Stun && state != MonsterState.Dead
      //      && state != MonsterState.Born) ;
      //  {
      //      RotateTowards(Target.position);
      //  }

        if (Groggypoint >= 100f)
        {
            isGroggy = true;
        }

        if (CurrentHealth <= 0)
        {
            isDead = true;
        }

       // if (Input.GetKeyDown(KeyCode.K))
       // {
       //     CurrentHealth = 0;
       // }


    }

  //  public MonsterAttributes Attributes => monster; // 속성 접근자 추가

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


}
