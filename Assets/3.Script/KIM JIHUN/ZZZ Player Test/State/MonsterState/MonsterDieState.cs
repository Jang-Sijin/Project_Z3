using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MonsterDieState : EnemyStateBase
{
    private static System.Random random = new System.Random();

    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("Die");
    }
    public override void Update()
    {
        base.Update();
        if (IsAnimationEnd())
        {
            // 몬스터 경험치 획득
            GameManager.Instance.StageTotalExp += monsterController.monsterModel.monsterStatus.Exp;
            // 몬스터 아이템 획득: 30% 확률
            bool gotItem = TryGetItem();
            if (gotItem) 
            {
                GameManager.Instance.StageGetItemList.Add(GameManager.Instance.SelectRandomDropItem());
            }

            monsterController.monsterModel.MonsterDie();
            return;
        }
    }

    private bool TryGetItem()
    {
        int chance = random.Next(0, 100); // 0부터 99까지의 숫자 중 하나를 무작위로 생성
        return chance < 30; // 0부터 29까지는 아이템을 획득 (30% 확률)
    }
}
