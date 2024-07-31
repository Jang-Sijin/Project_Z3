using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameClearManager : SingletonBase<InGameClearManager>
{
    public GameObject LastMonster;
    private Build_MonsterModel _monsterModel;

    private void Start()
    {
        _monsterModel = LastMonster.GetComponent<Build_MonsterModel>();
    }

    public void OnMonsterDeath(Build_MonsterModel monsterModel)
    {
        if (monsterModel.gameObject == LastMonster)
        {
            SceneManagerEx.Instance.LoadScene(Define.SceneType.Clear);
        }
    }
}