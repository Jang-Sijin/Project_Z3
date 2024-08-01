using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Define;

public class GameManager : SingletonBase<GameManager>
{
    public float StageClearTime = 0f;
    public int StageTotalExp = 0;
    public List<Build_Item> StageGetItemList;

    public List<Build_Item> DropItemList;
    private DateTime stageStartTime;

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

    public void SetStageStartTime()
    {
        stageStartTime = DateTime.Now;
    }

    public void CalculateStageClearTime()
    {
        StageClearTime = (float)(DateTime.Now - stageStartTime).TotalSeconds;
    }

    /// <summary>
    /// 씬이 변경될 때 초기화가 필요한 작업을 수행합니다.
    /// </summary>
    public void ChangeSceneInit(SceneType sceneType)
    {
        UIManager.Instance.CloseAllUI();
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
                RefreshData();
                InitBattle1();
                break;
            case Define.SceneType.Battle2:
                RefreshData();
                InitBattle2();
                break;
            case Define.SceneType.Battle3:
                RefreshData();
                InitBattle3();
                break;
            case Define.SceneType.Battle4:
                RefreshData();
                InitBattle4();
                break;
            case Define.SceneType.Battle5:
                RefreshData();
                InitBattle5();
                break;
            case Define.SceneType.Clear:
                InitClaer();
                break;
        }
    }

    // 1. 타이틀 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitTitle()
    {
        UIManager.Instance.OpenIntroUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Title.ToString());
    }

    // 2. 마을 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitTown()
    {
        UIManager.Instance.OpenCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Town.ToString());
    }

    // 3. 홈(비디오) 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitHome()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.Instance.OpenCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Home.ToString());
    }

    // 4-1. 배틀1 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitBattle1()
    {
        UIManager.Instance.CloseCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle1.ToString());
        //PlayerController를 불러오기 위해 PlayerController Start에서 호출
        //UIManager.Instance.OpenIngameUI();
    }

    // 4-2. 배틀2 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitBattle2()
    {
        UIManager.Instance.CloseCityUI();
        //PlayerController를 불러오기 위해 PlayerController Start에서 호출
        //UIManager.Instance.OpenIngameUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle2.ToString());
    }

    // 4-3. 배틀3 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitBattle3()
    {
        UIManager.Instance.CloseCityUI();
        //PlayerController를 불러오기 위해 PlayerController Start에서 호출
        //UIManager.Instance.OpenIngameUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle3.ToString());
    }

    // 4-4. 배틀4 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitBattle4()
    {
        UIManager.Instance.CloseCityUI();
        //PlayerController를 불러오기 위해 PlayerController Start에서 호출
        //UIManager.Instance.OpenIngameUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle4.ToString());
    }

    // 4-5. 배틀5 씬에 진입했을 때, 초기화가 필요한 로직들 실행. (초기 설정 필요한 작업 집합소)
    private void InitBattle5()
    {
        UIManager.Instance.CloseCityUI();
        //PlayerController를 불러오기 위해 PlayerController Start에서 호출
        //UIManager.Instance.OpenIngameUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle5.ToString());
    }

    private void InitClaer()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        // SoundManager.Instance.PlayBgm(Define.SceneType.Clear.ToString());
    }

    private void RefreshData()
    {
        StageClearTime = 0f;
        StageTotalExp = 0;
        StageGetItemList = new List<Build_Item>();
        SetStageStartTime();  // 스테이지 시작 시간을 설정합니다.
    }

    /// <summary>
    /// 아래부터는 외부 접근 가능 메서드
    /// </summary>
    /// <returns></returns>
    public static Define.SceneType GetCurrentSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        Define.SceneType sceneType;
        Enum.TryParse(currentScene.name, out sceneType);

        return sceneType;
    }

    /// <summary>
    /// DropItemList에서 무작위로 아이템을 선택합니다.
    /// </summary>
    /// <returns>무작위로 선택된 Build_Item</returns>
    public Build_Item SelectRandomDropItem()
    {
        if (DropItemList == null || DropItemList.Count == 0)
        {
            throw new InvalidOperationException("DropItemList is empty or not initialized.");
        }

        int randomIndex = UnityEngine.Random.Range(0, DropItemList.Count);
        return DropItemList[randomIndex];
    }
}
