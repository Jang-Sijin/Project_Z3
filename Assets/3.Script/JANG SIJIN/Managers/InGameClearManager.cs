using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameClearManager : SingletonBase<InGameClearManager>
{
    public GameObject LastMonster;
    private Build_MonsterModel _monsterModel;

    public GameObject gameClearUI; // 게임 클리어 UI 오브젝트

    private void Start()
    {
        _monsterModel = LastMonster.GetComponent<Build_MonsterModel>();
    }

    public void OnMonsterDeath(Build_MonsterModel monsterModel)
    {
        if (monsterModel.gameObject == LastMonster)
        {
            // 마지막 몬스터가 사망했을 때 게임 클리어 UI 표시
            gameClearUI.SetActive(true);
        }
    }
}