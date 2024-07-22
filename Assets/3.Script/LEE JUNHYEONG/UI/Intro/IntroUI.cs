using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private Intro_TVContorl TV;

    [SerializeField] private Image loadingBarBackGround;
    [SerializeField] private Image loadingBarFill;
    [SerializeField] public TextMeshProUGUI MiddleText;

    public Image loadingFill
    {
        get
        {
            return loadingBarFill;
        }

        set
        {
            loadingBarFill = value;
        }
    }
    private void Start()
    {
        UIManager.instance.Creat_UI(WhichUI.pauseMenuUI);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
            UIManager.instance.OpenAndClosePause();
    }
    public void OnTVClick()
    {
        TV.nextVideo(TV._player);
    }
    public void OnClickGameStart() // debug
    {
        loadingBarBackGround.gameObject.SetActive(true);

        UIManager.instance.LoadScene("MainCity", true);
    }
}
