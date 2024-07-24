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
        // 타이틀 씬에서 시작한다는 가정하에 호출되는 초기 설정 값 입니다.
        ChangeSceneInit(GetCurrentSceneName());
    }

    /// <summary>
    /// 씬이 변경될 때 초기화가 필요한 작업을 수행합니다.
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

    // 1. 타이틀 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitTitle()
    {
        UIManager.instance.Creat_UI(WhichUI.pauseMenuUI);
        SoundManager.Instance.PlayBgm(Define.SceneType.Title.ToString());
    }    

    // 2. 마을 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitTown()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Town.ToString());
    }

    // 3. 홈(비디오) 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitHome()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Home.ToString());
    }

    // 4-1. 배틀1 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitBattle1()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle1.ToString());
    }

    // 4-2. 배틀2 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitBattle2()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle2.ToString());
    }

    // 4-3. 배틀3 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitBattle3()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle3.ToString());
    }

    // 4-4. 배틀4 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitBattle4()
    {
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle4.ToString());
    }

    // 4-5. 배틀5 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
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
