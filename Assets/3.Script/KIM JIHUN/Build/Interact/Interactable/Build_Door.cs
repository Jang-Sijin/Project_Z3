using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_Door : Build_Interact
{
    [SerializeField] private string nextSceneName;

    public override void Interact()
    {
        Build_UIManager.instance.LoadScene(nextSceneName);
    }
}
