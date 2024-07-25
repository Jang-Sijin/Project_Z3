using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build_Computer : Build_Interact
{    
    [SerializeField] private GameObject stageSelectCanvas;

    private void Start()
    {
        stageSelectCanvas.SetActive(false);
    }

    public override void Interact()
    {
        stageSelectCanvas.SetActive(true);
        if (BelleController.INSTANCE != null)
        {
            BelleController.INSTANCE.CanInput = false;
            BelleController.INSTANCE.UnlockMouse();
            BelleController.INSTANCE.LockCamera();
        }
    }

    public void CloseStageSelect()
    {
        stageSelectCanvas.SetActive(false);
        if (BelleController.INSTANCE != null)
        {
            BelleController.INSTANCE.CanInput = true;
            BelleController.INSTANCE.LockMouse();
            BelleController.INSTANCE.UnlockCamera();
        }
    }

    public void SelectStage(Define.SceneType sceneType)
    {        
        stageSelectCanvas.SetActive(false);
        UIManager.Instance.LoadScene(sceneType);
    }
}
