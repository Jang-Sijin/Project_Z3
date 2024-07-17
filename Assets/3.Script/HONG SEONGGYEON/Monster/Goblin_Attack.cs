using System.Collections;
using UnityEngine;

public class Goblin_Attack : MonoBehaviour
{
    public Transform target; // �÷��̾� Ÿ��
    public float walkDistance; // �ȱ� ������ �Ÿ�
    public float attackDistance; // ���� ������ �Ÿ�
    public float attackDistance_3; // ����3 ������ �Ÿ�
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
        mon_ani.applyRootMotion = true; // ��Ʈ ��� ���
        monsterManager = FindObjectOfType<Monster_Queue>();

        monster = GetComponent<Monster_Control>();

        // ���� �ݶ��̴� ���� (��� �ڽ� ��ü�� �ִ� ��� Collider ã��)
        Colliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in Colliders)
        {
            if (col.CompareTag("MovingUI"))
            {
                col.enabled = false;
            }
            else col.enabled = true;
        }


        // ������ �� idle �ڷ�ƾ ����
        StartCoroutine(StartIdle_co());
    }

    private void Update()
    {
        if (monster.isGroggy) return; // �׷α� ���¸� �������� ����
        if (!isStart) return; // ���� ���°� �ƴ� �� �������� ����

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
        if (monster.isGroggy) return; // �׷α� ���¸� �������� ����

        Vector3 direction = (targetPosition - transform.position).normalized;
        direction.y = 0; // ���� ȸ���� ���
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5.0f);
        }
    }

    public void StartAttack()
    {
        if (monster.isGroggy) return; // �׷α� ���¸� �������� ����

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
        yield return new WaitForSeconds(1.0f); // ���� �ִϸ��̼� ��� �ð�
        yield return new WaitForSeconds(coolTime); // ��Ÿ�� ���� ���

        isAttacking = false;
        monsterManager.AttackFinished();
    }

    private IEnumerator AttackType2_co()
    {
        mon_ani.SetTrigger("canAttack_02");
        yield return new WaitForSeconds(1.0f); // ���� �ִϸ��̼� ��� �ð�
        yield return new WaitForSeconds(coolTime); // ��Ÿ�� ���� ���

        isAttacking = false;
        monsterManager.AttackFinished();
    }

    private IEnumerator AttackType3_co()
    {
        isAttacking = true;
        mon_ani.SetTrigger("canAttack_03");
        Debug.Log("�׽�Ʈ ����");

        while (true)
        {
            AnimatorStateInfo stateInfo = mon_ani.GetCurrentAnimatorStateInfo(0);
            float aniTime = stateInfo.normalizedTime;
            float currentDistance = Vector3.Distance(transform.position, target.position);

            // �ִϸ��̼��� ���� ���� �� �Ÿ� ������ üũ
            if (aniTime < 1.0f && currentDistance < 1.0f)
            {
                mon_ani.SetBool("canAttack_03_End", true);
                yield return new WaitForSeconds(2.0f);
                Debug.Log("�ٷ����");
                mon_ani.SetBool("canAttack_03_End", false);
                yield return new WaitForSeconds(coolTime); // ��Ÿ�� ���� ���
                isAttacking = false;
                monsterManager.AttackFinished();
                yield break;
            }

            // �ִϸ��̼��� ���� ��� ���� Ż��
            if (aniTime >= 1.0f && stateInfo.IsName("Monster_Claymore_Ani_Attack_03"))
            {
                //  Debug.Log("�ִϸ��̼� ��: " + stateInfo.IsName("Monster_Claymore_Ani_Attack_03"));
                break;
            }

            yield return null;
        }

        mon_ani.SetBool("canAttack_03_End", true);
        yield return new WaitForSeconds(2.0f);
        Debug.Log("��ٷȴ� ���");
        mon_ani.SetBool("canAttack_03_End", false);
        yield return new WaitForSeconds(coolTime); // ��Ÿ�� ���� ���
        isAttacking = false;
        monsterManager.AttackFinished();
    }

    private IEnumerator StartIdle_co()
    {
        mon_ani.SetBool("Walking", false);
        mon_ani.SetBool("Running", false);
        yield return new WaitForSeconds(5.0f); // idle ���¸� 3�� ���� ����
        isStart = true;
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ���� �޼���
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

    // �ִϸ��̼� �̺�Ʈ���� ȣ���� �޼���
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
        // �ִϸ��̼� ��� �ӵ��� 0���� �����Ͽ� ��� ����
        mon_ani.speed = 0;
        yield return new WaitForSeconds(0.1f);
        // �ִϸ��̼� ��� �ӵ��� 1�� �����Ͽ� �ٽ� ���
        mon_ani.speed = 1;
    }
}
