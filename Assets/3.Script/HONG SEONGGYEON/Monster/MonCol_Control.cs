using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonCol_Control : MonoBehaviour
{
    private Collider[] Colliders;
    private bool hasHitPlayer = false; // 플레이어를 때렸는지 여부
    private float hitCooldown = 0.5f; // 쿨다운 시간
    private bool isAttacking = false;  //  공격중에  무적판정하기 위해 만든 변수
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
                // Debug.Log("활성");
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
                // Debug.Log("비활성");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasHitPlayer && other.CompareTag("Player"))
        {
            Debug.Log("때림");
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
