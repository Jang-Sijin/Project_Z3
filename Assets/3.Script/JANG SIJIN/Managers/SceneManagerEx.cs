using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerEx : SingletonBase<SceneManagerEx>
{
    //**************************************************
    private string nextSceneName;
    private string introLoadingScene;
    private string commonLoadingScene;
    //**************************************************

    protected override void Awake()
    {
        base.Awake();
    }


    public void LoadScene(Define.SceneType sceneType) // �Ϲ� �� �ҷ����� ����Դϴ�.
    {
        UIManager.Instance.CloseAllUI();
        nextSceneName = sceneType.ToString();
        //SceneManager.LoadScene(Define.SceneType.LoadingScene.ToString());
        StartCoroutine(LoadScene_co(false));
        GameManager.Instance.ChangeSceneInit(sceneType);
    }

    public void LoadScene(Define.SceneType sceneType, bool isIntro) // ��Ʈ�ο��� �� �ҷ����� ����Դϴ�.
    {
        UIManager.Instance.CloseAllUI();
        nextSceneName = sceneType.ToString();
        StartCoroutine(LoadScene_co(true));
        GameManager.Instance.ChangeSceneInit(sceneType);
    }

    private IEnumerator LoadScene_co(bool isIntro) // ������ �� �ҷ����� �޼ҵ��Դϴ�.
    {
        AsyncOperation op = SceneManager.LoadSceneAsync(nextSceneName);

        //Debug.Log($"isIntro : {isIntro}");

        op.allowSceneActivation = false;
        float timer = 0f;

        UIManager.Instance.UI_Loading.ShowUI();
        switch (isIntro)
        {
            case true: // ��Ʈ�ο��� �ε�
                UIManager.Instance.IntroUI.MiddleText.text = "�ε���";

                while (!op.isDone)
                {
                    yield return null;

                    if (op.progress < 0.90f)
                    {
                        UIManager.Instance.IntroUI.loadingFill.fillAmount = op.progress;
                    }
                    else
                    {
                        timer += Time.unscaledDeltaTime;
                        UIManager.Instance.IntroUI.loadingFill.fillAmount = Mathf.Lerp(0.9f, 1f, timer);

                        if (UIManager.Instance.IntroUI.loadingFill.fillAmount >= 1f)
                        {
                            UIManager.Instance.IntroUI.MiddleText.text = "�ε� �Ϸ�";
                            op.allowSceneActivation = true;
                            UIManager.Instance.IntroUI.gameObject.SetActive(false);
                            UIManager.Instance.MainCityUI.gameObject.SetActive(true);                            
                        }
                    }
                }
                break;
            case false: // �׳� �ε���
                if (nextSceneName != "Shop")
                {
                    while (!op.isDone)
                    {
                        yield return null;
                        if (op.progress < 0.90f)
                        {
                            //Debug.Log("CommonLoadingScene");
                            if (!UIManager.Instance.CommonLoadingUI.gameObject.activeSelf)
                            {
                                UIManager.Instance.CommonLoadingUI.gameObject.SetActive(true);
                            }
                        }
                        if (op.progress < 0.99f)
                        {
                            op.allowSceneActivation = true;

                            yield return new WaitForSeconds(2f); // 2�� ���

                            UIManager.Instance.CommonLoadingUI.ActivateEndText();
                        }
                    }
                    break;
                }
                else
                {
                    while (!op.isDone)
                    {
                        yield return null;
                        if (op.progress < 0.90f)
                        {
                            //Debug.Log("CommonLoadingScene");
                            if (!UIManager.Instance.CommonLoadingUI.gameObject.activeSelf)
                            {
                                UIManager.Instance.CommonLoadingUI.gameObject.SetActive(true);
                            }
                        }
                        if (op.progress < 0.99f)
                        {
                            op.allowSceneActivation = true;

                            yield return new WaitForSeconds(2f); // 2�� ���

                            UIManager.Instance.CommonLoadingUI.ActivateEndText();
                        }
                    }
                    break;
                }
        }
        UIManager.Instance.UI_Loading.HideUI();
    }
}