using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Control : MonoBehaviour
{ 
    private Monster_Rootmotion rootmon;
    private Monster_Control monster;
    [SerializeField] public int Attack;
    [SerializeField] public int GroggyPoint;

    private void Start()
    {
        monster = FindAnyObjectByType<Monster_Control>();
        rootmon = FindAnyObjectByType<Monster_Rootmotion>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            monster.mon_GP -= GroggyPoint;
            monster.mon_HP -= Attack;
            Debug.Log($"{monster.mon_GP}����: �׷α�");
            Debug.Log($"{monster.mon_HP}����: ��");
            StartCoroutine(rootmon.HitReaction());
        }
    }
}
