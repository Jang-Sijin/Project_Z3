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
using UnityEditor.EditorTools;

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

    [Header("하위 UI들")]
    [SerializeField] private GameObject inventoryUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject equipmentUI;
    [SerializeField] private GameObject CharSelectUI;
    [SerializeField] private CharStatUI charStatUI;
    [SerializeField] private MovePanel CharLevelUpUI;
    [SerializeField] private MovePanel PromoteUI;
    public enum EMenuState
    {
        MainCity = 0,
        MainMenu = 1,
        CharSelectMenu = 2,
        CharStatMenu = 3,
        CharPromteMenu = 4,
        CharLevelUpMenu =5,
        EquipmentMenu = 6,
        InventoryMenu = 7,
        PauseMenu = 8,
        ShopMenu = 9,
        GameOffMenu = 10
    };

    public EMenuState emenuState;

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
                emenuState = EMenuState.MainMenu;
                Debug.Log(emenuState);
            }

            else
            {
                TurnOffMenuByMenuStateEnum();
                Debug.Log(emenuState);
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
        videoPlayer.gameObject.SetActive(false);
        ReturnDark_bg();

        foreach (MovePanel movePanel in movePanels)
        {
            movePanel.GoToOriginPos();
        }

        BGFadeEFF();
    }
    #endregion

    #region 백그라운드UI 효과
    public void ChangeToOtherMenuEFF() // 메뉴에서 다른 메뉴를 클릭했을 때 효과입니다. (예 : 캐릭터 선택 클릭시 -> 캐릭터 선택 UI로 이동)
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

    public void TurnOffMenuByMenuStateEnum() // 메뉴를 공간 상태에 따라 이동하는 메소드입니다.
    {
        switch (emenuState)
        {
            case EMenuState.MainMenu:
                End_bg();
                DeactivateAllUI();
                CharSelectUI.SetActive(false);
                emenuState = EMenuState.MainCity;
                break;

            case EMenuState.CharSelectMenu:
                ChangeToOtherMenuEFF();
                CharSelectUI.SetActive(false);
                emenuState = EMenuState.MainMenu;
                break;

            case EMenuState.CharStatMenu:
                ChangeToOtherMenuEFF();
                charStatUI.gameObject.SetActive(false);
                emenuState = EMenuState.CharSelectMenu;
                break;

            case EMenuState.CharPromteMenu:
                PromoteUI.GoToEndPos();
                charStatUI.TurnOffSideUIBG();
                emenuState = EMenuState.CharStatMenu;
                break;

            case EMenuState.CharLevelUpMenu:
                CharLevelUpUI.GoToEndPos();
                charStatUI.TurnOffSideUIBG();
                emenuState= EMenuState.CharStatMenu;
                break;

            case EMenuState.EquipmentMenu:
                ChangeToOtherMenuEFF();
                equipmentUI.SetActive(false);
                emenuState = EMenuState.CharSelectMenu;
                break;
            case EMenuState.InventoryMenu:
                ChangeToOtherMenuEFF();
                inventoryUI.SetActive(false); 
                emenuState = EMenuState.MainMenu;
                break;

            case EMenuState.PauseMenu:
                emenuState = EMenuState.MainMenu;
                Debug.Log($"{emenuState} 구현 필요.");
                break;

            case EMenuState.ShopMenu:
                emenuState = EMenuState.MainMenu;
                Debug.Log($"{emenuState} 구현 필요.");
                break;

            case EMenuState.GameOffMenu:
                emenuState = EMenuState.MainMenu;
                Debug.Log($"{emenuState} 구현 필요.");
                break;

            default:
                Debug.LogWarning($"그런 {emenuState} UI 환경은 없습니다.");
                break;
        }
        Debug.Log(emenuState);
    }

    public void OnClickCharSelectMenuBTN()
    {
        ChangeToOtherMenuEFF();
        CharSelectUI.SetActive(true);
        emenuState = EMenuState.CharSelectMenu;
    }

    public void DeactivateAllUI() // 모든 UI 한 번에 끄기
    {
        CharSelectUI.SetActive(false);

        charStatUI.gameObject.SetActive(false);

        //equipmentUI.SetActive(false);

        //inventoryUI.SetActive(false);

        emenuState = EMenuState.MainCity;
        End_bg();
        Debug.Log(emenuState);
    }
        //******************************************************************************************
    }
