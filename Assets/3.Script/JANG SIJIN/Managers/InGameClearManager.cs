using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameClearManager : SingletonBase<InGameClearManager>
{
    public GameObject LastMonster;
    private Build_MonsterModel _monsterModel;

    public GameObject gameClearUI; // ���� Ŭ���� UI ������Ʈ

    private void Start()
    {
        _monsterModel = LastMonster.GetComponent<Build_MonsterModel>();
    }

    public void OnMonsterDeath(Build_MonsterModel monsterModel)
    {
        if (monsterModel.gameObject == LastMonster)
        {
            // ������ ���Ͱ� ������� �� ���� Ŭ���� UI ǥ��
            gameClearUI.SetActive(true);
        }
    }
}