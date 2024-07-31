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
    #region 각종 텍스트
    [Header("각종 텍스트")]
    [SerializeField] private TextMeshProUGUI[] HPText; //체력 텍스트 // index = 0 , 현재 능력치 텍스트/ index = 1, 예상 능력치 텍스트
    [SerializeField] private TextMeshProUGUI[] DMGText; //데미지 텍스트
    [SerializeField] private TextMeshProUGUI[] DEFText; //방어력 텍스트
    [SerializeField] private TextMeshProUGUI[] curLevel; // 현재 레벨 텍스트 index = 0은 현재 레벨 1은 레벨 상한선
    [SerializeField] private TextMeshProUGUI[] nextLevel; // 다음 레벨 텍스트
    [SerializeField] private TextMeshProUGUI costText; // 가격표 텍스트

    [Header("돌파 아이템 텍스트 & 이미지")]
    [SerializeField] private Image itemIMG; // 아이템 이미지
    [SerializeField] private TextMeshProUGUI ItemAmountText; // 현재 아이템의 개수 텍스트
    [SerializeField] private Animator itemGageAni;
    #endregion

    #region 각종 가중치 하드코딩
    private readonly float addHP = 100f; // 돌파시 체력 100추가
    private readonly float addDMG = 10f; // 돌파시 데미지 10추가
    private readonly float addDEF = 10f; // 돌파시 방어력 10추가
    private readonly int addMaxLevel = 10; // 돌파시 레벨 제한 10추가
    private readonly int cost = 5000; // 진급 화폐

    private readonly int[] amountOfRequireItem = { 2, 4, 4, 6 }; // 진급시 필요한 아이템 수
    // 20레벨 : A아이템 2개
    // 30레벨 : A아이템 4개
    // 40레벨 : S아이템 4개
    // 50레벨 : S아이템 6개

    private int requireIndex;

    [Header ("돌파 아이템")]
    [SerializeField]private Item itemA;
    [SerializeField]private Item itemS;
    private Item BTNitem;
    #endregion

    #region 각종 캐싱 변수
    private int amountOfItem;
    [SerializeField] private CharselectBtnManager charselectBtnManager;
    private ECharacter mECharacter;
    private MovePanel movePanel;
    private CharStatUI charStatUI;
    #endregion

    #region 출력 메소드

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

    private void PrintStats(CharacterInfo characterInfo, InventoryManager.DebugCharInfo debugCharInfo) // 초기화 혹은 구매 시 모든 텍스트 출력 메소드
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
                Debug.Log("비정상적인 레벨 제한");
                break;
        }
    }
    #endregion

    #region 각종 계산 메소드
    private void AssignAmountOfItem() // 아이템의 총 개수 캐싱
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
                Debug.Log("비정상적인 레벨 제한입니다.");
                break;
        }
    }

    private void AssignValueToCharInfo() // 값들을 실제 캐릭터에 적용
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


    #region 버튼 입력 메소드
    public void OnClickPromoteBTN() // 진급 버튼 메소드
    {
        if (amountOfItem < amountOfRequireItem[requireIndex] || InventoryManager.instance.Wallet < cost)
        {
            return;
        }

        StartCoroutine(OnClickPromote_co());
    }

    private IEnumerator OnClickPromote_co() // 진급 누를 시 실제 캐릭터에 효과 적용
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

    public void OnClickClose() // 창 닫기
    {
        charStatUI.TurnOffSideUIBG();
        movePanel.GoToEndPos();
    }
    #endregion
}
