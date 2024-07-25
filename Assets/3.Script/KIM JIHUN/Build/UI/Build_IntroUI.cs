using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Build_IntroUI : MonoBehaviour
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
        //Build_UIManager.instance.Creat_UI(WhichUI.pauseMenuUI);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            UIManager.Instance.OptionUIOpenClose();
    }
    public void OnTVClick()
    {
        TV.nextVideo(TV._player);
    }
    public void OnClickGameStart() // debug:ÁØÇü
    {
        loadingBarBackGround.gameObject.SetActive(true);

        SceneManagerEx.Instance.LoadScene(Define.SceneType.Home, true);
    }
}
