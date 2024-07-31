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
using TMPro;

public class MainCityMenuUIManager : MonoBehaviour
{

    #region ���� �޴� UI�� ���� ������
    [SerializeField] private Image fadeImage_BG; // ȭ�� ��İ� �ϴ� �̹���
    [SerializeField] private float duration_BG; // ȭ���� ��İ� Ȥ�� ����ϴ� �ð�
    [SerializeField] private GameObject videoPlayer; // ����� ����
    [SerializeField] private TextMeshProUGUI walletText;

    private List<MovePanel> movePanels; // ���̵忡 ������ �г��Դϴ�.
    private bool isOpened = false; // ���� �޴� UI�� ���ȴ��� Ȯ���ϴ� �Ұ�
    private Tween tween; // Ʈ�� ĳ�� : �� Ʈ������ ����ϱ� ���ؼ�
    #endregion

    [Header("���� UI��")]
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
        CharLevelUpMenu = 5,
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
        InventoryManager.instance.moneyAction += PrintWalletOnMainMenuUI;
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
        PrintWalletOnMainMenuUI();
    }



    public void End_bg() // �޴��� ���� ȿ��
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

        if (isOpened)
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
        tween?.Kill();

        tween = fadeImage_BG.DOFade(1f, duration_BG).OnComplete(StartClean_bg);
    }

    private void BGFadeEFF() // ȭ�� ������� �ϴ� ȿ��
    {
        tween?.Kill();

        tween = fadeImage_BG.DOFade(0f, duration_BG);
    }

    #endregion

    public void TurnOffMenuByMenuStateEnum() // �޴��� ���� ���¿� ���� �̵��ϴ� �޼ҵ��Դϴ�.
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
                emenuState = EMenuState.CharStatMenu;
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
                Debug.Log($"{emenuState} ���� �ʿ�.");
                break;

            case EMenuState.ShopMenu:
                emenuState = EMenuState.MainMenu;
                Debug.Log($"{emenuState} ���� �ʿ�.");
                break;

            case EMenuState.GameOffMenu:
                emenuState = EMenuState.MainMenu;
                Debug.Log($"{emenuState} ���� �ʿ�.");
                break;

            default:
                Debug.LogWarning($"�׷� {emenuState} UI ȯ���� �����ϴ�.");
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

    public void DeactivateAllUI() // ��� UI �� ���� ����
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

    public void PrintWalletOnMainMenuUI()
    {
        walletText.text = InventoryManager.instance.Wallet.ToString();
    }

}
