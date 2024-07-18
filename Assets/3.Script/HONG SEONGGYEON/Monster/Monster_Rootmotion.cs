using System.Collections;
using UnityEngine;

public class Monster_Rootmotion : MonoBehaviour
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
    private Collider[] weaponColliders;
    private Rigidbody rigid;
    private Vector3 lastPosition; // ������ ��ġ ����

    [SerializeField] private float coolTime;

    private void Start()
    {
        mon_ani = GetComponent<Animator>();
        rigid = GetComponent<Rigidbody>();
   //     mon_ani.applyRootMotion = true; // ��Ʈ ��� ���
        monsterManager = FindObjectOfType<Monster_Queue>();
        monsterManager.RegisterMonster(this);

        monster = GetComponent<Monster_Control>();

        // ���� �ݶ��̴� ���� (��� �ڽ� ��ü�� �ִ� ��� Collider ã��)
        weaponColliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in weaponColliders)
        {
            if (col.CompareTag("MovingUI")) col.enabled = false; // �ʿ信 ���� �ݶ��̴� ��Ȱ��ȭ
            else col.enabled = true;
        }

        // ������ �� idle �ڷ�ƾ ����
        StartCoroutine(StartIdle_co());
    }

    private void Update()
    {
        rigid.velocity = Vector3.zero;
        rigid.angularVelocity = Vector3.zero;

        if (monster.isGroggy) return; // �׷α� ���¸� �������� ����
        if (!isStart) return; // ���� ���°� �ƴ� �� �������� ����

        float distance = Vector3.Distance(transform.position, target.position);
        RotateTowards(target.position);
        if (distance < attackDistance_3 && !isAttacking)
        {
            StartCoroutine(AttackType3_co());
        }
        else if (distance < walkDistance)
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lastPosition = rigid.position;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            rigid.MovePosition(lastPosition); // �浹 ���� �� ������ ��ġ�� ������ ��ġ�� �����Ͽ� �и��� �ʵ��� ��
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            lastPosition = rigid.position;
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ���� �޼���
    public void EnableWeaponCollider()
    {
        foreach (Collider col in weaponColliders)
        {
            col.enabled = true;
        }
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ���� �޼���
    public void DisableWeaponCollider()
    {
        foreach (Collider col in weaponColliders)
        {
            col.enabled = false;
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
