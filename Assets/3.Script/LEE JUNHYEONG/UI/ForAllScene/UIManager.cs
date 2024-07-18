using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public enum WhichScene
{
    INTRO = 0,
    MAINCITY = 1,
    INGAME = 2
};

public class UIManager : MonoBehaviour
{
    /*
    * UI 메니저에서 씬 로드시 알맞은 UI를 Load합니다
    * Ingame씬에서 InGameUI를 불러옴
    * Intro씬에서 IntroUI를 불러옴
    * MainMenu씬에서 MainMenuUI를 불러옴
    */

    public static UIManager instance = null;

    [SerializeField] private GameObject introUI_ob;
    public IntroUI introUI;

    [SerializeField] private GameObject mainCityUI_ob;
    public MainCityUI mainCityUI;

    [SerializeField] private GameObject inGameUI_ob;
    public InGameUI inGameUI;

    //**************************************************
    [SerializeField] private GameObject pauseMenuUI_ob;
    public PauseMenuUI pauseMenuUI;

    public bool isCloseOrOpen = false;
    public bool isPause = false;
    //**************************************************

    [SerializeField] private GameObject commonLoadingUI_ob;
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

    public void Creat_Intro_UI()
    {
        GameObject temp_ob = Instantiate(introUI_ob);
        introUI = temp_ob.GetComponent<IntroUI>();
    }

    public void Creat_Lobby_UI()
    {
        GameObject temp_ob = Instantiate(mainCityUI_ob);
        mainCityUI = temp_ob.GetComponent<MainCityUI>();
    }

    public void Creat_InGame_UI()
    {
        GameObject temp_ob = Instantiate(inGameUI_ob);
        inGameUI = temp_ob.GetComponent<InGameUI>();
    }

    public void CreatPause()
    {
        GameObject temp_ob = Instantiate(pauseMenuUI_ob);
        pauseMenuUI = temp_ob.GetComponent <PauseMenuUI>();
    }

    public void OpenAndClosePause()
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

    public void LoadScene(string sceneName) // 일반 씬 불러오는 방법입니다.
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
        StartCoroutine(LoadScene_co(false));
    }

    public void LoadScene(string sceneName, bool isIntro) // 인트로에서 씬 불러오는 방법입니다.
    {
        nextSceneName = sceneName;
        StartCoroutine(LoadScene_co(true));
    }

    private IEnumerator LoadScene_co(bool isIntro)
    {
        AsyncOperation op = SceneManager.LoadSceneAsync("Intro", LoadSceneMode.Additive);
        op.allowSceneActivation = false;
        float timer = 0f;

        switch (isIntro)
        {
            case true: // 인트로에서 로딩
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
}
