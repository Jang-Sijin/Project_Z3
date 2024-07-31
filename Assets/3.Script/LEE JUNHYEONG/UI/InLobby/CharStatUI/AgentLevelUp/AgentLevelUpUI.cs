using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using static InventoryManager;
using static Define;

public class AgentLevelUpUI : MonoBehaviour
{
    #region �����ؽ�Ʈ

    [Header("���� �ؽ�Ʈ")]
    //**************************************************************************************************************
    // index -> 0 �� ���� ������ index -> 1�� ������ �� ��ġ��
    [SerializeField] private TextMeshProUGUI[] HPText; // ü�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI[] DMGText; // ������ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI[] DEFText; // ���� �ؽ�Ʈ
    //**************************************************************************************************************

    [SerializeField] private TextMeshProUGUI curLevelText; // ���緹�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI nextLevelText; // �������� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI amountOfEXPText; // (���� ����ġ / ���� ���� ����ġ) ����ġ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI amountOfItemAText; // A��ũ ����ġ ������ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI amountOfItemSText; // S��ũ ����ġ ������ �ؽ�Ʈ

    [Header("EXP������")]
    [SerializeField] private Image EXPGageIMG; // exp ������
    //**************************************************************************************************************
    #endregion

    [Header ("���õ� ĳ���� ĳ��")]
    [SerializeField] private CharselectBtnManager lastChar; // ���õ� ĳ���͸� ���� ĳ��
    private ECharacter meCharacter; // ���õ� ĳ���� ĳ��
    private readonly int[] item_Amount = new int[2]; // ���� ������ ��ȭ ������ ĳ��

    [Header("������ ��� ��ư")]
    [SerializeField] private GameObject[] itemCancleBTN;// Item.EitemRank�� �ε��� ����

    //**************************************************************************************************************
   
    private float expectedCurEXP; // ���� ���� exp
    private float expectedMaxEXP; // ���� ���� ���� �ִ� exp

    private float expectedHP = 0f; // ���� ü��
    private float expectedDMG = 0f; // ���� ���ݷ�
    private float expectedDEF = 0f; // ���� ����
    private int expectedCurLevel = 0; // ���� ����
    private readonly string blank = " "; // ���� ���� ��ĭ���� �ʱ�ȭ

    private readonly int[] clickedItem_Amount = new int[2]; // ������ ���� ������ ����
    //item�� ��ũ�� �ε��� ����
    //ex : S��ũ �������� 0�� A��ũ ������ 1��

    private MovePanel movePanel; // ����� �ʿ�
    private CharStatUI charStatUI; // ����UI�� �ٲ� ���� ����
    //**************************************************************************************************************

    #region ���� ����ġ �ϵ� �ڵ�
    private readonly float addHP = 50f; //ü��
    private readonly float addDMG = 5f; //������
    private readonly float addDEF = 5f; //����
    private readonly int addEXPLimit = 100; //���� ���Ѽ�
    #endregion

    /*
     * ������ ��� �̸� ����ϴ� �޼ҵ�
     * ������ �̸� ��� �� �̹��� fill�ٲٴ� �޼ҵ�
     * ���� �͵� �����ؾ���
     * 
     * ������ �� ���� �ؽ�Ʈ��
     * ü��, 
     */
    private void Awake()
    {
        movePanel = GetComponent<MovePanel>();
        charStatUI = GetComponentInParent<CharStatUI>();
    }

    private void OnEnable()
    {
        meCharacter = lastChar.PrevCharBtn.eCharacter;
        InitExpectValues();
        AssignItemAmountFromInventory();
        PrintActualStats(PlayerController.INSTANCE.controllableModels[(int)meCharacter].characterInfo, InventoryManager.instance.debugCharInfo[(int)meCharacter]);
        PrintBlankTextToExpectedText();
    }



