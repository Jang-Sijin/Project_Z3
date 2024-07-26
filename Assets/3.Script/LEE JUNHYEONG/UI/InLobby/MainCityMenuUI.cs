using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class MainCityMenuUI : MonoBehaviour
{

    // 메뉴를 켜고 끌 때 발동되는 메소드
    //******************************************************************************************

    [SerializeField] private Image image_BG;

    [SerializeField] private float duration_BG;
    [SerializeField] private GameObject videoPlayer;

    private List<MovePanel> movePanels;
    private bool isOpened = false;
    private Tween tween;
    private void Start()
    {
        movePanels = new List<MovePanel>();

        movePanels = GetComponentsInChildren<MovePanel>().ToList();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isOpened)
            {
                Start_bg();
            }

            else
            {
                End_bg();
            }
        }
    }

    private void Start_bg()
    {
        isOpened = true;

        if (tween != null)
        {
            tween.Kill();
        }

        foreach (MovePanel movePanel in movePanels)
        {
            movePanel.GoToTargetPos();
        }

        tween = image_BG.DOFade(1f, duration_BG).OnComplete(StartClean_bg);
    }

    private void StartClean_bg()
    {
        StartCoroutine(Clean_bg());
    }

    private IEnumerator Clean_bg()
    {
        videoPlayer.gameObject.SetActive(true);
        yield return new WaitForSeconds(duration_BG);

        Color color = image_BG.color;
        color.a = 0f;
        image_BG.color = color;
    }

    private void ReturnDark_bg()
    {
        Color color = image_BG.color;
        color.a = 0.8f;
        image_BG.color = color;
    }

    public void End_bg()
    {
        isOpened = false;

        if (tween != null)
        {
            tween.Kill();
        }

        videoPlayer.gameObject.SetActive(false);
        ReturnDark_bg();

        foreach (MovePanel movePanel in movePanels)
        {
            movePanel.GoToOriginPos();
        }

        tween = image_BG.DOFade(0f, duration_BG);

    }
    //******************************************************************************************
}
