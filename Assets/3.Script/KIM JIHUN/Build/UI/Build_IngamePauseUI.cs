using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_IngamePauseUI : MonoBehaviour
{

    public void OpenIngamePauseUI()
    {
        PlayerController.INSTANCE.UnlockMouse();
        PlayerController.INSTANCE.LockCamera();
        PlayerController.INSTANCE.CanInput = false;
        Time.timeScale = 0;
        this.gameObject.SetActive(true);
    }

    public void CloseIngamePauseUI()
    {
        PlayerController.INSTANCE.LockMouse();
        PlayerController.INSTANCE.UnlockCamera();
        PlayerController.INSTANCE.CanInput = true;
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    public void QuitStage()
    {
        CloseIngamePauseUI();
        SceneManagerEx.Instance.LoadScene(Define.SceneType.Home);
    }
}
