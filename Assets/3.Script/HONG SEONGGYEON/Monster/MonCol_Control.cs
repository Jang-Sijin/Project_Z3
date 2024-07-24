using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonCol_Control : MonoBehaviour
{
    private Collider[] colliders;
    private bool hasHitPlayer = false; // 플레이어를 때렸는지 여부
    private float hitCooldown = 0.5f; // 쿨다운 시간
    private bool isAttacking = false;  // 공격 중에 무적 판정을 위해 만든 변수
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
                //Debug.Log("활성");
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
              //  Debug.Log("비활성");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasHitPlayer && other.CompareTag("Player"))
        {
            float PlayerHealth = PlayerController.INSTANCE.playerModel.playerStatus.CurrentHealth;
            float PlayerMax= PlayerController.INSTANCE.playerModel.playerStatus.MaxHealth;
            Debug.Log("때림");
            hasHitPlayer = true;
            PlayerHealth -= PlayerMax * 0.1f;
            PlayerController.INSTANCE.playerModel.playerStatus.CurrentHealth = PlayerHealth;
            Debug.Log($"{PlayerHealth}남음");

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
