using ExternalPropertyAttributes;
using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class Stage3MonsterSpawnManager : MonoBehaviour
{
    public GameObject[] MonsterSpawnObjects;

    private BoxCollider[] _boxColliders;
    private MonsterSpawnData[] _monsterSpawnData;

    private void Start()
    {
        Init();
        SetupTriggers();
    }

    private void Init()
    {
        _boxColliders = new BoxCollider[MonsterSpawnObjects.Length];
        _monsterSpawnData = new MonsterSpawnData[MonsterSpawnObjects.Length];

        for (int i = 0; i < MonsterSpawnObjects.Length; i++)
        {
            _boxColliders[i] = MonsterSpawnObjects[i].GetComponent<BoxCollider>();
            _monsterSpawnData[i] = MonsterSpawnObjects[i].GetComponent<MonsterSpawnData>();


            // 몬스터의 오브젝트가 SetActive true일 경우, false로 초기 세팅을 진행한다.
            var monsterData = _monsterSpawnData[i];
            if (monsterData != null)
            {
                foreach (var monsterPrefab in monsterData.MonsterPrefab)
                {
                    if (monsterPrefab != null)
                    {
                        monsterPrefab.SetActive(false);
                    }
                }
            }
        }
    }
    private void SetupTriggers()
    {
        for (int i = 0; i < _boxColliders.Length; i++)
        {
            var boxCollider = _boxColliders[i];
            var monsterData = _monsterSpawnData[i];

            if (boxCollider != null && monsterData != null)
            {
                // Observe the trigger event
                boxCollider.OnTriggerEnterAsObservable()
                    .Where(collider => collider.CompareTag("Player")) // Check if the collider is the player
                    .Subscribe(_ =>
                    {
                        ActivateMonsters(monsterData);
                    })
                    .AddTo(this); // Ensure proper cleanup when this MonoBehaviour is destroyed
            }
        }
    }

    private void ActivateMonsters(MonsterSpawnData monsterData)
    {
        foreach (var monsterPrefab in monsterData.MonsterPrefab)
        {
            if (monsterPrefab != null)
            {
                monsterPrefab.SetActive(true);
            }
        }
    }
}