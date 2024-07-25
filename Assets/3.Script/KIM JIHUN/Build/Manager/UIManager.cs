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

    [SerializeField] private Build_IntroUI introUI;    
    [SerializeField] private MainCityUI mainCityUI;
    [SerializeField] private InGameUI inGameUI;
    [SerializeField] private Build_OptionMenuUI pauseMenuUI;
    [SerializeField] private Build_CommonLoadingUI commonLoadingUI;

    public Build_IntroUI IntroUI => introUI;
    public MainCityUI MainCityUI => mainCityUI;
    public InGameUI InGameUI => inGameUI;
    public Build_OptionMenuUI PauseMenuUI => pauseMenuUI;
    public Build_CommonLoadingUI CommonLoadingUI => commonLoadingUI;

    [HideInInspector] public bool isCloseOrOpen = false;
    [HideInInspector] public bool isPause = false;

    //**************************************************
    [HideInInspector] public string nextSceneName;
    [HideInInspector] public string introLoadingScene;
    [HideInInspector] public string commonLoadingScene;
    //**************************************************

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
                if(BelleController.INSTANCE != null)
                {
                    BelleController.INSTANCE.UnlockMouse();
                    BelleController.INSTANCE.CanInput= false;
                }
                else if (PlayerController.INSTANCE != null)
                {
                    PlayerController.INSTANCE.UnlockMouse();
                    PlayerController.INSTANCE.CanInput = false;
                }                                                
            }
        }
    }

    public void LoadScene(Define.SceneType sceneType) // 일반 씬 불러오는 방법입니다.
    {
        Debug.Log($"씬 이동: " + sceneType);
        nextSceneName = sceneType.ToString();
        GameManager.Instance.ChangeSceneInit(sceneType);
        StartCoroutine(LoadScene_co(false));
    }
    public void LoadScene(Define.SceneType sceneType, bool isIntro) // 인트로에서 씬 불러오는 방법입니다.
    {
        Debug.Log($"씬 이동: " + sceneType);
        nextSceneName = sceneType.ToString();
        GameManager.Instance.ChangeSceneInit(sceneType);
        StartCoroutine(LoadScene_co(true)); // 비동기 로드
    }

    private IEnumerator LoadScene_co(bool isIntro) // 디버깅용 씬 불러오는 메소드입니다.
    {        
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneName);

        //Debug.Log($"isIntro : {isIntro}");

        op.allowSceneActivation = false;
        float timer = 0f;
        
        switch (isIntro)
        {
            case true: // 인트로에서 로딩
                introUI.MiddleText.text = "로딩중";

                while (!op.isDone)
                {
                    yield return null;

                    if (op.progress < 0.90f)
                    {
                        introUI.loadingFill.fillAmount = op.progress;
                    }
                    else
                    {
                        timer += Time.unscaledDeltaTime;
                        introUI.loadingFill.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                        if (introUI.loadingFill.fillAmount >= 1f)
                        {
                            introUI.MiddleText.text = "로딩 완료";
                            op.allowSceneActivation = true;
                            introUI.gameObject.SetActive(false);
                            mainCityUI.gameObject.SetActive(true);
                            yield break;
                        }
                    }
                }
                break;
            case false: // 그냥 로딩씬
                while (!op.isDone)
                {
                    yield return null;
                    if (op.progress < 0.90f)
                    {
                        //Debug.Log("CommonLoadingScene");
                        if (!commonLoadingUI.gameObject.activeSelf)
                        {
                            if (BelleController.INSTANCE != null)
                                BelleController.INSTANCE.CanInput = false;
                            else
                                PlayerController.INSTANCE.CanInput = false;
                            
                            commonLoadingUI.gameObject.SetActive(true);
                        }
                    }
                    if (op.progress < 0.99f)
                    {
                        op.allowSceneActivation = true;

                        yield return new WaitForSeconds(2f); // 2초 대기

                        if (BelleController.INSTANCE != null)
                            BelleController.INSTANCE.CanInput = true;
                        else
                            PlayerController.INSTANCE.CanInput = true;
                        
                        commonLoadingUI.ActivateEndText();
                        
                        yield break;
                    }
                }
                break;
        }
    }

    public void OnClickGameOff()
    {
        Application.Quit();
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