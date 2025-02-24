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
            // ���� ����ġ ȹ��
            GameManager.Instance.StageTotalExp += monsterController.monsterModel.monsterStatus.Exp;
            // ���� ������ ȹ��: 30% Ȯ��
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
        int chance = random.Next(0, 100); // 0���� 99������ ���� �� �ϳ��� �������� ����
        return chance < 30; // 0���� 29������ �������� ȹ�� (30% Ȯ��)
    }
}
