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
    #region 각종텍스트

    [Header("각종 텍스트")]
    //**************************************************************************************************************
    // index -> 0 은 현재 스탯을 index -> 1은 레벨업 후 수치를
    [SerializeField] private TextMeshProUGUI[] HPText; // 체력 텍스트
    [SerializeField] private TextMeshProUGUI[] DMGText; // 데미지 텍스트
    [SerializeField] private TextMeshProUGUI[] DEFText; // 방어력 텍스트
    //**************************************************************************************************************

    [SerializeField] private TextMeshProUGUI curLevelText; // 현재레벨 텍스트
    [SerializeField] private TextMeshProUGUI nextLevelText; // 다음레벨 텍스트
    [SerializeField] private TextMeshProUGUI amountOfEXPText; // (예상 경험치 / 다음 레벨 경험치) 경험치 텍스트
    [SerializeField] private TextMeshProUGUI amountOfItemAText; // A랭크 경험치 아이템 텍스트
    [SerializeField] private TextMeshProUGUI amountOfItemSText; // S랭크 경험치 아이템 텍스트

    [Header("EXP게이지")]
    [SerializeField] private Image EXPGageIMG; // exp 게이지
    //**************************************************************************************************************
    #endregion

    [Header ("선택된 캐릭터 캐싱")]
    [SerializeField] private CharselectBtnManager lastChar; // 선택된 캐릭터를 위한 캐싱
    private ECharacter meCharacter; // 선택된 캐릭터 캐싱
    private readonly int[] item_Amount = new int[2]; // 실제 유저의 강화 아이템 캐싱

    [Header("아이템 취소 버튼")]
    [SerializeField] private GameObject[] itemCancleBTN;// Item.EitemRank로 인덱스 접근

    //**************************************************************************************************************
   
    private float expectedCurEXP; // 예상 현재 exp
    private float expectedMaxEXP; // 예상 현재 레벨 최대 exp

    private float expectedHP = 0f; // 예상 체력
    private float expectedDMG = 0f; // 예상 공격력
    private float expectedDEF = 0f; // 예상 방어력
    private int expectedCurLevel = 0; // 예상 레벨
    private readonly string blank = " "; // 예상 스탯 빈칸으로 초기화

    private readonly int[] clickedItem_Amount = new int[2]; // 유저가 누른 아이템 개수
    //item의 랭크로 인덱스 접근
    //ex : S랭크 아이탬은 0번 A랭크 아이템 1번

    private MovePanel movePanel; // 종료시 필요
    private CharStatUI charStatUI; // 스탯UI에 바뀐 사항 적용
    //**************************************************************************************************************

    #region 렙업 가중치 하드 코딩
    private readonly float addHP = 50f; //체력
    private readonly float addDMG = 5f; //데미지
    private readonly float addDEF = 5f; //방어력
    private readonly int addEXPLimit = 100; //레벨 상한선
    #endregion

    /*
     * 레벨업 계산 미리 계산하는 메소드
     * 레벨업 미리 계산 시 이미지 fill바꾸는 메소드
     * 빼는 것도 가능해야함
     * 
     * 레벨업 시 변할 텍스트들
     * 체력, 
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



    #region 텍스트 출력 메소드
    public void PrintActualStats(CharacterInfo characterInfo, InventoryManager.DebugCharInfo debugCharInfo) // 레벨업 후 혹은 초기화시 불릴 메소드
    {
        if(characterInfo != null)
        { // 실제 체력
        HPText[0].text = ((int)characterInfo.maxHealth).ToString();
        // 실제 공격력
        DMGText[0].text = ((int)characterInfo.defaultAttackDamage).ToString();
        }
        // 실제 방어력
        DEFText[0].text = ((int)debugCharInfo.actualDEF).ToString(); // 디버깅용 변경 필요

        // 실제 레벨
        curLevelText.text = debugCharInfo.actualLevel.ToString(); // 디버깅용 변경 필요

        nextLevelText.text = (debugCharInfo.actualLevel + 1).ToString(); // 디버깅용 변경 필요

        // 실제 exp
        amountOfEXPText.text = debugCharInfo.actualcurEXP.ToString() + "/" + 
            debugCharInfo.actualmaxEXP.ToString(); //디버깅용 변경 필요

        // 아이템의 개수
        amountOfItemSText.text = "0 /" + item_Amount[(int)Item.EItemRank.S].ToString();
        amountOfItemAText.text = "0 /" + item_Amount[(int)Item.EItemRank.A].ToString();

        EXPGageIMG.fillAmount = debugCharInfo.actualcurEXP / debugCharInfo.actualmaxEXP;
    }

    private void PrintExpectValues() // 새로운 예상 수치 출력시 호출
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

    public void PrintBlankTextToExpectedText() // 빈 텍스트 출력 메소드
    {
        //*************************
        // 예상 수치 빈칸으로 초기화
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

    #region 아이템 계산 메소드
    private void InitExpectValues() // 예상 변수들 초기화 메소드
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
    private void AssignItemAmountFromInventory() // 실제 아이템 개수 캐싱 메소드
    {
        for (int i = 0; i < item_Amount.Length; i++)
        {
            item_Amount[i] = InventoryManager.instance.GetAmountOfItemByTypeAndRank(Item.EItemType.EXP_CHARACTER, (Item.EItemRank)i);
        }
    }

    Tween EXPUpTween; // 첫 트위닝 캐싱
    Tween EXPUpTween2; // 두 번째 트위닝 캐싱
    private void CalculateExpectedLevelUp(Item item) // 아이템을 메개변수로 받고 예상치를 보여주는 메소드
    {
        if (expectedCurLevel >= InventoryManager.instance.debugCharInfo[(int)meCharacter].actualMaxLevel)
        {
            return;
        }

        if (clickedItem_Amount[(int)item.rank] >= item_Amount[(int)item.rank]) // 클릭한 아이템이 보유한 것 보다 많다면 return
        {
            clickedItem_Amount[(int)item.rank] = item_Amount[(int)item.rank];
            return;
        }

        itemCancleBTN[(int)item.rank].SetActive(true); // 아이템 취소 버튼

        expectedCurEXP += item.stat; // 스텟 가중치 exp에 더하기
        clickedItem_Amount[(int)item.rank] += 1; // 클릭한 아이템 1개 늘리기

        int tweeningCounter = 0;

        EXPUpTween?.Kill();
        EXPUpTween2?.Kill();

        while (expectedCurEXP >= expectedMaxEXP) // 스탯이 상한선 밑으로 갈 때 까지
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
                Debug.Log("상한선 이상의 접근");
                EXPUpTween = EXPGageIMG.DOFillAmount(1, 0.2f).OnStepComplete(() => EXPGageIMG.fillAmount = 0f).SetLoops(tweeningCounter).OnComplete(() => EXPGageIMG.fillAmount = 0f); // 트위닝 시작
                PrintExpectValues();
                amountOfEXPText.text = $"0 / {(int)expectedMaxEXP}";
                return;
            }
        }


        if (tweeningCounter > 0)
        {
            EXPUpTween = EXPGageIMG.DOFillAmount(1, 0.2f).OnStepComplete(() => EXPGageIMG.fillAmount = 0f).SetLoops(tweeningCounter).OnComplete(() =>
            EXPUpTween2 = EXPGageIMG.DOFillAmount(expectedCurEXP / expectedMaxEXP, 0.2f)); // 트위닝 시작
        }

        else
        {
            EXPUpTween2 = EXPGageIMG.DOFillAmount(expectedCurEXP / expectedMaxEXP, 0.2f); // 목표 fill까지 트위닝
        }

        PrintExpectValues();
    }
    private void RemoveExpectedLevelUp(Item item) // 선택한 아이템을 예상 계산에서 제외
    {

        if (clickedItem_Amount[(int)item.rank] <= 0)
        {
            clickedItem_Amount[(int)item.rank] = 0;
            return;
        }

        clickedItem_Amount[(int)item.rank] -= 1;

        if (clickedItem_Amount[(int)item.rank] <= 0)
        {
            itemCancleBTN[(int)item.rank].SetActive(false); // 아이템 취소 버튼
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
            EXPUpTween2 = EXPGageIMG.DOFillAmount(expectedCurEXP / expectedMaxEXP, 0.2f); // 목표 fill까지 트위닝

        PrintExpectValues();
    }

    private void ChangeOriginStats(CharacterInfo characterInfo, InventoryManager.DebugCharInfo debugCharInfo) // 예상 스탯을 실제 스탯에 적용
    {
        if (characterInfo != null)
        {
            characterInfo.maxHealth += expectedHP;
            characterInfo.defaultAttackDamage += expectedDMG;
            characterInfo.exSkillDamage += expectedHP;
        }

        debugCharInfo.actualDEF += expectedDEF; // 디버깅용
        debugCharInfo.actualcurEXP = expectedCurEXP; // 디버깅
        debugCharInfo.actualmaxEXP = expectedMaxEXP; // 디버깅
        debugCharInfo.actualLevel = expectedCurLevel; // 디버깅
            
        Debug.Log($"{meCharacter.ToString()}의 방어력 : {debugCharInfo.actualDEF}");
        Debug.Log($"{meCharacter.ToString()}의 현재 EXP : {debugCharInfo.actualcurEXP}");
        Debug.Log($"{meCharacter.ToString()}의 최대 EXP : {debugCharInfo.actualmaxEXP}");
        Debug.Log($"{meCharacter.ToString()}의 레벨 : {debugCharInfo.actualLevel}");
    }

    private void ChangeItemAmount() // 인벤토리에서 아이템을 빼는 메소드
    {
        InventoryManager.instance.RemoveItemsByTypeAndRank(Item.EItemType.EXP_CHARACTER, Item.EItemRank.S, clickedItem_Amount[(int)Item.EItemRank.S]);
        InventoryManager.instance.RemoveItemsByTypeAndRank(Item.EItemType.EXP_CHARACTER, Item.EItemRank.A, clickedItem_Amount[(int)Item.EItemRank.A]);
        clickedItem_Amount[0] = 0;
        clickedItem_Amount[1] = 0;
        AssignItemAmountFromInventory();
    }

    #endregion

    #region 버튼 입력 메소드

    public void OnClickLevelUpItem(Item item) // 레벨업 아이템 클릭 메소드
    {
        CalculateExpectedLevelUp(item);
    }

    public void OnClickCancleItem(Item item) // 레벨업 아이템 취소 메소드
    {
        RemoveExpectedLevelUp(item);
    }

    public void OnClickLevelUp() // 레벨업 버튼 클릭 메소드
    {
        if (clickedItem_Amount[0] <= 0 &&
            clickedItem_Amount[1] <= 0)
        {
            return;
        }

        ChangeOriginStats(PlayerController.INSTANCE.controllableModels[(int)meCharacter].characterInfo, InventoryManager.instance.debugCharInfo[(int)meCharacter]); // 원래 수치에 반영
            ChangeItemAmount(); // 아이템 인벤토리에서 제거
        PrintActualStats(PlayerController.INSTANCE.controllableModels[(int)meCharacter].characterInfo, InventoryManager.instance.debugCharInfo[(int)meCharacter]); // 실제 수치 출력
            InitExpectValues(); // 예상 가중치 초기화
            PrintBlankTextToExpectedText(); // 예상치 출력 백지화
            charStatUI.PrintCharStat(); // 스탯 UI도 갱신

        Debug.Log($"{meCharacter.ToString()}의 방어력 : {InventoryManager.instance.debugCharInfo[(int)meCharacter].actualDEF}");
        Debug.Log($"{meCharacter.ToString()}의 현재 EXP : {InventoryManager.instance.debugCharInfo[(int)meCharacter].actualcurEXP}");
        Debug.Log($"{meCharacter.ToString()}의 최대 EXP : {InventoryManager.instance.debugCharInfo[(int)meCharacter].actualmaxEXP}");
        Debug.Log($"{meCharacter.ToString()}의 레벨 : {InventoryManager.instance.debugCharInfo[(int)meCharacter].actualLevel}");

        if (InventoryManager.instance.debugCharInfo[(int)meCharacter].actualLevel.Equals(InventoryManager.instance.debugCharInfo[(int)meCharacter].actualMaxLevel))
        {
            Debug.Log("왜 안 들어가지지?");
            movePanel.GoToEndPos();
            charStatUI.TurnOffSideUIBG();

            InventoryManager.instance.debugCharInfo[(int)meCharacter].actualcurEXP = 0; // 한계선을 넘었다면 exp는 0으로 만듭니다.
        }

    }
    #endregion
}
