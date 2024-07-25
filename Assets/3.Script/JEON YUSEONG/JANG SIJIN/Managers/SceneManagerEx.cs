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


    public void LoadScene(Define.SceneType sceneType) // 일반 씬 불러오는 방법입니다.
    {
        nextSceneName = sceneType.ToString();
        SceneManager.LoadScene(Define.SceneType.LoadingScene.ToString());
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
                UIManager.Instance.IntroUI.MiddleText.text = "로딩중";

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
                            UIManager.Instance.IntroUI.MiddleText.text = "로딩 완료";
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