using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonCol_Control : MonoBehaviour
{
    private Collider[] colliders;
    private bool hasHitPlayer = false; // �÷��̾ ���ȴ��� ����
    private float hitCooldown = 0.5f; // ��ٿ� �ð�
    private bool isAttacking = false;  // ���� �߿� ���� ������ ���� ���� ����
    private MonsterController monster;
    private PlayerController player;

    private void Start()
    {
        colliders = GetComponentsInChildren<Collider>();
        player = FindObjectOfType<PlayerController>();

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("EnemyWeapon"))
                col.enabled = false;
            else
                col.enabled = true;
        }
    }

    public void EnableWeaponCollider()
    {
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("EnemyWeapon"))
            {
                col.enabled = true;
                //Debug.Log("Ȱ��");
            }
        }
    }

    public void DisableWeaponCollider()
    {
        foreach (Collider col in colliders)
        {
            if (col.CompareTag("EnemyWeapon"))
            {
                col.enabled = false;
              //  Debug.Log("��Ȱ��");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasHitPlayer && other.CompareTag("Player"))
        {
            float PlayerHealth = PlayerController.INSTANCE.playerModel.playerStatus.CurrentHealth;
            float PlayerMax= PlayerController.INSTANCE.playerModel.playerStatus.MaxHealth;
            Debug.Log("����");
            hasHitPlayer = true;
            PlayerHealth -= PlayerMax * 0.1f;
            PlayerController.INSTANCE.playerModel.playerStatus.CurrentHealth = PlayerHealth;
            Debug.Log($"{PlayerHealth}����");

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

    private IEnumerator ResetHitCooldown()
    {
        yield return new WaitForSeconds(hitCooldown);
        hasHitPlayer = false;
    }
}
