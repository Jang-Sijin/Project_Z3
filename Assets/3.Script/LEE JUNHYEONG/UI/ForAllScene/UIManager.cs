using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum WhichUI
{
    introUI = 0,
    mainCityUI = 1,
    inGameUI = 2,
    pauseMenuUI = 3,
    commonLoadingUI =4
};
/*
 * UI 메니저에서 모든 UI를 생성, 제거하는 메소드가 있습니다.
 */

public class UIManager : MonoBehaviour
{
    /*
    * UI 메니저에서 씬 로드시 알맞은 UI를 Load합니다
    * Ingame씬에서 InGameUI를 불러옴
    * Intro씬에서 IntroUI를 불러옴
    * MainMenu씬에서 MainMenuUI를 불러옴
    */

    public static UIManager instance = null;

    [SerializeField] private GameObject introUI_prefab;
    public IntroUI introUI;

    [SerializeField] private GameObject mainCityUI_prefab;
    public MainCityUI mainCityUI;

    [SerializeField] private GameObject inGameUI_prefab;
    public InGameUI inGameUI;

    //**************************************************
    [SerializeField] private GameObject pauseMenuUI_prefab;
    [SerializeField]public PauseMenuUI pauseMenuUI;

    public bool isCloseOrOpen = false;
    public bool isPause = false;
    //**************************************************

    [SerializeField] private GameObject commonLoadingUI_prefab;
    public commonLoadingUI commonLoadingUI;

    //**************************************************
    [SerializeField] public string nextSceneName;
    [SerializeField] public string introLoadingScene;
    [SerializeField] public string commonLoadingScene; 
    //**************************************************

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Creat_UI(WhichUI ui) // UI 생성 메소드 입니다.
    {
        GameObject temp_ob;
        switch (ui)
        {
            case WhichUI.introUI:
                if (introUI != null)
                {
                    Debug.Log($"{ui}는 이미 존재합니다.");
                    return;
                }

                temp_ob = Instantiate(introUI_prefab);
                introUI = temp_ob.GetComponent<IntroUI>();
                break;

            case WhichUI.mainCityUI:
                if (mainCityUI != null)
                {
                    Debug.Log($"{ui}는 이미 존재합니다.");
                    return;
                }

                temp_ob = Instantiate(mainCityUI_prefab);
                mainCityUI = temp_ob.GetComponent<MainCityUI>();
                break;

            case WhichUI.pauseMenuUI:
                if (pauseMenuUI != null)
                {
                    Debug.Log($"{ui}는 이미 존재합니다.");
                    return;
                }

                temp_ob = Instantiate(pauseMenuUI_prefab);
                pauseMenuUI = temp_ob.GetComponent<PauseMenuUI>();
                break;

            case WhichUI.inGameUI:
                if (inGameUI != null)
                {
                    Debug.Log($"{ui}는 이미 존재합니다.");
                    Destroy(inGameUI.gameObject);
                }

                temp_ob = Instantiate(inGameUI_prefab);
                inGameUI = temp_ob.GetComponent<InGameUI>();
                break;
        }    
    } 

    public void OpenAndClosePause() // Pause를 열고 닫는 메소드입니다.
    {
        if (!isCloseOrOpen)
        {
            if (isPause)
            {
                pauseMenuUI.OnClickClose();
            }

            else
            {
                pauseMenuUI.gameObject.SetActive(true);
                StartCoroutine(pauseMenuUI.CallPauseMenu_co());
            }
        }
    }

    public void LoadScene(Define.SceneType sceneType) // 일반 씬 불러오는 방법입니다.
    {
        nextSceneName = sceneType.ToString();        
        SceneManager.LoadScene("LoadingScene");
        GameManager.Instance.ChangeSceneInit(sceneType);
        StartCoroutine(LoadScene_co(false));
    }

    public void LoadScene(Define.SceneType sceneType, bool isIntro) // 인트로에서 씬 불러오는 방법입니다.
    {
        nextSceneName = sceneType.ToString();
        GameManager.Instance.ChangeSceneInit(sceneType);
        StartCoroutine(LoadScene_co(true));
    }

    private IEnumerator LoadScene_co(bool isIntro) // 디버깅용 씬 불러오는 메소드입니다.
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Intro", LoadSceneMode.Additive);
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
                            yield break;
                        }
                    }
                }
                break;

            case false: // 그냥 로딩씬
                while (!op.isDone)
                {
                    yield return null;

                    if (op.progress < 0.99f)
                    {
                        commonLoadingUI.ActivateEndText();

                        if (Input.anyKeyDown)
                        {
                            op.allowSceneActivation = true;
                            yield break;
                        }
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
