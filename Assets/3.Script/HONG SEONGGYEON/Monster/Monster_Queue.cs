using System.Collections.Generic;
using UnityEngine;

public class Monster_Queue : MonoBehaviour
{
    private List<Monster_Rootmotion> monstersList = new List<Monster_Rootmotion>();
    private Monster_Rootmotion currentMonster;  // 공격중인 몬스터

    [SerializeField] private Transform playerTransform; // 플레이어의 Transform

    public void RegisterMonster(Monster_Rootmotion monster)
    {
        monstersList.Add(monster);  
    }

    public void RequestAttack(Monster_Rootmotion monster)
    {
        if (monster.monster.isGroggy) return; // 그로기 상태인 몬스터는 공격하지 않음

        if (currentMonster == null)  // 공격중인 몬스터가 없으면
        {
            currentMonster = GetClosestMonster();  // 가장 가까운 놈 불러와
            if (currentMonster != null)
            {
                currentMonster.StartAttack();  // 공격시켜
            }
        }
        else if (currentMonster == monster)  // 공격중인 몬스터
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
        Monster_Rootmotion closestMonster = null; //빈공간 만들고
        float closestDistance = float.MaxValue;  // 거리 저장할 변수

        foreach (Monster_Rootmotion monster in monstersList)
        {
            if (monster == currentMonster || monster.monster.isGroggy) // 그로기, 공격중은 빼
                continue;

            float distance = Vector3.Distance(monster.transform.position, playerTransform.position);
            if (distance < closestDistance)
            {
                closestDistance = distance;
                closestMonster = monster;  // 거리 계속 갱신
            }
        }

        return closestMonster;
    }
}