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
        // Ÿ��Ʋ ������ �����Ѵٴ� �����Ͽ� ȣ��Ǵ� �ʱ� ���� �� �Դϴ�.
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
    /// ���� ����� �� �ʱ�ȭ�� �ʿ��� �۾��� �����մϴ�.
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

    // 1. Ÿ��Ʋ ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitTitle()
    {
        UIManager.Instance.OpenIntroUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Title.ToString());
    }

    // 2. ���� ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitTown()
    {
        UIManager.Instance.OpenCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Town.ToString());
    }

    // 3. Ȩ(����) ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitHome()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        UIManager.Instance.OpenCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Home.ToString());
    }

    // 4-1. ��Ʋ1 ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitBattle1()
    {
        UIManager.Instance.CloseCityUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle1.ToString());
        //PlayerController�� �ҷ����� ���� PlayerController Start���� ȣ��
        //UIManager.Instance.OpenIngameUI();
    }

    // 4-2. ��Ʋ2 ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitBattle2()
    {
        UIManager.Instance.CloseCityUI();
        //PlayerController�� �ҷ����� ���� PlayerController Start���� ȣ��
        //UIManager.Instance.OpenIngameUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle2.ToString());
    }

    // 4-3. ��Ʋ3 ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitBattle3()
    {
        UIManager.Instance.CloseCityUI();
        //PlayerController�� �ҷ����� ���� PlayerController Start���� ȣ��
        //UIManager.Instance.OpenIngameUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle3.ToString());
    }

    // 4-4. ��Ʋ4 ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitBattle4()
    {
        UIManager.Instance.CloseCityUI();
        //PlayerController�� �ҷ����� ���� PlayerController Start���� ȣ��
        //UIManager.Instance.OpenIngameUI();
        SoundManager.Instance.PlayBgm(Define.SceneType.Battle4.ToString());
    }

    // 4-5. ��Ʋ5 ���� �������� ��, �ʱ�ȭ�� �ʿ��� ������ ����. (�ʱ� ���� �ʿ��� �۾� ���ռ�)
    private void InitBattle5()
    {
        UIManager.Instance.CloseCityUI();
        //PlayerController�� �ҷ����� ���� PlayerController Start���� ȣ��
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
        SetStageStartTime();  // �������� ���� �ð��� �����մϴ�.
    }

    /// <summary>
    /// �Ʒ����ʹ� �ܺ� ���� ���� �޼���
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
    /// DropItemList���� �������� �������� �����մϴ�.
    /// </summary>
    /// <returns>�������� ���õ� Build_Item</returns>
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
