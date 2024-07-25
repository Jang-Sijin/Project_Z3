using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonBase<UIManager>
{
    /*
    * UI �޴������� �� �ε�� �˸��� UI�� Load�մϴ�
    * Ingame������ InGameUI�� �ҷ���
    * Intro������ IntroUI�� �ҷ���
    * MainMenu������ MainMenuUI�� �ҷ���
    */

    [SerializeField] private Build_IntroUI introUI;
    [SerializeField] private MainCityUI mainCityUI;
    [SerializeField] private Build_IngameUI inGameUI;
    [SerializeField] private Build_OptionMenuUI pauseMenuUI;
    [SerializeField] private Build_CommonLoadingUI commonLoadingUI;

    public Build_IntroUI IntroUI => introUI;
    public MainCityUI MainCityUI => mainCityUI;
    public Build_IngameUI InGameUI => inGameUI;
    public Build_OptionMenuUI PauseMenuUI => pauseMenuUI;
    public Build_CommonLoadingUI CommonLoadingUI => commonLoadingUI;


    [HideInInspector] public bool isCloseOrOpen = false;
    [HideInInspector] public bool isPause = false;
    //KimJihun
    private bool isMainCity;

    //JangSijin
    private bool isMainMenu = true; // ���� �޴� ���� ���� ���� ȣ��Ǳ� ������ true�� ������

    public void OptionUIOpenClose() // �ɼ� �޴�UI �� ���� �ݴ� �޼ҵ��Դϴ�.
    {
        if (!isCloseOrOpen)
        {
            if (isPause)
            {
                pauseMenuUI.OnClickCloseMainUI();

                if (BelleController.INSTANCE != null)
                {
                    BelleController.INSTANCE.LockMouse();
                    BelleController.INSTANCE.CanInput = true;
                }
                else if (PlayerController.INSTANCE != null)
                {
                    PlayerController.INSTANCE.LockMouse();
                    PlayerController.INSTANCE.CanInput = true;
                }
            }
            else
            {
                pauseMenuUI.gameObject.SetActive(true);
                StartCoroutine(pauseMenuUI.CallPauseMenu_co());

                Debug.Log("Open Pause Menu");
                if (BelleController.INSTANCE != null)
                {
                    BelleController.INSTANCE.UnlockMouse();
                    BelleController.INSTANCE.CanInput = false;
                }
                else if (PlayerController.INSTANCE != null)
                {
                    PlayerController.INSTANCE.UnlockMouse();
                    PlayerController.INSTANCE.CanInput = false;
                }
            }
        }
    }

    /// <summary>
    /// Player�� ������ Lock
    /// </summary>
    public void LockPlayer()
    {
        if (BelleController.INSTANCE != null)
        {
            BelleController.INSTANCE.UnlockMouse();
            BelleController.INSTANCE.CanInput = false;
        }
        else if (PlayerController.INSTANCE != null)
        {
            PlayerController.INSTANCE.UnlockMouse();
            PlayerController.INSTANCE.CanInput = false;
        }
    }

    /// <summary>
    /// Player�� ������ Unlock
    /// </summary>
    public void UnlockPlayer()
    {
        if (BelleController.INSTANCE != null)
        {
            BelleController.INSTANCE.LockMouse();
            BelleController.INSTANCE.CanInput = true;
        }
        else if (PlayerController.INSTANCE != null)
        {
            PlayerController.INSTANCE.LockMouse();
            PlayerController.INSTANCE.CanInput = true;
        }
    }


    public void OpenIntroUI()
    {
        introUI.gameObject.SetActive(true);
    }
    public void CloseIntroUI()
    {
        introUI.gameObject.SetActive(false);
    }

    public void OpenIngameUI()
    {
        inGameUI.gameObject.SetActive(true);
    }

    public void CloseIngameUI()
    {
        inGameUI.gameObject.SetActive(false);
    }

    public void OpenCityUI()
    {
        mainCityUI.gameObject.SetActive(true);
    }

    public void CloseCityUI()
    {
        mainCityUI.gameObject.SetActive(false);
    }

    public void OnClickGameOff()
    {
        Application.Quit();
    }

    public void CloseAllUI()
    {
        CloseCityUI();
        CloseIngameUI();
        CloseIntroUI();
    }
}

// [���Ž�]
//public void Creat_UI(WhichUI ui) // UI ���� �޼ҵ� �Դϴ�.
//{
//    GameObject temp_ob;
//    switch (ui)
//    {
//        case WhichUI.introUI:
//            if (introUI != null)
//            {
//                Debug.Log($"{ui}�� �̹� �����մϴ�.");
//                return;
//            }

//            temp_ob = Instantiate(introUI_prefab);
//            introUI = temp_ob.GetComponent<IntroUI>();
//            break;

//        case WhichUI.mainCityUI:
//            if (mainCityUI != null)
//            {
//                Debug.Log($"{ui}�� �̹� �����մϴ�.");
//                return;
//            }

//            temp_ob = Instantiate(mainCityUI_prefab);
//            mainCityUI = temp_ob.GetComponent<MainCityUI>();
//            break;

//        case WhichUI.pauseMenuUI:
//            if (pauseMenuUI != null)
//            {
//                Debug.Log($"{ui}�� �̹� �����մϴ�.");
//                return;
//            }

//            temp_ob = Instantiate(pauseMenuUI_prefab);
//            pauseMenuUI = temp_ob.GetComponent<PauseMenuUI>();
//            break;

//        case WhichUI.inGameUI:
//            if (inGameUI != null)
//            {
//                Debug.Log($"{ui}�� �̹� �����մϴ�.");
//                Destroy(inGameUI.gameObject);
//            }

//            temp_ob = Instantiate(inGameUI_prefab);
//            inGameUI = temp_ob.GetComponent<InGameUI>();
//            break;
//    }
//}   