using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Rendering;
using UnityEngine.Video;

public class Build_MainCityUI : MonoBehaviour
{
    // �޴��� �Ѱ� �� �� �ߵ��Ǵ� �޼ҵ�
    //******************************************************************************************

    [SerializeField] private Image image_BG;

    [SerializeField] private float duration_BG;
    [SerializeField] private GameObject videoPlayer;

    private List<MovePanel> movePanels;
    public bool isOpened = false;
    private Tween tween;
    private void Start()
    {
        movePanels = new List<MovePanel>();

        movePanels = GetComponentsInChildren<MovePanel>().ToList();
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Escape))
    //    {
    //        if (!isOpened)
    //        {
    //            Start_bg();
    //        }
    //
    //        else
    //        {
    //            End_bg();
    //        }
    //    }
    //}

    public void ToggleMainCityUI()
    {
        if (UIManager.Instance.PauseMenuUI.gameObject.activeSelf) return;

        if (!isOpened)
        {
            BelleController.INSTANCE.LockCamera();
            UIManager.Instance.LockPlayer();
            UIManager.Instance.CloseCityUI();
            Start_bg();
            videoPlayer.GetComponent<VideoPlayer>().targetCamera = Camera.main;
        }

        else
        {
            BelleController.INSTANCE.UnlockCamera();
            UIManager.Instance.OpenCityUI();
            UIManager.Instance.UnlockPlayer();
            End_bg();
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