    #region �ؽ�Ʈ ��� �޼ҵ�
    public void PrintActualStats(CharacterInfo characterInfo, InventoryManager.DebugCharInfo debugCharInfo) // ������ �� Ȥ�� �ʱ�ȭ�� �Ҹ� �޼ҵ�
    {
        if(characterInfo != null)
        { // ���� ü��
        HPText[0].text = ((int)characterInfo.maxHealth).ToString();
        // ���� ���ݷ�
        DMGText[0].text = ((int)characterInfo.defaultAttackDamage).ToString();
        }
        // ���� ����
        DEFText[0].text = ((int)debugCharInfo.actualDEF).ToString(); // ������ ���� �ʿ�

        // ���� ����
        curLevelText.text = debugCharInfo.actualLevel.ToString(); // ������ ���� �ʿ�

        nextLevelText.text = (debugCharInfo.actualLevel + 1).ToString(); // ������ ���� �ʿ�

        // ���� exp
        amountOfEXPText.text = debugCharInfo.actualcurEXP.ToString() + "/" + 
            debugCharInfo.actualmaxEXP.ToString(); //������ ���� �ʿ�

        // �������� ����
        amountOfItemSText.text = "0 /" + item_Amount[(int)Item.EItemRank.S].ToString();
        amountOfItemAText.text = "0 /" + item_Amount[(int)Item.EItemRank.A].ToString();

        EXPGageIMG.fillAmount = debugCharInfo.actualcurEXP / debugCharInfo.actualmaxEXP;
    }

    private void PrintExpectValues() // ���ο� ���� ��ġ ��½� ȣ��
    {
        HPText[1].text = "+" + ((int)expectedHP).ToString();
        DMGText[1].text = "+" + ((int)expectedDMG).ToString();
        DEFText[1].text = "+" + ((int)expectedDEF).ToString();

        curLevelText.text = ((int)expectedCurLevel).ToString();
        nextLevelText.text = ((int)expectedCurLevel + 1).ToString();
        amountOfEXPText.text = $"{((int)expectedCurEXP)} / {(int)expectedMaxEXP}";
        amountOfItemAText.text = $"{clickedItem_Amount[(int)Item.EItemRank.A]}/{item_Amount[(int)Item.EItemRank.A]}";
        amountOfItemSText.text = $"{clickedItem_Amount[(int)Item.EItemRank.S]}/{item_Amount[(int)Item.EItemRank.S]}";
    }

    public void PrintBlankTextToExpectedText() // �� �ؽ�Ʈ ��� �޼ҵ�
    {
        //*************************
        // ���� ��ġ ��ĭ���� �ʱ�ȭ
        HPText[1].text = blank;
        DMGText[1].text = blank;
        DEFText[1].text = blank;
        //*************************

        for (int i = 0; i < itemCancleBTN.Length; i++)
        {
            itemCancleBTN[i].SetActive(false);
        }
    }

    #endregion

    #region ������ ��� �޼ҵ�
    private void InitExpectValues() // ���� ������ �ʱ�ȭ �޼ҵ�
    {
        expectedHP = 0f;
        expectedDEF = 0f;
        expectedDMG = 0f;

        expectedCurEXP = InventoryManager.instance.debugCharInfo[(int)meCharacter].actualcurEXP;
        expectedMaxEXP = InventoryManager.instance.debugCharInfo[(int)meCharacter].actualmaxEXP;

        expectedCurLevel = InventoryManager.instance.debugCharInfo[(int)meCharacter].actualLevel;
        

        for (int i = 0; i < clickedItem_Amount.Length; i++)
        {
            clickedItem_Amount[i] = 0;
        }

        EXPGageIMG.fillAmount = expectedCurEXP / expectedMaxEXP;
    }
    private void AssignItemAmountFromInventory() // ���� ������ ���� ĳ�� �޼ҵ�
    {
        for (int i = 0; i < item_Amount.Length; i++)
        {
            item_Amount[i] = InventoryManager.instance.GetAmountOfItemByTypeAndRank(Item.EItemType.EXP_CHARACTER, (Item.EItemRank)i);
        }
    }

