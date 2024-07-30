using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Battle5 : MonoBehaviour
{
    [SerializeField] private List<GameObject> _monsterList;
    [SerializeField] private GameObject target;


    private void OnTriggerEnter(Collider other)
    {
        //몬스터들의 타겟 설정
        if (other.CompareTag("Player"))
        {
            foreach (var item in _monsterList)
            {
                item.SetActive(true);
                item.GetComponent<Build_NavMesh>().SetTarget(target);
            }
        }
        gameObject.SetActive(false);
        //배리어 설정
    }
}

