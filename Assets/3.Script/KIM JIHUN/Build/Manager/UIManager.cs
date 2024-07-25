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

    public void LoadScene(Define.SceneType sceneType) // �Ϲ� �� �ҷ����� ����Դϴ�.
    {
        Debug.Log($"�� �̵�: " + sceneType);
        nextSceneName = sceneType.ToString();
        GameManager.Instance.ChangeSceneInit(sceneType);
        StartCoroutine(LoadScene_co(false));
    }
    public void LoadScene(Define.SceneType sceneType, bool isIntro) // ��Ʈ�ο��� �� �ҷ����� ����Դϴ�.
    {
        Debug.Log($"�� �̵�: " + sceneType);
        nextSceneName = sceneType.ToString();
        GameManager.Instance.ChangeSceneInit(sceneType);
        StartCoroutine(LoadScene_co(true)); // �񵿱� �ε�
    }

    private IEnumerator LoadScene_co(bool isIntro) // ������ �� �ҷ����� �޼ҵ��Դϴ�.
    {        
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneName);

        //Debug.Log($"isIntro : {isIntro}");

        op.allowSceneActivation = false;
        float timer = 0f;
        
        switch (isIntro)
        {
            case true: // ��Ʈ�ο��� �ε�
                introUI.MiddleText.text = "�ε���";

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
                            introUI.MiddleText.text = "�ε� �Ϸ�";
                            op.allowSceneActivation = true;
                            introUI.gameObject.SetActive(false);
                            mainCityUI.gameObject.SetActive(true);
                            yield break;
                        }
                    }
                }
                break;
            case false: // �׳� �ε���
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

                        yield return new WaitForSeconds(2f); // 2�� ���

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