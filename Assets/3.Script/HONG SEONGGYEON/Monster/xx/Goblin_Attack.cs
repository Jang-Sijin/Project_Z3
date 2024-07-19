using System.Collections;
using UnityEngine;

public class Goblin_Attack : MonoBehaviour
{
    public Transform target; // 플레이어 타겟
    public float walkDistance; // 걷기 시작할 거리
    public float attackDistance; // 공격 시작할 거리
    public float attackDistance_3; // 공격3 시작할 거리
    private Animator mon_ani;
    private bool isAttacking = false;
    private bool isStart = false;
    private Monster_Queue monsterManager;
    public Monster_Control monster;
    private Collider[] Colliders;

    [SerializeField] private float coolTime;

    private void Start()
    {
        mon_ani = GetComponent<Animator>();
        mon_ani.applyRootMotion = true; // 루트 모션 사용
        monsterManager = FindObjectOfType<Monster_Queue>();

        monster = GetComponent<Monster_Control>();

        // 무기 콜라이더 설정 (모든 자식 개체에 있는 모든 Collider 찾기)
        Colliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in Colliders)
        {
            if (col.CompareTag("MovingUI"))
            {
                col.enabled = false;
            }
            else col.enabled = true;
        }


        // 시작할 때 idle 코루틴 실행
        StartCoroutine(StartIdle_co());
    }

    private void Update()
    {
        if (monster.isGroggy) return; // 그로기 상태면 동작하지 않음
        if (!isStart) return; // 시작 상태가 아닐 때 동작하지 않음

        float distance = Vector3.Distance(transform.position, target.position);
        RotateTowards(target.position);
   //     if (distance < attackDistance_3 && !isAttacking)
   //     {
   //         StartCoroutine(AttackType3_co());
   //     }
         if (distance < walkDistance)
        {
            mon_ani.SetBool("Running", false);
            mon_ani.SetBool("Walking", true);
            if (distance < attackDistance && !isAttacking)
            {
                StartAttack();
            }
        }
        else
        {
            mon_ani.SetBool("Running", true);
            mon_ani.SetBool("Walking", false);
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

        int pattern = Random.Range(0, 3);
        if (pattern == 0)
        {
            StartCoroutine(AttackType2_co());
        }
        else
        {
            StartCoroutine(AttackType1_co());
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
        mon_ani.SetTrigger("canAttack_02");
        yield return new WaitForSeconds(1.0f); // 공격 애니메이션 재생 시간
        yield return new WaitForSeconds(coolTime); // 쿨타임 동안 대기

        isAttacking = false;
        monsterManager.AttackFinished();
    }

    private IEnumerator AttackType3_co()
    {
        isAttacking = true;
        mon_ani.SetTrigger("canAttack_03");
        Debug.Log("테스트 시작");

        while (true)
        {
            AnimatorStateInfo stateInfo = mon_ani.GetCurrentAnimatorStateInfo(0);
            float aniTime = stateInfo.normalizedTime;
            float currentDistance = Vector3.Distance(transform.position, target.position);

            // 애니메이션이 진행 중일 때 거리 조건을 체크
            if (aniTime < 1.0f && currentDistance < 1.0f)
            {
                mon_ani.SetBool("canAttack_03_End", true);
                yield return new WaitForSeconds(2.0f);
                Debug.Log("바로재생");
                mon_ani.SetBool("canAttack_03_End", false);
                yield return new WaitForSeconds(coolTime); // 쿨타임 동안 대기
                isAttacking = false;
                monsterManager.AttackFinished();
                yield break;
            }

            // 애니메이션이 끝난 경우 루프 탈출
            if (aniTime >= 1.0f && stateInfo.IsName("Monster_Claymore_Ani_Attack_03"))
            {
                //  Debug.Log("애니메이션 끝: " + stateInfo.IsName("Monster_Claymore_Ani_Attack_03"));
                break;
            }

            yield return null;
        }

        mon_ani.SetBool("canAttack_03_End", true);
        yield return new WaitForSeconds(2.0f);
        Debug.Log("기다렸다 재생");
        mon_ani.SetBool("canAttack_03_End", false);
        yield return new WaitForSeconds(coolTime); // 쿨타임 동안 대기
        isAttacking = false;
        monsterManager.AttackFinished();
    }

    private IEnumerator StartIdle_co()
    {
        mon_ani.SetBool("Walking", false);
        mon_ani.SetBool("Running", false);
        yield return new WaitForSeconds(5.0f); // idle 상태를 3초 동안 유지
        isStart = true;
    }

    // 애니메이션 이벤트에서 호출할 메서드
    public void EnableWeaponCollider()
    {
        foreach (Collider col in Colliders)
        {
            if (col.CompareTag("MovingUI"))
            {
                col.enabled = true;
            }
        }
    }

    // 애니메이션 이벤트에서 호출할 메서드
    public void DisableWeaponCollider()
    {
        foreach (Collider col in Colliders)
        {
            if(col.CompareTag("MovingUI"))
            {
                col.enabled = false;
            }
        }
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
