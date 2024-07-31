using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class InShopUI : MonoBehaviour
{
    [SerializeField] private VideoPlayer videoPlayer;
    public Shop shopScript;
    public Sell sellScript;
    public void OnClickBack()
    {
        /*
         * 메인 씨티 로드 로직 필요
         */

        SceneManagerEx.Instance.LoadScene(Define.SceneType.Home);
    }
    public void OnClickBuy()
    {

    }
    public void OnClickSell()
    {

    }

    public void OpenShopUI()
    {
        UIManager.Instance.MainCityMenuUI.End_bg();
        UIManager.Instance.CloseCityUI();
        if (BelleController.INSTANCE != null)
        {
            BelleController.INSTANCE.CanInput = false;
            BelleController.INSTANCE.LockCamera();
            BelleController.INSTANCE.UnlockMouse();

        }
        gameObject.SetActive(true);
        videoPlayer.targetCamera = Camera.main;
        shopScript.OpenShop();
    }
    public void CloseShopUI()
    {

        if (BelleController.INSTANCE != null)
        {
            UIManager.Instance.OpenCityUI();
            BelleController.INSTANCE.CanInput = true;
            BelleController.INSTANCE.UnlockCamera();
            BelleController.INSTANCE.LockMouse();
        }
        gameObject.SetActive(false);
    }
}
