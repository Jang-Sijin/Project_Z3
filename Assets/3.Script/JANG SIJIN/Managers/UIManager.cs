using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonBase<UIManager>
{
    /*
    * UI 메니저에서 씬 로드시 알맞은 UI를 Load합니다
    * Ingame씬에서 InGameUI를 불러옴
    * Intro씬에서 IntroUI를 불러옴
    * MainMenu씬에서 MainMenuUI를 불러옴
    */

    [SerializeField] private Build_IntroUI _introUI;
    [SerializeField] private MainCityUI _mainCityUI;
    [SerializeField] private Build_IngameUI _inGameUI;
    [SerializeField] private Build_OptionMenuUI _pauseMenuUI;
    [SerializeField] private Build_CommonLoadingUI _commonLoadingUI;
    [SerializeField] private Build_MainCityUI _mainCityMenuUI;
    [SerializeField] private Build_IngamePauseUI _ingamePauseUI;
    [SerializeField] private Build_InventoryUI _inventoryUI;

    public Build_IntroUI IntroUI => _introUI;
    public MainCityUI MainCityUI => _mainCityUI;
    public Build_IngameUI InGameUI => _inGameUI;
    public Build_OptionMenuUI PauseMenuUI => _pauseMenuUI;
    public Build_CommonLoadingUI CommonLoadingUI => _commonLoadingUI;
    public Build_MainCityUI MainCityMenuUI => _mainCityMenuUI;
    public Build_IngamePauseUI IngamePauseUI => _ingamePauseUI;
    public Build_InventoryUI InventoryUI => _inventoryUI;


    [HideInInspector] public bool isCloseOrOpen = false;
    [HideInInspector] public bool isPause = false;
    //KimJihun
    private bool isMainCity;

    //JangSijin
    private bool isMainMenu = true; // 메인 메뉴 씬은 가장 먼저 호출되기 때문에 true로 설정함

    public void OptionUIOpenClose() // 옵션 메뉴UI 를 열고 닫는 메소드입니다.
    {
        if (!isCloseOrOpen)
        {
            if (isPause)
            {
                _pauseMenuUI.OnClickCloseMainUI();

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
                if (_mainCityMenuUI.isOpened)
                {
                    _mainCityMenuUI.ToggleMainCityUI();
                }
                _pauseMenuUI.gameObject.SetActive(true);
                StartCoroutine(_pauseMenuUI.CallPauseMenu_co());

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
    /// Player의 조작을 Lock
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
    /// Player의 조작을 Unlock
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
        _introUI.gameObject.SetActive(true);
    }
    public void CloseIntroUI()
    {
        _introUI.gameObject.SetActive(false);
    }

    public void OpenIngameUI()
    {
        _inGameUI.gameObject.SetActive(true);
        InGameUI.SetIngameUI();
    }

    public void CloseIngameUI()
    {
        _inGameUI.gameObject.SetActive(false);
    }

    public void OpenCityUI()
    {
        _mainCityUI.gameObject.SetActive(true);
    }

    public void CloseCityUI()
    {
        _mainCityUI.gameObject.SetActive(false);
    }

    public void OnClickGameOff()
    {
        Application.Quit();
    }


    public void OpenInventoryUI()
    {
        _inventoryUI.OpenInventoryUI();
    }
    public void CloseInventoryUI()
    {
        _inventoryUI.gameObject.SetActive(false);
    }
    public void CloseAllUI()
    {
        CloseCityUI();
        CloseIngameUI();
        CloseIntroUI();
        CloseInventoryUI();
    }
}

// [레거시]
//public void Creat_UI(WhichUI ui) // UI 생성 메소드 입니다.
//{
//    GameObject temp_ob;
//    switch (ui)
//    {
//        case WhichUI.introUI:
//            if (introUI != null)
//            {
//                Debug.Log($"{ui}는 이미 존재합니다.");
//                return;
//            }

//            temp_ob = Instantiate(introUI_prefab);
//            introUI = temp_ob.GetComponent<IntroUI>();
//            break;

//        case WhichUI.mainCityUI:
//            if (mainCityUI != null)
//            {
//                Debug.Log($"{ui}는 이미 존재합니다.");
//                return;
//            }

//            temp_ob = Instantiate(mainCityUI_prefab);
//            mainCityUI = temp_ob.GetComponent<MainCityUI>();
//            break;

//        case WhichUI.pauseMenuUI:
//            if (pauseMenuUI != null)
//            {
//                Debug.Log($"{ui}는 이미 존재합니다.");
//                return;
//            }

//            temp_ob = Instantiate(pauseMenuUI_prefab);
//            pauseMenuUI = temp_ob.GetComponent<PauseMenuUI>();
//            break;

//        case WhichUI.inGameUI:
//            if (inGameUI != null)
//            {
//                Debug.Log($"{ui}는 이미 존재합니다.");
//                Destroy(inGameUI.gameObject);
//            }

//            temp_ob = Instantiate(inGameUI_prefab);
//            inGameUI = temp_ob.GetComponent<InGameUI>();
//            break;
//    }
//}   