using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private Intro_TVContorl TV;

    [SerializeField] private Image loadingBarBackGround;
    [SerializeField] private Image loadingBarFill;
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
        TV = FindObjectOfType<Intro_TVContorl>();
        UIManager.instance.CreatPause();
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
        Debug.Log("메인 씬 전환 추가 필요");

        loadingBarBackGround.gameObject.SetActive(true);

        UIManager.instance.LoadScene("MainCity", true);
    }
}
