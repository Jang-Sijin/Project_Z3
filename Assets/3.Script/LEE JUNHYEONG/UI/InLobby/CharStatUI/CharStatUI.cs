using JSJ;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharStatUI : MonoBehaviour
{
    [SerializeField] private CharselectBtnManager charselectBtnManager;

    [Header("���� �޴� UI �޴���")]
    [SerializeField] private MainCityMenuUIManager mainCityMenuUIManager;


    [Header("���� UI��")]
    [SerializeField] private MovePanel charLevelUpUI; // ĳ���� ������ UI
    [SerializeField] private MovePanel charPromoteUI; // ĳ���� ���� UI

    [SerializeField] private GameObject sideUIBG; // �� ���

    [SerializeField] private GameObject[] factions; // ����� ���� 

    #region �� �ؽ�Ʈ��
    [Header("�� �ؽ�Ʈ��")]
    [SerializeField] private TextMeshProUGUI charNameText; // ĳ������ �̸� ���
    [SerializeField] private TextMeshProUGUI charLevelText; // ĳ���� ���� ���
    [SerializeField] private TextMeshProUGUI charMaxLevelText; // ĳ���� �ִ� ���� ���
    [SerializeField] private TextMeshProUGUI charHPText; // ĳ���� ü�� ���
    [SerializeField] private TextMeshProUGUI charDefenceText; // ĳ���� ���� ���
    [SerializeField] private TextMeshProUGUI charDMGText; // ĳ���� ������ ���
    #endregion
    private readonly string[] charkoreanName = // ĳ���� �ѱ� �̸� ����� ���� ����
    {
        "�ڸ� ��ũ��",
        "�غ� ������",
        "11ȣ"
    };

    private void OnEnable()
    {
        PrintCharStat();
    }

    public void PrintCharStat() // ĳ������ ������ ����մϴ�.
    {
        for (int i = 0; i < factions.Length; i++)
        {
            factions[i].SetActive(false);
        }

        factions[(int)charselectBtnManager.PrevCharBtn.eCharacter].SetActive(true); // ĳ���� �Ѽ� ���
        charNameText.text = charkoreanName[(int)charselectBtnManager.PrevCharBtn.eCharacter]; // ĳ���� �̸� ���
        Debug.Log("���� �÷��̾� �ε��� : " + (int)charselectBtnManager.PrevCharBtn.eCharacter + " / ĳ���� �迭 ����" + InventoryManager.instance.debugCharInfo.Length);
        charLevelText.text = $"LV.{InventoryManager.instance.debugCharInfo[(int)charselectBtnManager.PrevCharBtn.eCharacter].actualLevel}"; // ĳ���� ���� ��� // ������ ���� �ʿ�
        charMaxLevelText.text = InventoryManager.instance.debugCharInfo[(int)charselectBtnManager.PrevCharBtn.eCharacter].actualMaxLevel.ToString(); // ĳ���� ���� ��� // ������ ���� �ʿ�

        if (PlayerController.INSTANCE != null)
        {
            charHPText.text = ((int)PlayerController.INSTANCE.
            controllableModels[(int)charselectBtnManager.PrevCharBtn.eCharacter].
            characterInfo.maxHealth).ToString(); // ü�� ���
            charDMGText.text = ((int)PlayerController.INSTANCE.controllableModels
                [(int)charselectBtnManager.PrevCharBtn.eCharacter].characterInfo.
                defaultAttackDamage).ToString(); // ������ ���
        }

        charDefenceText.text = InventoryManager.instance.debugCharInfo[(int)charselectBtnManager.PrevCharBtn.eCharacter].actualDEF.ToString();  // ���� info�� defence ���� ���� ������
    }

    public void OnClickLevelUpBTN()
    {
        if (InventoryManager.instance.debugCharInfo[(int)charselectBtnManager.PrevCharBtn.eCharacter].actualLevel >= 50)
        {
            return;
        }

        else if ( //���� ������ ���� ���Ѽ��� ���ٸ� ĳ���� ����â�� ����
            InventoryManager.instance.debugCharInfo[(int)charselectBtnManager.PrevCharBtn.eCharacter].actualLevel.Equals(
                InventoryManager.instance.debugCharInfo[(int)charselectBtnManager.PrevCharBtn.eCharacter].actualMaxLevel)
            )
        {
            charPromoteUI.gameObject.SetActive(true);
            charPromoteUI.GoToTargetPos();
        mainCityMenuUIManager.emenuState = MainCityMenuUIManager.EMenuState.CharPromteMenu;
        }
        
        else
        {
            charLevelUpUI.gameObject.SetActive(true);
            charLevelUpUI.GoToTargetPos();
            mainCityMenuUIManager.emenuState = MainCityMenuUIManager.EMenuState.CharLevelUpMenu;
        }
        
        sideUIBG.gameObject.SetActive(true);
    }

    public void TurnOffSideUIBG()
    {
        mainCityMenuUIManager.emenuState = MainCityMenuUIManager.EMenuState.CharStatMenu;
        sideUIBG.gameObject.SetActive(false);
    }
}
