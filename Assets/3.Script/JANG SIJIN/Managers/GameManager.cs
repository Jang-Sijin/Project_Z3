using static Define;
using System.Collections.Generic;
using System;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : SingletonBase<GameManager>
{
    public float StageClearTime = 0f;
    private DateTime stageStartTime;
    public int StageTotalExp = 0;
    public List<Build_Item> StageGetItemList;
    public List<Build_Item> DropItemList;

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
        Debug.Log("Stage started at: " + stageStartTime);
    }

    public void CalculateStageClearTime()
    {
        StageClearTime = (float)(DateTime.Now - stageStartTime).TotalSeconds;
        Debug.Log("Stage cleared in: " + StageClearTime + " seconds");
    }

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
                CalculateStageClearTime();
                InitClear();
                break;
        }
    }

    private void InitTitle()
    {
        UIManager.Instance.OpenIntroUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Title.ToString());
    }

    private void InitTown()
    {
        UIManager.Instance.OpenCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Town.ToString());
    }

    private void InitHome()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.Instance.OpenCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Home.ToString());
    }

    private void InitBattle1()
    {
        UIManager.Instance.CloseCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle1.ToString());
        SetStageStartTime();
    }

    private void InitBattle2()
    {
        UIManager.Instance.CloseCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle2.ToString());
        SetStageStartTime();
    }

    private void InitBattle3()
    {
        UIManager.Instance.CloseCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle3.ToString());
        SetStageStartTime();
    }

    private void InitBattle4()
    {
        UIManager.Instance.CloseCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle4.ToString());
        SetStageStartTime();
    }

    private void InitBattle5()
    {
        UIManager.Instance.CloseCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle5.ToString());
        SetStageStartTime();
    }

    private void InitClear()
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

    public static Define.SceneType GetCurrentSceneName()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        Define.SceneType sceneType;
        Enum.TryParse(currentScene.name, out sceneType);

        return sceneType;
    }

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
