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
        nextSceneName = sceneType.ToString();
        SceneManager.LoadScene(Define.SceneType.LoadingScene.ToString());
        GameManager.Instance.ChangeSceneInit(sceneType);
        StartCoroutine(LoadScene_co(false));
    }

    public void LoadScene(Define.SceneType sceneType, bool isIntro) // ��Ʈ�ο��� �� �ҷ����� ����Դϴ�.
    {
        nextSceneName = sceneType.ToString();
        GameManager.Instance.ChangeSceneInit(sceneType);
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
                        UIManager.Instance.CommonLoadingUI.ActivateEndText();

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