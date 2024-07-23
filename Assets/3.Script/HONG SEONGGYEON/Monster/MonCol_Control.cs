using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonCol_Control : MonoBehaviour
{
    private Collider[] Colliders;
    private bool hasHitPlayer = false; // �÷��̾ ���ȴ��� ����
    private float hitCooldown = 0.5f; // ��ٿ� �ð�
    private bool isAttacking = false;  //  �����߿�  ���������ϱ� ���� ���� ����
    private MonsterController monster;
    private PlayerController player;

    private void Start()
    {
        Colliders = GetComponentsInChildren<Collider>();

        foreach (Collider col in Colliders)
        {
            if (col.CompareTag("EnemyWeapon"))
                col.enabled = false;
            else
                col.enabled = true;
        }
    }

    public void EnableWeaponCollider()
    {
        foreach (Collider col in Colliders)
        {
            if (col.CompareTag("EnemyWeapon"))
            {
                col.enabled = true;
                // Debug.Log("Ȱ��");
            }
        }
    }

    public void DisableWeaponCollider()
    {
        foreach (Collider col in Colliders)
        {
            if (col.CompareTag("EnemyWeapon"))
            {
                col.enabled = false;
                // Debug.Log("��Ȱ��");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasHitPlayer && other.CompareTag("Player"))
        {
            Debug.Log("����");
            hasHitPlayer = true;

            StartCoroutine(ResetHitCooldown());
        }
    }


    public void AttackingDisable()
    {
        isAttacking = true;
    }

    public void AttackingEnable()
    {
        isAttacking = false;
    }



    //  private void Update()
    //  {
    //     if (monster.monsterModel.state == MonsterState.AttackType_01 || monster.monsterModel.state == MonsterState.AttackType_02)
    //     {
    //         EnableWeaponCollider();
    //     }
    //     else
    //     {
    //         DisableWeaponCollider();
    //     }
    //  }



    private IEnumerator ResetHitCooldown()
    {
        yield return new WaitForSeconds(hitCooldown);
        hasHitPlayer = false;
    }


}
