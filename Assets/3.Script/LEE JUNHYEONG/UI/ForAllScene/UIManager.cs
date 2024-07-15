using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    /*
    * UI �޴������� �� �ε�� �˸��� UI�� Load�մϴ�
    * Ingame������ InGameUI�� �ҷ���
    * Intro������ IntroUI�� �ҷ���
    * MainMenu������ MainMenuUI�� �ҷ���
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
