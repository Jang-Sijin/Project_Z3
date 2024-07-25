using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Build_Computer : Build_Interact
{  

    [SerializeField] private GameObject stageSelectCanvas;
    [SerializeField] private GameObject nameTag;

    private Define.SceneType sceneType;

    private void Start()
    {
        stageSelectCanvas.SetActive(false);
        nameTag.SetActive(false);
    }

    public override void Interact()
    {
        sceneType = Define.SceneType.Max;
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
        sceneType = Define.SceneType.Max;
        stageSelectCanvas.SetActive(false);
        if (BelleController.INSTANCE != null)
        {
            BelleController.INSTANCE.CanInput = true;
            BelleController.INSTANCE.LockMouse();
            BelleController.INSTANCE.UnlockCamera();
        }
    }

    public void SelectStage(int stageIndex)
    {
        switch (stageIndex)
        {
            case 1:
                sceneType = Define.SceneType.Battle1;
                break;
            case 2:
                sceneType = Define.SceneType.Battle2;
                break;
            case 3:
                sceneType = Define.SceneType.Battle3;
                break;
            case 4:
                sceneType = Define.SceneType.Battle4;
                break;
            case 5:
                sceneType = Define.SceneType.Battle5;
                break;
            default:
                return;
        }
    }

    public void ComfirmSelect()
    {
        if (sceneType == Define.SceneType.Max)
            return;
        stageSelectCanvas.SetActive(false);
        SceneManagerEx.Instance.LoadScene(sceneType);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            nameTag.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            nameTag.SetActive(false);
        }
    }
}