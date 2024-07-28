using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;

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
    private int[] item_Amount = new int[2]; // ���� ������ ��ȭ ������ ĳ��

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

    private int[] clickedItem_Amount = new int[2]; // ������ ���� ������ ����
    //item�� ��ũ�� �ε��� ����
    //ex : S��ũ �������� 0�� A��ũ ������ 1��

    //**************************************************************************************************************

    #region ���� ����ġ �ϵ� �ڵ�
    private float addHP = 50f; //ü��
    private float addDMG = 5f; //������
    private float addDEF = 5f; //����
    private int addEXPLimit = 100; //���� ���Ѽ�
    #endregion

    /*
     * ������ ��� �̸� ����ϴ� �޼ҵ�
     * ������ �̸� ��� �� �̹��� fill�ٲٴ� �޼ҵ�
     * ���� �͵� �����ؾ���
     * 
     * ������ �� ���� �ؽ�Ʈ��
     * ü��, 
     */

    private void OnEnable()
    {
        meCharacter = lastChar.PrevCharBtn.eCharacter;
        InitExpectValues();
        AssignItemAmountFromInventory();
        PrintActualStats();
        PrintBlankTextToExpectedText();
    }



    #region �ؽ�Ʈ ��� �޼ҵ�
    public void PrintActualStats() // ������ �� Ȥ�� �ʱ�ȭ�� �Ҹ� �޼ҵ�
    {
        // ���� ü��
        HPText[0].text = PlayerController.INSTANCE?.controllableModels[
            (int)meCharacter].characterInfo.maxHealth.ToString();
        // ���� ���ݷ�
        DMGText[0].text = PlayerController.INSTANCE?.controllableModels[
            (int)meCharacter].characterInfo.defaultAttackDamage.ToString();
        // ���� ����
        DEFText[0].text = ((int)InventoryManager.instance.debugCharInfo[
            (int)meCharacter].actualDEF).ToString(); // ������ ���� �ʿ�

        // ���� ����
        curLevelText.text = InventoryManager.instance.debugCharInfo[
            (int)meCharacter].actualLevel.ToString(); // ������ ���� �ʿ�

        nextLevelText.text = (InventoryManager.instance.debugCharInfo[
            (int)meCharacter].actualLevel + 1).ToString(); // ������ ���� �ʿ�

        // ���� exp
        amountOfEXPText.text = InventoryManager.instance.debugCharInfo[(int)meCharacter].actualcurEXP.ToString() + "/" + 
            InventoryManager.instance.debugCharInfo[(int)meCharacter].actualmaxEXP.ToString(); //������ ���� �ʿ�

        // �������� ����
        amountOfItemSText.text = "0 /" + item_Amount[(int)Item.EItemRank.S].ToString();
        amountOfItemAText.text = "0 /" + item_Amount[(int)Item.EItemRank.A].ToString();

        EXPGageIMG.fillAmount = 0f;
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
        expectedCurEXP = InventoryManager.instance.debugCharInfo[(int)meCharacter].actualcurEXP;
        expectedMaxEXP = InventoryManager.instance.debugCharInfo[(int)meCharacter].actualmaxEXP;

        expectedCurLevel = InventoryManager.instance.debugCharInfo[(int)meCharacter].actualLevel;

        Debug.Log(meCharacter + InventoryManager.instance.debugCharInfo[(int)meCharacter].actualLevel);
        Debug.Log("expected" + expectedCurLevel);
        

        for (int i = 0; i < clickedItem_Amount.Length; i++)
        {
            clickedItem_Amount[i] = 0;
        }

        EXPGageIMG.fillAmount = 0f;
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

    public void CalculateExpectedLevelUp(Item item) // �������� �ް������� �ް� ����ġ�� �����ִ� �޼ҵ� �������� ���� �� �ش� ��ġ�� ���� ĳ���� �ɷ¿� �ݿ�
    {


        if (clickedItem_Amount[(int)item.rank] >= item_Amount[(int)item.rank]) // Ŭ���� �������� ������ �� ���� ���ٸ� return
        {
            clickedItem_Amount[(int)item.rank] = item_Amount[(int)item.rank];
            return;
        }

        itemCancleBTN[(int)item.rank].SetActive(true); // ������ ��� ��ư

        expectedCurEXP += item.stat; // ���� ����ġ exp�� ���ϱ�
        clickedItem_Amount[(int)item.rank] += 1; // Ŭ���� ������ 1�� �ø���

        int tweeningCounter = 0;

        while (expectedCurEXP >= expectedMaxEXP) // ������ ���Ѽ� ������ �� �� ����
        {
            expectedCurLevel += 1;
            Debug.Log(expectedCurLevel);
            expectedCurEXP = expectedCurEXP - expectedMaxEXP;
            expectedMaxEXP += addEXPLimit;

            expectedHP += addHP;
            expectedDMG += addDMG;
            expectedDEF += addDEF;

            tweeningCounter += 1;
        }

        if (EXPUpTween != null) EXPUpTween.Done();
        if(EXPUpTween2 != null) EXPUpTween2.Done();

        if (tweeningCounter > 0)
        {
            EXPUpTween = EXPGageIMG.DOFillAmount(1, 0.2f).OnStepComplete(() => EXPGageIMG.fillAmount = 0f).SetLoops(tweeningCounter).OnComplete(() =>
            EXPUpTween2 = EXPGageIMG.DOFillAmount(expectedCurEXP / expectedMaxEXP, 0.2f)); // Ʈ���� ����
        }

        else
            EXPUpTween2 = EXPGageIMG.DOFillAmount(expectedCurEXP / expectedMaxEXP, 0.2f); // ��ǥ fill���� Ʈ����

        PrintExpectValues();
    }

    public void RemoveExpectedLevelUp(Item item)
    {
        if (clickedItem_Amount[(int)item.rank] <= 0)
        {
            PrintBlankTextToExpectedText();
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

        if (EXPUpTween != null) EXPUpTween.Done();
        if(EXPUpTween2 != null) EXPUpTween2.Done();

        if (tweeningCounter > 0)
        {
            Debug.Log(tweeningCounter);

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
    #endregion

    #region ��ư �Է� �޼ҵ�


    #endregion

    private void OnDisable()
    {
        PrintActualStats();
    }
}
