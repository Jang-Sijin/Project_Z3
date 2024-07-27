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

    #region ���� �޴� UI�� ���� ������
    [SerializeField] private Image fadeImage_BG; // ȭ�� ��İ� �ϴ� �̹���
    [SerializeField] private float duration_BG; // ȭ���� ��İ� Ȥ�� ����ϴ� �ð�
    [SerializeField] private GameObject videoPlayer; // ����� ����

    private List<MovePanel> movePanels; // ���̵忡 ������ �г��Դϴ�.
    private bool isOpened = false; // ���� �޴� UI�� ���ȴ��� Ȯ���ϴ� �Ұ�
    private Tween tween; // Ʈ�� ĳ�� : �� Ʈ������ ����ϱ� ���ؼ�
    #endregion

    [SerializeField] private GameObject CharSelectUI;
    [SerializeField] private GameObject inVentoryUI;
    [SerializeField] private GameObject pauseUI;
    [SerializeField] private GameObject equipmentUI;
    [SerializeField] private GameObject charStatUI;
    public enum EMenuState
    {
        MainCity = 0,
        MainMenu = 1,
        CharSelectMenu = 2,
        CharStatMenu = 3,
        EquipmentMenu = 4,
        InventoryMenu = 5,
        PauseMenu = 6,
        ShopMenu = 7,
        GameOffMenu = 8
    };

    private EMenuState menuState;

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
                menuState = EMenuState.MainMenu;
            }

            else
            {
                TurnOffMenuByMenuStateEnum();
            }
        }
    }

    #region ���θ޴�UI �Ѱ� ���� �޼ҵ�

    private void Start_bg() // �޴��� ������ ��� ó�� ȿ�� �޼ҵ�
    {
        isOpened = true;

        foreach (MovePanel movePanel in movePanels)
        {
            movePanel.GoToTargetPos();
        }
        StartClean_bg();
        BGVisualizeEFF();
    }



    public void End_bg() // �޴��� ���� ȿ��
    {
        isOpened = false;
        DeactivateAllUI();
        videoPlayer.gameObject.SetActive(false);
        ReturnDark_bg();

        foreach (MovePanel movePanel in movePanels)
        {
            movePanel.GoToOriginPos();
        }

        BGFadeEFF();
    }
    #endregion

    #region ��׶���UI ȿ��
    public void ChangeToOtherMenuEFF() // �޴����� �ٸ� �޴��� Ŭ������ �� ȿ���Դϴ�. (�� : ĳ���� ���� Ŭ���� -> ĳ���� ���� UI�� �̵�)
    {
        ReturnDark_bg();
        BGFadeEFF();
    }
    private void StartClean_bg() // Clean_bg_co() �ڷ�ƾ ���� �޼ҵ�
    {
        StartCoroutine(Clean_bg_co());
    }

    private IEnumerator Clean_bg_co() // ����� �����ð� ���� ���ֱ�
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
    private void ReturnDark_bg() // � ȭ������ �ٲٱ�
    {
        Color color = fadeImage_BG.color;
        color.a = 0.8f;
        fadeImage_BG.color = color;
    }

    private void BGVisualizeEFF() // ȭ�� ��İ� �ϴ� ȿ��
    {
        if (tween != null)
        {
            tween.Kill();
        }

        tween = fadeImage_BG.DOFade(1f, duration_BG).OnComplete(StartClean_bg);
    }

    private void BGFadeEFF() // ȭ�� ������� �ϴ� ȿ��
    {
        if (tween != null)
        {
            tween.Kill();
        }

        tween = fadeImage_BG.DOFade(0f, duration_BG);
    }

    #endregion

    public void TurnOffMenuByMenuStateEnum() // �޴��� ���� ���¿� ���� �̵��ϴ� �޼ҵ��Դϴ�.
    {
        switch (menuState)
        {
            case EMenuState.MainMenu:
                End_bg();
                DeactivateAllUI();
                CharSelectUI.SetActive(false);
                menuState = EMenuState.MainCity;
                break;

            case EMenuState.CharSelectMenu:
                ChangeToOtherMenuEFF();
                CharSelectUI.SetActive(false);
                menuState = EMenuState.MainMenu;
                break;

            case EMenuState.CharStatMenu:
                ChangeToOtherMenuEFF();
                charStatUI.SetActive(false);
                menuState = EMenuState.CharSelectMenu;
                break;

            case EMenuState.EquipmentMenu:
                ChangeToOtherMenuEFF();
                equipmentUI.SetActive(false);
                menuState = EMenuState.CharSelectMenu;
                break;
            case EMenuState.InventoryMenu:
                ChangeToOtherMenuEFF();
                inVentoryUI.SetActive(false); 
                menuState = EMenuState.MainMenu;
                break;

            case EMenuState.PauseMenu:
                menuState = EMenuState.MainMenu;
                Debug.Log($"{menuState} ���� �ʿ�.");
                break;

            case EMenuState.ShopMenu:
                menuState = EMenuState.MainMenu;
                Debug.Log($"{menuState} ���� �ʿ�.");
                break;

            case EMenuState.GameOffMenu:
                menuState = EMenuState.MainMenu;
                Debug.Log($"{menuState} ���� �ʿ�.");
                break;

            default:
                Debug.Log($"�׷� {menuState} UI ȯ���� �����ϴ�.");
                break;
        }
    }

    public void DeactivateAllUI() // ��� UI �� ���� ����
    {
        CharSelectUI.SetActive(false);
        charStatUI.SetActive(false);
        equipmentUI.SetActive(false);
        inVentoryUI.SetActive(false);
        menuState = EMenuState.MainCity;
        End_bg();
    }
        //******************************************************************************************
    }
