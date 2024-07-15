using System.Collections;
using UnityEngine;

public class Monster_Rootmotion : MonoBehaviour
{
    public Transform target; // �÷��̾� Ÿ��
    public float followDistance = 10.0f; // ���� �Ÿ�
    public float walkDistance = 3.0f; // �ȱ� ������ �Ÿ�
    private Animator mon_ani;
    private bool isAttacking = false;
    private Monster_Queue monsterManager;
    public Monster_Control monster;
    private Collider Weaponcol;

    [SerializeField] private float coolTime = 5f;


    //���� ���ݸ�� ���϶��� ��Ȱ���س�
    private void Start()
    {
        mon_ani = GetComponent<Animator>();
        mon_ani.applyRootMotion = true; // ��Ʈ ��� ���
        monsterManager = FindObjectOfType<Monster_Queue>();
        monsterManager.RegisterMonster(this);

        monster = GetComponent<Monster_Control>();
        if (monster == null)
        {
            Debug.LogError("Monster_Control component not found!");
        }

        // ���� �ݶ��̴� ���� (Bip001 L Forearm�� �ִ� BoxCollider ã��)
        Weaponcol = GetComponentInChildren<Collider>();
        if (Weaponcol == null)
        {
            Debug.LogError("Weapon Collider not found!");
        }
        Weaponcol.enabled = false;
    }

    private void Update()
    {
        if (monster.isGroggy) return; // �׷α� ���¸� �������� ����

        float distance = Vector3.Distance(transform.position, target.position);

        if (distance > followDistance)  // �÷��̾� ���� �̵��ϴ� ����
        {
            RotateTowards(target.position);
            mon_ani.SetBool("Running", true);
            mon_ani.SetBool("isWalk", false);
        }
        else if (distance > walkDistance) // �ȴ� ����
        {
            RotateTowards(target.position);
            mon_ani.SetBool("Running", false);
            mon_ani.SetBool("isWalk", true);
        }
        else  // ���� ����
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
        int pattern = Random.Range(0, 4);
        RotateTowards(target.position); // Ÿ�� ������ ȸ��
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
        yield return new WaitForSeconds(1.0f); // ���� �ִϸ��̼� ��� �ð�

        yield return new WaitForSeconds(coolTime); // ��Ÿ�� ���� ���
        isAttacking = false;
        monsterManager.AttackFinished();
    }

    private IEnumerator AttackType2_co()
    {
        RotateTowards(target.position); // Ÿ�� ������ ȸ��
        mon_ani.SetTrigger("canAttack_02");
        yield return new WaitForSeconds(1.0f); // ���� �ִϸ��̼� ��� �ð�

        yield return new WaitForSeconds(coolTime); // ��Ÿ�� ���� ���
        isAttacking = false;
        monsterManager.AttackFinished();
    }

    private IEnumerator AttackType3_co()
    {
        RotateTowards(target.position); // Ÿ�� ������ ȸ��
        mon_ani.SetTrigger("canAttack_03");
        yield return new WaitForSeconds(1.0f); // ���� �ִϸ��̼� ��� �ð�

        yield return new WaitForSeconds(coolTime); // ��Ÿ�� ���� ���
        isAttacking = false;
        monsterManager.AttackFinished();
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ���� �޼���
    public void EnableWeaponCollider()
    {
        Weaponcol.enabled = true;
    }

    // �ִϸ��̼� �̺�Ʈ���� ȣ���� �޼���
    public void DisableWeaponCollider()
    {
        Weaponcol.enabled = false;
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
