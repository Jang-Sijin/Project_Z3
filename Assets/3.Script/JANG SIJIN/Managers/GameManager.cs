using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class GameManager : SingletonBase<GameManager>    
{
    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        Init();
    }

    private void Init()
    {
        // Ÿ��Ʋ ������ �����Ѵٴ� �����Ͽ� ȣ��Ǵ� �ʱ� ���� �� �Դϴ�.
        ChangeSceneInit(GetCurrentSceneName());
    }

    /// <summary>
    /// ���� ����� �� �ʱ�ȭ�� �ʿ��� �۾��� �����մϴ�.
    /// </summary>
    public void ChangeSceneInit(SceneType sceneType)
    {
        switch (sceneType)
        {
            case Define.SceneType.Title:
                InitTitle();
                break;
            case Define.SceneType.Town:
                InitTown();
                break;
            case Define.SceneType.Home:
                InitHome();
                break;
            case Define.SceneType.Battle1:
                InitBattle1();
                break;
            case Define.SceneType.Battle2:
                InitBattle2();
                break;
            case Define.SceneType.Battle3:
                InitBattle3();
                break;
            case Define.SceneType.Battle4:
                InitBattle4();
                break;
            case Define.SceneType.Battle5:
                InitBattle5();
                break;
        }
    }

    // 1. Ÿ��Ʋ ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitTitle()
    {
        UIManager.instance.Creat_UI(WhichUI.pauseMenuUI);
        SoundManager.Instance.PlayBgm(Define.SceneType.Title.ToString());
    }    

    // 2. ���� ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitTown()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Town.ToString());
    }

    // 3. Ȩ(����) ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitHome()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Home.ToString());
    }

    // 4-1. ��Ʋ1 ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitBattle1()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle1.ToString());
    }

    // 4-2. ��Ʋ2 ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitBattle2()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle2.ToString());
    }

    // 4-3. ��Ʋ3 ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitBattle3()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle3.ToString());
    }

    // 4-4. ��Ʋ4 ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitBattle4()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle4.ToString());
    }

    // 4-5. ��Ʋ5 ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitBattle5()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle5.ToString());
    }

    public static Define.SceneType GetCurrentSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        Define.SceneType sceneType;
        Enum.TryParse(currentScene.name, out sceneType);

        return sceneType;
    }
}
