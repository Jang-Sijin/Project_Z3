using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WhichPlace
{
    INTRO = 0,
    MAINCITY = 1,
    INGAME = 2
};

public class UIManager : MonoBehaviour
{
    /*
    * UI 메니저에서 씬 로드시 알맞은 UI를 Load합니다
    * Ingame씬에서 InGameUI를 불러옴
    * Intro씬에서 IntroUI를 불러옴
    * MainMenu씬에서 MainMenuUI를 불러옴
    */

    public static UIManager instance = null;

    [SerializeField] private IntroUI introUI;

    [SerializeField] private MainCityUI mainMenuUI;

    [SerializeField] private InGameUI inGameUI;

    //**************************************************
    [SerializeField] public PauseMenuUI pauseMenuUI;
    public bool isCloseOrOpen = false;
    public bool isPause = false;
    //**************************************************

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            pauseMenuUI.gameObject.SetActive(true);
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void Creat_Intro_UI()
    {
        Instantiate(introUI);
    }

    private void Creat_Lobby_UI()
    {
        Instantiate(mainMenuUI);
    }

    private void Creat_InGame_UI()
    {
        Instantiate(inGameUI);
    }

    private void CreatPause()
    {
        Instantiate(pauseMenuUI);
    }
}
