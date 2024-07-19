using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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
 * UI �޴������� ��� UI�� ����, �����ϴ� �޼ҵ尡 �ֽ��ϴ�.
 */

public class UIManager : MonoBehaviour
{
    /*
    * UI �޴������� �� �ε�� �˸��� UI�� Load�մϴ�
    * Ingame������ InGameUI�� �ҷ���
    * Intro������ IntroUI�� �ҷ���
    * MainMenu������ MainMenuUI�� �ҷ���
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
    public PauseMenuUI pauseMenuUI;

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

    public void Creat_UI(WhichUI ui) // UI ���� �޼ҵ� �Դϴ�.
    {
        GameObject temp_ob;

        switch (ui)
        {
            case WhichUI.introUI:
                temp_ob = Instantiate(introUI_prefab);
                introUI = temp_ob.GetComponent<IntroUI>();
                break;

            case WhichUI.mainCityUI:
                temp_ob = Instantiate(mainCityUI_prefab);
                mainCityUI = temp_ob.GetComponent<MainCityUI>();
                break;

            case WhichUI.pauseMenuUI:
                temp_ob = Instantiate(pauseMenuUI_prefab);
                pauseMenuUI = temp_ob.GetComponent<PauseMenuUI>();
                break;

            case WhichUI.inGameUI:
                temp_ob = Instantiate(inGameUI_prefab);
                inGameUI = temp_ob.GetComponent<InGameUI>();
                break;
        }    
    }

    public void Destory_UI(WhichUI ui)
    {
        switch (ui)
        {
            case WhichUI.introUI:
                Destroy(introUI.gameObject);
                break;

            case WhichUI.mainCityUI:
                Destroy(mainCityUI.gameObject);
                break;

            case WhichUI.pauseMenuUI:
                Destroy(pauseMenuUI.gameObject);
                break;

            case WhichUI.inGameUI:
                Destroy(inGameUI.gameObject);
                break;
        }
    }

    public void OpenAndClosePause() // Pause�� ���� �ݴ� �޼ҵ��Դϴ�.
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

    private IEnumerator LoadScene_co(bool isIntro) // ������ �� �ҷ����� �޼ҵ��Դϴ�.
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

    public void OnClickGameOff()
    {
        Application.Quit();
    }
}
