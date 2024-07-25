using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Door : Build_Interact
{
    [SerializeField] private Define.SceneType sceneType;

    public override void Interact()
    {
        UIManager.Instance.LoadScene(sceneType);
    }
}
