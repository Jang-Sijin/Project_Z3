using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemy : MonoBehaviour
{
    EnemyUIController enemyUIController;
    EnemyHitFontSpawner enemyHitFontSpawner;
    public struct TempEnemyData
    {
        public float maxHP;
        public float curHP;
        public float maxSP;
        public float curSP;
    };

    TempEnemyData tempEnemyData;

    private void Awake()
    {
        enemyUIController = GetComponentInChildren<EnemyUIController>();
        enemyHitFontSpawner = GetComponentInChildren<EnemyHitFontSpawner>();

        tempEnemyData = new TempEnemyData();

        tempEnemyData.maxHP = 200f;
        tempEnemyData.curHP = 200f;
        tempEnemyData.maxSP = 1.0f;
        tempEnemyData.curSP = 0f;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log(20 + "의 데미지");
            tempEnemyData.curHP -= 20f;
            tempEnemyData.curSP += 0.1f;
            enemyHitFontSpawner.poolingFont();
            enemyUIController.RefreshHealth(tempEnemyData.curHP, tempEnemyData.maxHP);
            enemyUIController.RefreshStun(tempEnemyData.curSP, tempEnemyData.maxSP);
        }
    }
}
