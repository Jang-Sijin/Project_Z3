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
    [SerializeField] public MovePanel charLevelUpUI;
    [SerializeField] public MovePanel charPromoteUI;

    private int[] charLevels; // ������ : ĳ���� ����
    private int[] charMaxLevels = {20, 20, 20}; // ������ : ĳ���� �ִ� 

    [Header("�� �ؽ�Ʈ��")]
    [SerializeField] private GameObject[] factions; // ����� ���� 
    [SerializeField] private TextMeshProUGUI charNameText; // ĳ������ �̸� ���
    [SerializeField] private TextMeshProUGUI charLevelText; // ĳ���� ���� ���
    [SerializeField] private TextMeshProUGUI charMaxLevelText; // ĳ���� �ִ� ���� ���
    [SerializeField] private TextMeshProUGUI charHPText; // ĳ���� ü�� ���
    [SerializeField] private TextMeshProUGUI charDefenceText; // ĳ���� ���� ���
    [SerializeField] private TextMeshProUGUI charDMGText; // ĳ���� ������ ���

    private string[] charkoreanName = // ĳ���� �ѱ� �̸� ����� ���� ����
    {
        "�ڸ� ��ũ��",
        "�غ� ������",
        "11ȣ"
    };

    private void Awake() // ������ 
    {
        charLevels = new int[3];// ������

        for (int i = 0; i < charLevels.Length; i++)// ������
        {
            charLevels[i] = 18 + i;
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < factions.Length; i++)
        {
            factions[i].SetActive(false);
        }

        factions[(int)charselectBtnManager.PrevCharBtn.eCharacter].SetActive(true); // ĳ���� �Ѽ� ���
        charNameText.text = charkoreanName[(int)charselectBtnManager.PrevCharBtn.eCharacter]; // ĳ���� �̸� ���
        charLevelText.text = $"LV.{charLevels[(int)charselectBtnManager.PrevCharBtn.eCharacter]}"; // ĳ���� ���� ��� // ������ ���� �ʿ�
        charMaxLevelText.text = charMaxLevels[(int)charselectBtnManager.PrevCharBtn.eCharacter].ToString(); // ĳ���� ���� ��� // ������ ���� �ʿ�

        charHPText.text = ((int)PlayerController.INSTANCE.
            controllableModels[(int)charselectBtnManager.PrevCharBtn.eCharacter].
            characterInfo.maxHealth).ToString(); // ü�� ���

        charDefenceText.text = "12";  // ���� info�� defence ���� ���� ������

        charDMGText.text = ((int)PlayerController.INSTANCE.controllableModels
            [(int)charselectBtnManager.PrevCharBtn.eCharacter].characterInfo.
            defaultAttackDamage).ToString(); // ������ ���
    }

    public void OnClickLevelUpBTN()
    {
        charPromoteUI.gameObject.SetActive(true);
        charPromoteUI.GoToTargetPos();
        mainCityMenuUIManager.emenuState = MainCityMenuUIManager.EMenuState.CharPromteMenu;
    }


}
