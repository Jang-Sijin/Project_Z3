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
    * UI �޴������� �� �ε�� �˸��� UI�� Load�մϴ�
    * Ingame������ InGameUI�� �ҷ���
    * Intro������ IntroUI�� �ҷ���
    * MainMenu������ MainMenuUI�� �ҷ���
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

    public void LoadScene(string sceneName) // �Ϲ� �� �ҷ����� ����Դϴ�.
    {
        nextSceneName = sceneName;
        SceneManager.LoadScene("LoadingScene");
        StartCoroutine(LoadScene_co(false));
    }

    public void LoadScene(string sceneName, bool isIntro) // ��Ʈ�ο��� �� �ҷ����� ����Դϴ�.
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
            case true: // ��Ʈ�ο��� �ε�
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

            case false: // �׳� �ε���
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