    Tween EXPUpTween; // ù Ʈ���� ĳ��
    Tween EXPUpTween2; // �� ��° Ʈ���� ĳ��
    private void CalculateExpectedLevelUp(Item item) // �������� �ް������� �ް� ����ġ�� �����ִ� �޼ҵ�
    {
        if (expectedCurLevel >= InventoryManager.instance.debugCharInfo[(int)meCharacter].actualMaxLevel)
        {
            return;
        }

        if (clickedItem_Amount[(int)item.rank] >= item_Amount[(int)item.rank]) // Ŭ���� �������� ������ �� ���� ���ٸ� return
        {
            clickedItem_Amount[(int)item.rank] = item_Amount[(int)item.rank];
            return;
        }

        itemCancleBTN[(int)item.rank].SetActive(true); // ������ ��� ��ư

        expectedCurEXP += item.stat; // ���� ����ġ exp�� ���ϱ�
        clickedItem_Amount[(int)item.rank] += 1; // Ŭ���� ������ 1�� �ø���

        int tweeningCounter = 0;

        EXPUpTween?.Kill();
        EXPUpTween2?.Kill();

        while (expectedCurEXP >= expectedMaxEXP) // ������ ���Ѽ� ������ �� �� ����
        {

            expectedCurLevel += 1;


            expectedCurEXP -= expectedMaxEXP;
            expectedMaxEXP += addEXPLimit;

            expectedHP += addHP;
            expectedDMG += addDMG;
            expectedDEF += addDEF;

            tweeningCounter += 1;

            if (expectedCurLevel >= InventoryManager.instance.debugCharInfo[(int)meCharacter].actualMaxLevel)
            {
                Debug.Log("���Ѽ� �̻��� ����");
                EXPUpTween = EXPGageIMG.DOFillAmount(1, 0.2f).OnStepComplete(() => EXPGageIMG.fillAmount = 0f).SetLoops(tweeningCounter).OnComplete(() => EXPGageIMG.fillAmount = 0f); // Ʈ���� ����
                PrintExpectValues();
                amountOfEXPText.text = $"0 / {(int)expectedMaxEXP}";
                return;
            }
        }


        if (tweeningCounter > 0)
        {
            EXPUpTween = EXPGageIMG.DOFillAmount(1, 0.2f).OnStepComplete(() => EXPGageIMG.fillAmount = 0f).SetLoops(tweeningCounter).OnComplete(() =>
            EXPUpTween2 = EXPGageIMG.DOFillAmount(expectedCurEXP / expectedMaxEXP, 0.2f)); // Ʈ���� ����
        }

        else
        {
            EXPUpTween2 = EXPGageIMG.DOFillAmount(expectedCurEXP / expectedMaxEXP, 0.2f); // ��ǥ fill���� Ʈ����
        }

        PrintExpectValues();
    }
    private void RemoveExpectedLevelUp(Item item) // ������ �������� ���� ��꿡�� ����
    {

        if (clickedItem_Amount[(int)item.rank] <= 0)
        {
            clickedItem_Amount[(int)item.rank] = 0;
            return;
        }

        clickedItem_Amount[(int)item.rank] -= 1;

        if (clickedItem_Amount[(int)item.rank] <= 0)
        {
            itemCancleBTN[(int)item.rank].SetActive(false); // ������ ��� ��ư
        }

        expectedCurEXP -= item.stat;

        int tweeningCounter = 0;

        while (expectedCurEXP < 0)
        {
            expectedCurLevel -= 1;
            Debug.Log(expectedCurLevel);
            expectedMaxEXP -= addEXPLimit;
            expectedCurEXP += expectedMaxEXP;

            expectedHP -= addHP;
            expectedDMG -= addDMG;
            expectedDEF -= addDEF;

            tweeningCounter += 1;
        }

        EXPUpTween?.Kill();
        EXPUpTween2?.Kill();

        if (tweeningCounter > 0)
        {
            if (tweeningCounter > 2)
            {
                EXPGageIMG.fillAmount = 1f;
                tweeningCounter -= 1;
            }

            EXPUpTween = EXPGageIMG.DOFillAmount(0, 0.2f).OnStepComplete(() => EXPGageIMG.fillAmount = 1f).SetLoops(tweeningCounter).OnComplete(() =>
            EXPUpTween2 = EXPGageIMG.DOFillAmount(expectedCurEXP / expectedMaxEXP, 0.2f));
        }

        else
            EXPUpTween2 = EXPGageIMG.DOFillAmount(expectedCurEXP / expectedMaxEXP, 0.2f); // ��ǥ fill���� Ʈ����

        PrintExpectValues();
    }

