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
    * UI �޴������� �� �ε�� �˸��� UI�� Load�մϴ�
    * Ingame������ InGameUI�� �ҷ���
    * Intro������ IntroUI�� �ҷ���
    * MainMenu������ MainMenuUI�� �ҷ���
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
