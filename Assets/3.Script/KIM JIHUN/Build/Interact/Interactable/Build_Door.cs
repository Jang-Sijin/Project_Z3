using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Door : Build_Interact
{
    [SerializeField] private Define.SceneType sceneType;
    [SerializeField] private GameObject nameTag;


    private void Start()
    {
        if (nameTag != null)
        {
            nameTag.SetActive(false);
        }
    }

    public override void Interact()
    {
        UIManager.Instance.LoadScene(sceneType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (nameTag != null)
        {
            if (other.CompareTag("Player"))
            {
                nameTag.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (nameTag != null)
        {
            if (other.CompareTag("Player"))
            {
                nameTag.SetActive(false);
            }
        }
    }
}