    private void ChangeOriginStats(CharacterInfo characterInfo, InventoryManager.DebugCharInfo debugCharInfo) // ���� ������ ���� ���ȿ� ����
    {
        if (characterInfo != null)
        {
            characterInfo.maxHealth += expectedHP;
            characterInfo.defaultAttackDamage += expectedDMG;
            characterInfo.exSkillDamage += expectedHP;
        }

        debugCharInfo.actualDEF += expectedDEF; // ������
        debugCharInfo.actualcurEXP = expectedCurEXP; // �����
        debugCharInfo.actualmaxEXP = expectedMaxEXP; // �����
        debugCharInfo.actualLevel = expectedCurLevel; // �����
            
        Debug.Log($"{meCharacter.ToString()}�� ���� : {debugCharInfo.actualDEF}");
        Debug.Log($"{meCharacter.ToString()}�� ���� EXP : {debugCharInfo.actualcurEXP}");
        Debug.Log($"{meCharacter.ToString()}�� �ִ� EXP : {debugCharInfo.actualmaxEXP}");
        Debug.Log($"{meCharacter.ToString()}�� ���� : {debugCharInfo.actualLevel}");
    }

    private void ChangeItemAmount() // �κ��丮���� �������� ���� �޼ҵ�
    {
        InventoryManager.instance.RemoveItemsByTypeAndRank(Item.EItemType.EXP_CHARACTER, Item.EItemRank.S, clickedItem_Amount[(int)Item.EItemRank.S]);
        InventoryManager.instance.RemoveItemsByTypeAndRank(Item.EItemType.EXP_CHARACTER, Item.EItemRank.A, clickedItem_Amount[(int)Item.EItemRank.A]);
        clickedItem_Amount[0] = 0;
        clickedItem_Amount[1] = 0;
        AssignItemAmountFromInventory();
    }

    #endregion

    #region ��ư �Է� �޼ҵ�

    public void OnClickLevelUpItem(Item item) // ������ ������ Ŭ�� �޼ҵ�
    {
        CalculateExpectedLevelUp(item);
    }

    public void OnClickCancleItem(Item item) // ������ ������ ��� �޼ҵ�
    {
        RemoveExpectedLevelUp(item);
    }

    public void OnClickLevelUp() // ������ ��ư Ŭ�� �޼ҵ�
    {
        if (clickedItem_Amount[0] <= 0 &&
            clickedItem_Amount[1] <= 0)
        {
            return;
        }

        ChangeOriginStats(PlayerController.INSTANCE.controllableModels[(int)meCharacter].characterInfo, InventoryManager.instance.debugCharInfo[(int)meCharacter]); // ���� ��ġ�� �ݿ�
            ChangeItemAmount(); // ������ �κ��丮���� ����
        PrintActualStats(PlayerController.INSTANCE.controllableModels[(int)meCharacter].characterInfo, InventoryManager.instance.debugCharInfo[(int)meCharacter]); // ���� ��ġ ���
            InitExpectValues(); // ���� ����ġ �ʱ�ȭ
            PrintBlankTextToExpectedText(); // ����ġ ��� ����ȭ
            charStatUI.PrintCharStat(); // ���� UI�� ����

        Debug.Log($"{meCharacter.ToString()}�� ���� : {InventoryManager.instance.debugCharInfo[(int)meCharacter].actualDEF}");
        Debug.Log($"{meCharacter.ToString()}�� ���� EXP : {InventoryManager.instance.debugCharInfo[(int)meCharacter].actualcurEXP}");
        Debug.Log($"{meCharacter.ToString()}�� �ִ� EXP : {InventoryManager.instance.debugCharInfo[(int)meCharacter].actualmaxEXP}");
        Debug.Log($"{meCharacter.ToString()}�� ���� : {InventoryManager.instance.debugCharInfo[(int)meCharacter].actualLevel}");

        if (InventoryManager.instance.debugCharInfo[(int)meCharacter].actualLevel.Equals(InventoryManager.instance.debugCharInfo[(int)meCharacter].actualMaxLevel))
        {
            Debug.Log("�� �� ������?");
            movePanel.GoToEndPos();
            charStatUI.TurnOffSideUIBG();

            InventoryManager.instance.debugCharInfo[(int)meCharacter].actualcurEXP = 0; // �Ѱ輱�� �Ѿ��ٸ� exp�� 0���� ����ϴ�.
        }

    }
    #endregion
}
