using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Battle5 : MonoBehaviour
{
    [SerializeField] private List<GameObject> _monsterList;

    private void Start()
    {
        foreach (var item in _monsterList)
        {
            item.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (var item in _monsterList)
            {
                item.SetActive(true);
                item.GetComponent<Navmesh>().SetTarget();
            }
        }
        gameObject.SetActive(false);
    }

}

