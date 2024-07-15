using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    /*
    * UI 메니저에서 씬 로드시 알맞은 UI를 Load합니다
    * Ingame씬에서 InGameUI를 불러옴
    * Intro씬에서 IntroUI를 불러옴
    * MainMenu씬에서 MainMenuUI를 불러옴
    */
    public static UIManager instance = null;

    [SerializeField] private GameObject introUI_ob;

    [SerializeField] private GameObject mainMenu_ob;

    [SerializeField] private GameObject inGameUI_ob;

    [SerializeField] private PauseMenuUI pauseMenu;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void Creat_Intro_UI()
    {
        Instantiate(introUI_ob);
    }

    public void Creat_Lobby_UI()
    {
        Instantiate(mainMenu_ob);
    }

    public void Creat_InGame_UI()
    {
        Instantiate(inGameUI_ob);
    }

    public void Creat_PuaseMenu_UI()
    {
        Instantiate(pauseMenu);
    }
}
