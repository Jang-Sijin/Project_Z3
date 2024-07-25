//using System.Collections;
//using System.Collections.Generic;
//using System.Runtime.CompilerServices;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.SceneManagement;
//using UnityEngine.UI;

//public enum WhichUI
//{
//    introUI = 0,
//    mainCityUI = 1,
//    inGameUI = 2,
//    pauseMenuUI = 3,
//    commonLoadingUI =4
//};
///*
// * UI 메니저에서 모든 UI를 생성, 제거하는 메소드가 있습니다.
// */

//public class UIManager : SingletonBase<UIManager>
//{
//    /*
//    * UI 메니저에서 씬 로드시 알맞은 UI를 Load합니다
//    * Ingame씬에서 InGameUI를 불러옴
//    * Intro씬에서 IntroUI를 불러옴
//    * MainMenu씬에서 MainMenuUI를 불러옴
//    */

//    [SerializeField] private GameObject introUI_prefab;
//    private IntroUI introUI;
//    public IntroUI IntroUI => introUI;

//    [SerializeField] private GameObject mainCityUI_prefab;
//    private MainCityUI mainCityUI;

//    [SerializeField] private GameObject inGameUI_prefab;
//    private InGameUI inGameUI;

//    //**************************************************
//    [SerializeField]private GameObject pauseMenuUI_prefab;
//    [SerializeField]public PauseMenuUI pauseMenuUI;

//    public bool isCloseOrOpen = false;
//    public bool isPause = false;
//    //**************************************************

//    [SerializeField] private GameObject commonLoadingUI_prefab;
//    private CommonLoadingUI commonLoadingUI;
//    public CommonLoadingUI CommonLoadingUI => commonLoadingUI;

//    //**************************************************
//    [SerializeField] public string nextSceneName;
//    [SerializeField] public string introLoadingScene;
//    [SerializeField] public string commonLoadingScene; 
//    //**************************************************

//    protected override void Awake()
//    {
//        base.Awake();
//    }

//    public void Creat_UI(WhichUI ui) // UI 생성 메소드 입니다.
//    {
//        GameObject temp_ob;
//        switch (ui)
//        {
//            case WhichUI.introUI:
//                if (introUI != null)
//                {
//                    Debug.Log($"{ui}는 이미 존재합니다.");
//                    return;
//                }

//                temp_ob = Instantiate(introUI_prefab);
//                introUI = temp_ob.GetComponent<IntroUI>();
//                break;

//            case WhichUI.mainCityUI:
//                if (mainCityUI != null)
//                {
//                    Debug.Log($"{ui}는 이미 존재합니다.");
//                    return;
//                }

//                temp_ob = Instantiate(mainCityUI_prefab);
//                mainCityUI = temp_ob.GetComponent<MainCityUI>();
//                break;

//            case WhichUI.pauseMenuUI:
//                if (pauseMenuUI != null)
//                {
//                    Debug.Log($"{ui}는 이미 존재합니다.");
//                    return;
//                }

//                temp_ob = Instantiate(pauseMenuUI_prefab);
//                pauseMenuUI = temp_ob.GetComponent<PauseMenuUI>();
//                break;

//            case WhichUI.inGameUI:
//                if (inGameUI != null)
//                {
//                    Debug.Log($"{ui}는 이미 존재합니다.");
//                    Destroy(inGameUI.gameObject);
//                }

//                temp_ob = Instantiate(inGameUI_prefab);
//                inGameUI = temp_ob.GetComponent<InGameUI>();
//                break;
//        }    
//    } 

//    public void OpenAndClosePause() // Pause를 열고 닫는 메소드입니다.
//    {
//        if (!isCloseOrOpen)
//        {
//            if (isPause)
//            {
//                pauseMenuUI.OnClickClose();
//            }

//            else
//            {
//                pauseMenuUI.gameObject.SetActive(true);
//                StartCoroutine(pauseMenuUI.CallPauseMenu_co());
//            }
//        }
//    }

//    public void OnClickGameOff()
//    {
//        Application.Quit();
//    }
//}
