using System.Collections;
using UnityEngine;

public class Monster_Rootmotion : MonoBehaviour
{
    public Transform target; // 플레이어 타겟
    public float followDistance = 10.0f; // 추적 거리
    public float walkDistance = 3.0f; // 걷기 시작할 거리
    private Animator mon_ani;
    private bool isAttacking = false;
    private Monster_Queue monsterManager;
    public Monster_Control monster;
    private Collider Weaponcol;

    [SerializeField] private float coolTime = 5f;


    //몬스터 공격모션 중일때는 비활성해놔
    private void Start()
    {
        mon_ani = GetComponent<Animator>();
        mon_ani.applyRootMotion = true; // 루트 모션 사용
        monsterManager = FindObjectOfType<Monster_Queue>();
        monsterManager.RegisterMonster(this);

        monster = GetComponent<Monster_Control>();
        if (monster == null)
        {
            Debug.LogError("Monster_Control component not found!");
        }

        // 무기 콜라이더 설정 (Bip001 L Forearm에 있는 BoxCollider 찾기)
        Weaponcol = GetComponentInChildren<Collider>();
        if (Weaponcol == null)
        {
            Debug.LogError("Weapon Collider not found!");
        }
        Weaponcol.enabled = false;
    }

    private void Update()
    {
        if (monster.isGroggy) return; // 그로기 상태면 동작하지 않음

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > followDistance)  // 플레이어 따라 이동하는 로직
        {
            RotateTowards(target.position);
            mon_ani.SetBool("Running", true);
            mon_ani.SetBool("isWalk", false);
        }
        else if (distance > walkDistance) // 걷는 로직
        {
            RotateTowards(target.position);
            mon_ani.SetBool("Running", false);
            mon_ani.SetBool("isWalk", true);
        }
        else  // 공격 로직
        {
            mon_ani.SetBool("Running", false);
            mon_ani.SetBool("isWalk", false);
            if (!isAttacking)
            {
                monsterManager.RequestAttack(this);
            }
        }
    }

    private void LateUpdate()
    {
        if (isAttacking)
        {
            RotateTowards(target.position);
        }
    }

    private void RotateTowards(Vector3 targetPosition)
    {
        if (monster.isGroggy) return; // 그로기 상태면 동작하지 않음

        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // 수평 회전만 고려
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
        }
    }

    public void StartAttack()
    {
        if (monster.isGroggy) return; // 그로기 상태면 공격하지 않음

        isAttacking = true;
        int pattern = Random.Range(0, 4);
        RotateTowards(target.position); // 타겟 쪽으로 회전
        switch (pattern)
        {
            case 0:
                StartCoroutine(AttackType1_co());
                break;
            case 1:
                StartCoroutine(AttackType1_co());
                break;
            case 2:
                StartCoroutine(AttackType2_co());
                break;
            case 3:
                StartCoroutine(AttackType3_co());
                break;
        }
    }

    private IEnumerator AttackType1_co()
    {
        mon_ani.SetTrigger("canAttack_01");
        yield return new WaitForSeconds(1.0f); // 공격 애니메이션 재생 시간

        yield return new WaitForSeconds(coolTime); // 쿨타임 동안 대기
        isAttacking = false;
        monsterManager.AttackFinished();
    }

    private IEnumerator AttackType2_co()
    {
        RotateTowards(target.position); // 타겟 쪽으로 회전
        mon_ani.SetTrigger("canAttack_02");
        yield return new WaitForSeconds(1.0f); // 공격 애니메이션 재생 시간

        yield return new WaitForSeconds(coolTime); // 쿨타임 동안 대기
        isAttacking = false;
        monsterManager.AttackFinished();
    }

    private IEnumerator AttackType3_co()
    {
        RotateTowards(target.position); // 타겟 쪽으로 회전
        mon_ani.SetTrigger("canAttack_03");
        yield return new WaitForSeconds(1.0f); // 공격 애니메이션 재생 시간

        yield return new WaitForSeconds(coolTime); // 쿨타임 동안 대기
        isAttacking = false;
        monsterManager.AttackFinished();
    }

    // 애니메이션 이벤트에서 호출할 메서드
    public void EnableWeaponCollider()
    {
        Weaponcol.enabled = true;
    }

    // 애니메이션 이벤트에서 호출할 메서드
    public void DisableWeaponCollider()
    {
        Weaponcol.enabled = false;
    }
    
    public IEnumerator HitReaction()
    {
        // 애니메이션 재생 속도를 0으로 설정하여 잠시 멈춤
        mon_ani.speed = 0;
        yield return new WaitForSeconds(0.1f);
        // 애니메이션 재생 속도를 1로 설정하여 다시 재생
        mon_ani.speed = 1;

    }
}
