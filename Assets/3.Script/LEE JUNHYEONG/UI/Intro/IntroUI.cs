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

    //private void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.Escape))
    //        UIManager.Instance.OpenAndClosePause();
    //}
    public void OnTVClick()
    {
        TV.nextVideo(TV._player);
    }
    public void OnClickGameStart() // debug
    {
        loadingBarBackGround.gameObject.SetActive(true);

        SceneManagerEx.Instance.LoadScene(Define.SceneType.Town, false);
    }
}
