using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class AgentPrmoteUI : MonoBehaviour
{
    #region ���� �ؽ�Ʈ
    [Header("���� �ؽ�Ʈ")]
    [SerializeField] private TextMeshProUGUI[] HPText; //ü�� �ؽ�Ʈ // index = 0 , ���� �ɷ�ġ �ؽ�Ʈ/ index = 1, ���� �ɷ�ġ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI[] DMGText; //������ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI[] DEFText; //���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI[] curLevel; // ���� ���� �ؽ�Ʈ index = 0�� ���� ���� 1�� ���� ���Ѽ�
    [SerializeField] private TextMeshProUGUI[] nextLevel; // ���� ���� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI costText; // ����ǥ �ؽ�Ʈ

    [Header("���� ������ �ؽ�Ʈ & �̹���")]
    [SerializeField] private Image itemIMG; // ������ �̹���
    [SerializeField] private TextMeshProUGUI ItemAmountText; // ���� �������� ���� �ؽ�Ʈ
    [SerializeField] private Animator itemGageAni;
    #endregion

    #region ���� ����ġ �ϵ��ڵ�
    private readonly float addHP = 100f; // ���Ľ� ü�� 100�߰�
    private readonly float addDMG = 10f; // ���Ľ� ������ 10�߰�
    private readonly float addDEF = 10f; // ���Ľ� ���� 10�߰�
    private readonly int addMaxLevel = 10; // ���Ľ� ���� ���� 10�߰�
    private readonly int cost = 5000; // ���� ȭ��

    private readonly int[] amountOfRequireItem = { 2, 4, 4, 6 }; // ���޽� �ʿ��� ������ ��
    // 20���� : A������ 2��
    // 30���� : A������ 4��
    // 40���� : S������ 4��
    // 50���� : S������ 6��

    private int requireIndex;

    [Header ("���� ������")]
    [SerializeField]private Item itemA;
    [SerializeField]private Item itemS;
    private Item BTNitem;
    #endregion

    #region ���� ĳ�� ����
    private int amountOfItem;
    [SerializeField] private CharselectBtnManager charselectBtnManager;
    private ECharacter mECharacter;
    private MovePanel movePanel;
    private CharStatUI charStatUI;
    #endregion

    #region ��� �޼ҵ�

    private void Awake()
    {
        movePanel = GetComponent<MovePanel>();
        charStatUI = GetComponentInParent<CharStatUI>();
    }

    private void OnEnable()
    {
        mECharacter = charselectBtnManager.PrevCharBtn.eCharacter;
        AssignAmountOfItem();
        PrintStats(PlayerController.INSTANCE.controllableModels[(int)mECharacter].characterInfo, InventoryManager.instance.debugCharInfo[(int)mECharacter]);
        itemGageAni.SetTrigger("Normal");
    }

    private void PrintStats(CharacterInfo characterInfo, InventoryManager.DebugCharInfo debugCharInfo) // �ʱ�ȭ Ȥ�� ���� �� ��� �ؽ�Ʈ ��� �޼ҵ�
    {
        if (characterInfo != null)
        {
            HPText[0].text = ((int)characterInfo.maxHealth).ToString();
            HPText[1].text = ((int)addHP).ToString();
            DMGText[0].text = ((int)characterInfo.defaultAttackDamage).ToString();
            DMGText[1].text = ((int)addDMG).ToString();
        }

        DEFText[0].text = ((int)debugCharInfo.actualDEF).ToString();
        DEFText[1].text = ((int)addDEF).ToString();

        curLevel[0].text = debugCharInfo.actualLevel.ToString();
        curLevel[1].text = " / " + debugCharInfo.actualMaxLevel.ToString();

        nextLevel[0].text = debugCharInfo.actualLevel.ToString();
        nextLevel[1].text = " / " + (debugCharInfo.actualMaxLevel + addMaxLevel).ToString();

        costText.text = $"{InventoryManager.instance.Wallet} / {cost}";

        switch (debugCharInfo.actualMaxLevel)
        {
            case 20:
                itemIMG.sprite = itemA.itemIcon;
                ItemAmountText.text = $"{amountOfItem} / {amountOfRequireItem[0]}";
                requireIndex = 0;
                break;

            case 30:
                itemIMG.sprite = itemA.itemIcon;
                ItemAmountText.text = $"{amountOfItem} / {amountOfRequireItem[1]}";
                requireIndex = 1;
                break;

            case 40:
                itemIMG.sprite = itemS.itemIcon;
                ItemAmountText.text = $"{amountOfItem} /  {amountOfRequireItem[2]}";
                requireIndex = 2;
                break;

            case 50:
                itemIMG.sprite = itemS.itemIcon;
                ItemAmountText.text = $"{amountOfItem} /  {amountOfRequireItem[3]}";
                requireIndex = 3;
                break;

            default:
                Debug.Log("���������� ���� ����");
                break;
        }
    }
    #endregion

    #region ���� ��� �޼ҵ�
    private void AssignAmountOfItem() // �������� �� ���� ĳ��
    {
        switch (InventoryManager.instance.debugCharInfo[(int)mECharacter].actualMaxLevel)
        {
            case 20:
                amountOfItem = InventoryManager.instance.GetAmountOfItemByItem(itemA);
                BTNitem = itemA;
                break;

            case 30:
                amountOfItem = InventoryManager.instance.GetAmountOfItemByItem(itemA);
                BTNitem = itemA;
                break;

            case 40:
                amountOfItem = InventoryManager.instance.GetAmountOfItemByItem(itemS);
                BTNitem = itemS;
                break;

            case 50:
                amountOfItem = InventoryManager.instance.GetAmountOfItemByItem(itemS);
                BTNitem = itemS;
                break;

            default:
                Debug.Log("���������� ���� �����Դϴ�.");
                break;
        }
    }

    private void AssignValueToCharInfo() // ������ ���� ĳ���Ϳ� ����
    {
        if (PlayerController.INSTANCE != null)
        {
            PlayerController.INSTANCE.controllableModels[(int)mECharacter].characterInfo.maxHealth += addHP;
            PlayerController.INSTANCE.controllableModels[(int)mECharacter].characterInfo.defaultAttackDamage += addDMG;    
            PlayerController.INSTANCE.controllableModels[(int)mECharacter].characterInfo.exSkillDamage += addDMG;
        }
            InventoryManager.instance.debugCharInfo[(int)mECharacter].actualDEF += addDEF;
            InventoryManager.instance.debugCharInfo[(int)mECharacter].actualMaxLevel += addMaxLevel;
    }
    #endregion


    #region ��ư �Է� �޼ҵ�
    public void OnClickPromoteBTN() // ���� ��ư �޼ҵ�
    {
        if (amountOfItem < amountOfRequireItem[requireIndex] || InventoryManager.instance.Wallet < cost)
        {
            return;
        }

        StartCoroutine(OnClickPromote_co());
    }

    private IEnumerator OnClickPromote_co() // ���� ���� �� ���� ĳ���Ϳ� ȿ�� ����
    {
        itemGageAni.SetTrigger("Pressed");
        WaitForSeconds wfs = new WaitForSeconds(itemGageAni.GetCurrentAnimatorStateInfo(0).normalizedTime);

        InventoryManager.instance.RemoveItemsByAmount(BTNitem, amountOfRequireItem[requireIndex]);
        InventoryManager.instance.RemoveMoneyFromWallet(cost);

        AssignValueToCharInfo();
        AssignAmountOfItem();
        PrintStats(PlayerController.INSTANCE.controllableModels[(int)mECharacter].characterInfo, InventoryManager.instance.debugCharInfo[(int)mECharacter]);

        yield return wfs;

        charStatUI.PrintCharStat();
        charStatUI.TurnOffSideUIBG();
        movePanel.GoToEndPos();
    }

    public void OnClickClose() // â �ݱ�
    {
        charStatUI.TurnOffSideUIBG();
        movePanel.GoToEndPos();
    }
    #endregion
}
