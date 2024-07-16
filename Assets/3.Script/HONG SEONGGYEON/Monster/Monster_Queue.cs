using System.Collections.Generic;
using UnityEngine;

public class Monster_Queue : MonoBehaviour
{
    private List<Monster_Rootmotion> monstersList = new List<Monster_Rootmotion>();
    private Monster_Rootmotion currentMonster;

    [SerializeField] private Transform playerTransform; // 플레이어의 Transform

    public void RegisterMonster(Monster_Rootmotion monster)
    {
        monstersList.Add(monster);
    }

    public void RequestAttack(Monster_Rootmotion monster)
    {
        if (monster.monster.isGroggy) return; // 그로기 상태인 몬스터는 공격하지 않음

        if (currentMonster == null)
        {
            currentMonster = GetClosestMonster();
            if (currentMonster != null)
            {
                currentMonster.StartAttack();
            }
        }
        else if (currentMonster == monster)
        {
            currentMonster.StartAttack();
        }
        else
        {
            if (!monstersList.Contains(monster))
            {
                monstersList.Add(monster);
            }
        }
    }

    public void AttackFinished()
    {
        currentMonster = GetClosestMonster();
        if (currentMonster != null)
        {
            currentMonster.StartAttack();
        }
    }

    private Monster_Rootmotion GetClosestMonster()
    {
        Monster_Rootmotion closestMonster = null;
        float closestDistance = float.MaxValue;

        foreach (Monster_Rootmotion monster in monstersList)
        {
            if (monster == currentMonster || monster.monster.isGroggy)
                continue;

            float distance = Vector3.Distance(monster.transform.position, playerTransform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMonster = monster;
            }
        }

        return closestMonster;
    }
}
