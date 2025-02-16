using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : SingletonBase<UIManager>
{
    /*
    * UI ????????? ?? ?��?? ????? UI?? Load????
    * Ingame?????? InGameUI?? ?????
    * Intro?????? IntroUI?? ?????
    * MainMenu?????? MainMenuUI?? ?????
    */

    [SerializeField] private Build_IntroUI _introUI;
    [SerializeField] private MainCityUI _mainCityUI;
    [SerializeField] private Build_IngameUI _inGameUI;
    [SerializeField] private Build_OptionMenuUI _pauseMenuUI;
    [SerializeField] private Build_CommonLoadingUI _commonLoadingUI;
    [SerializeField] private Build_MainCityUI _mainCityMenuUI;
    [SerializeField] private Build_IngamePauseUI _ingamePauseUI;
    [SerializeField] private Build_InventoryUI _inventoryUI;
    [SerializeField] private Build_AgentSelectUI _agentSelectUI;
    [SerializeField] private Build_WeaponUI _weaponUI;
    [SerializeField] private Build_CharacterStatusUI _characterStatusUI;
    [SerializeField] private InShopUI shopUI;
    [SerializeField] private UI_Loading _uiLoading;

    public Build_IntroUI IntroUI => _introUI;
    public MainCityUI MainCityUI => _mainCityUI;
    public Build_IngameUI InGameUI => _inGameUI;
    public Build_OptionMenuUI PauseMenuUI => _pauseMenuUI;
    public Build_CommonLoadingUI CommonLoadingUI => _commonLoadingUI;
    public Build_MainCityUI MainCityMenuUI => _mainCityMenuUI;
    public Build_IngamePauseUI IngamePauseUI => _ingamePauseUI;
    public Build_InventoryUI InventoryUI => _inventoryUI;
    public Build_AgentSelectUI AgentSelectUI => _agentSelectUI;
    public Build_WeaponUI WeaponUI => _weaponUI;
    public Build_CharacterStatusUI CharacterStatusUI => _characterStatusUI;
    public InShopUI ShopUI => shopUI;
    public UI_Loading UI_Loading => _uiLoading;


    [HideInInspector] public bool isCloseOrOpen = false;
    [HideInInspector] public bool isPause = false;
    //KimJihun
    private bool isMainCity;

    //JangSijin
    private bool isMainMenu = true; // ???? ??? ???? ???? ???? ????? ?????? true?? ??????

    public void OptionUIOpenClose() // ??? ???UI ?? ???? ??? ????????.
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
    /// Player?? ?????? Lock
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
    /// Player?? ?????? Unlock
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
        Debug.Log("OpenCityUI");
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

    public void OpenAgentSelectUI()
    {

        _agentSelectUI.OpenAgentSelectUI();
    }

    public void CloseAgentSelectUI()
    {

        _agentSelectUI.CloseAgentSelectUI();
    }
    public void OpenInventoryUI()
    {
        _inventoryUI.OpenInventoryUI();
    }
    public void CloseInventoryUI()
    {
        _inventoryUI.gameObject.SetActive(false);
    }

    public void OpenWeaponUI()
    {
        _weaponUI.OpenWeaponUI(_agentSelectUI.SelectedCharacter);
    }

    public void CloseWeaponUI()
    {
        _weaponUI.CloseWeaponUI();
    }
    public void OpenShopUI()
    {
        shopUI.OpenShopUI();
    }
    public void CloseShopUI()
    {
        shopUI.CloseShopUI();
        //OpenCityUI();
    }

    public void CloseAllUI()
    {
        CloseCityUI();
        CloseIngameUI();
        CloseIntroUI();
        CloseInventoryUI();
        CloseAgentSelectUI();
        CloseShopUI();
    }
}

// [?????]
//public void Creat_UI(WhichUI ui) // UI ???? ???? ????.
//{
//    GameObject temp_ob;
//    switch (ui)
//    {
//        case WhichUI.introUI:
//            if (introUI != null)
//            {
//                Debug.Log($"{ui}?? ??? ????????.");
//                return;
//            }

//            temp_ob = Instantiate(introUI_prefab);
//            introUI = temp_ob.GetComponent<IntroUI>();
//            break;

//        case WhichUI.mainCityUI:
//            if (mainCityUI != null)
//            {
//                Debug.Log($"{ui}?? ??? ????????.");
//                return;
//            }

//            temp_ob = Instantiate(mainCityUI_prefab);
//            mainCityUI = temp_ob.GetComponent<MainCityUI>();
//            break;

//        case WhichUI.pauseMenuUI:
//            if (pauseMenuUI != null)
//            {
//                Debug.Log($"{ui}?? ??? ????????.");
//                return;
//            }

//            temp_ob = Instantiate(pauseMenuUI_prefab);
//            pauseMenuUI = temp_ob.GetComponent<PauseMenuUI>();
//            break;

//        case WhichUI.inGameUI:
//            if (inGameUI != null)
//            {
//                Debug.Log($"{ui}?? ??? ????????.");
//                Destroy(inGameUI.gameObject);
//            }

//            temp_ob = Instantiate(inGameUI_prefab);
//            inGameUI = temp_ob.GetComponent<InGameUI>();
//            break;
//    }
//}   