using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Runtime.CompilerServices;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.Rendering;
using UnityEngine.Video;
using UnityEngine.Assertions.Must;

public class MainCityMenuUIManager : MonoBehaviour
{

    #region 메인 메뉴 UI를 위한 변수들
    [SerializeField] private Image fadeImage_BG; // 화면 까맣게 하는 이미지
    [SerializeField] private float duration_BG; // 화면을 까맣게 혹은 밝게하는 시간
    [SerializeField] private GameObject videoPlayer; // 재생할 비디오

    private List<MovePanel> movePanels; // 사이드에 움직일 패널입니다.
    private bool isOpened = false; // 메인 메뉴 UI가 열렸는지 확인하는 불값
    private Tween tween; // 트윈 캐싱 : 전 트위닝을 취소하기 위해서
    #endregion

    [SerializeField] private GameObject CharSelectUI;
    [SerializeField] private GameObject inVentoryUI;
    [SerializeField] private GameObject pauseUI;
    public enum EMenuState
    {
        MainMenu = 0,
        CharSelect = 1,
        Inventory = 2,
        PauseMenu =3,
            Shop = 4,
            GameOff = 5
    };

    EMenuState menuState;

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

    #region 메인메뉴UI 켜고 끄는 메소드

    private void Start_bg() // 메뉴를 눌렀을 경우 처음 효과 메소드
    {
        isOpened = true;

        foreach (MovePanel movePanel in movePanels)
        {
            movePanel.GoToTargetPos();
        }
        StartClean_bg();
        BGVisualizeEFF();
    }



    public void End_bg() // 메뉴를 끄는 효과
    {
        isOpened = false;
        DeactivateAllUI();
        videoPlayer.gameObject.SetActive(false);
        ReturnDark_bg();
        menuState = EMenuState.MainMenu;

        foreach (MovePanel movePanel in movePanels)
        {
            movePanel.GoToOriginPos();
        }

        BGFadeEFF();
    }
    #endregion

    #region 백그라운드UI 효과
    private void ChangeToOtherMenuEFF() // 메뉴에서 다른 메뉴를 클릭했을 때 효과입니다. (예 : 캐릭터 선택 클릭시 -> 캐릭터 선택 UI로 이동)
    {
        ReturnDark_bg();
        BGFadeEFF();
    }
    private void StartClean_bg() // Clean_bg_co() 코루틴 실행 메소드
    {
        StartCoroutine(Clean_bg_co());
    }

    private IEnumerator Clean_bg_co() // 배경을 일정시간 이후 없애기
    {
        yield return new WaitForSeconds(duration_BG);

        if(isOpened)
        videoPlayer.gameObject.SetActive(true);

        yield return new WaitForSeconds(0.3f);

        if (isOpened)
        {
            Color color = fadeImage_BG.color;
            color.a = 0f;
            fadeImage_BG.color = color;
        }
    }
    private void ReturnDark_bg() // 까만 화면으로 바꾸기
    {
        Color color = fadeImage_BG.color;
        color.a = 0.8f;
        fadeImage_BG.color = color;
    }

    private void BGVisualizeEFF() // 화면 까맣게 하는 효과
    {
        if (tween != null)
        {
            tween.Kill();
        }

        tween = fadeImage_BG.DOFade(1f, duration_BG).OnComplete(StartClean_bg);
    }

    private void BGFadeEFF() // 화면 사라지게 하는 효과
    {
        if (tween != null)
        {
            tween.Kill();
        }

        tween = fadeImage_BG.DOFade(0f, duration_BG);
    }

    #endregion

    /*
     *  하나의 메서드로 메뉴를 키고 하나의 메서드로 메뉴를 끊다
     */

    public void TurnOnMenuByOBJ(GameObject UI_obj)
    {
        switch (UI_obj.name)
        {
            case "CharSelectUI":

                ChangeToOtherMenuEFF();
                CharSelectUI.SetActive(true);
                menuState = EMenuState.CharSelect;
                break;

            default:
                break;
        }
    }

    public void TurnOffMenuByMenuStateEnum() // 메뉴를 공간 상태에 따라 이동하는 메소드입니다.
    {
        switch (menuState)
        {
            case EMenuState.MainMenu:
                End_bg();
                break;

            case EMenuState.CharSelect:
                CharSelectUI.SetActive(false);
                menuState = EMenuState.MainMenu;
                break;

            case EMenuState.Inventory:
                menuState = EMenuState.MainMenu;
                Debug.Log($"{menuState} 구현 필요.");
                break;

            case EMenuState.PauseMenu:
                menuState = EMenuState.MainMenu;
                Debug.Log($"{menuState} 구현 필요.");
                break;

            case EMenuState.Shop:
                menuState = EMenuState.MainMenu;
                Debug.Log($"{menuState} 구현 필요.");
                break;

            case EMenuState.GameOff:
                menuState = EMenuState.MainMenu;
                Debug.Log($"{menuState} 구현 필요.");
                break;

            default:
                Debug.Log($"그런 {menuState} UI 환경은 없습니다.");
                break;
        }
    }

    private void DeactivateAllUI() // 모든 UI 한 번에 끄기
    {
        CharSelectUI.SetActive(false);
    }
        //******************************************************************************************
    }
