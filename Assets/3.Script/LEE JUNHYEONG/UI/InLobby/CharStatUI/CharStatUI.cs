using JSJ;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharStatUI : MonoBehaviour
{
    [SerializeField] private CharselectBtnManager charselectBtnManager;

    [Header("메인 메뉴 UI 메니저")]
    [SerializeField] private MainCityMenuUIManager mainCityMenuUIManager;


    [Header("하위 UI들")]
    [SerializeField] private MovePanel charLevelUpUI; // 캐릭터 레벨업 UI
    [SerializeField] private MovePanel charPromoteUI; // 캐릭터 돌파 UI

    [SerializeField] private GameObject sideUIBG; // 뒷 배경

    [SerializeField] private GameObject[] factions; // 출력할 세력 

    #region 각 텍스트들
    [Header("각 텍스트들")]
    [SerializeField] private TextMeshProUGUI charNameText; // 캐릭터의 이름 출력
    [SerializeField] private TextMeshProUGUI charLevelText; // 캐릭터 레벨 출력
    [SerializeField] private TextMeshProUGUI charMaxLevelText; // 캐릭터 최대 레벨 출력
    [SerializeField] private TextMeshProUGUI charHPText; // 캐릭터 체력 출력
    [SerializeField] private TextMeshProUGUI charDefenceText; // 캐릭터 방어력 출력
    [SerializeField] private TextMeshProUGUI charDMGText; // 캐릭터 데미지 출력
    #endregion
    private readonly string[] charkoreanName = // 캐릭터 한글 이름 출력을 위한 변수
    {
        "코린 위크스",
        "앤비 데마라",
        "11호"
    };

    private void OnEnable()
    {
        PrintCharStat();
    }

    public void PrintCharStat() // 캐릭터의 스탯을 출력합니다.
    {
        for (int i = 0; i < factions.Length; i++)
        {
            factions[i].SetActive(false);
        }

        factions[(int)charselectBtnManager.PrevCharBtn.eCharacter].SetActive(true); // 캐릭터 팩션 출력
        charNameText.text = charkoreanName[(int)charselectBtnManager.PrevCharBtn.eCharacter]; // 캐릭터 이름 출력
        Debug.Log("현재 플레이어 인덱스 : " + (int)charselectBtnManager.PrevCharBtn.eCharacter + " / 캐릭터 배열 길이" + InventoryManager.instance.debugCharInfo.Length);
        charLevelText.text = $"LV.{InventoryManager.instance.debugCharInfo[(int)charselectBtnManager.PrevCharBtn.eCharacter].actualLevel}"; // 캐릭터 레벨 출력 // 디버깅용 수정 필요
        charMaxLevelText.text = InventoryManager.instance.debugCharInfo[(int)charselectBtnManager.PrevCharBtn.eCharacter].actualMaxLevel.ToString(); // 캐릭터 레벨 출력 // 디버깅용 수정 필요

        if (PlayerController.INSTANCE != null)
        {
            charHPText.text = ((int)PlayerController.INSTANCE.
            controllableModels[(int)charselectBtnManager.PrevCharBtn.eCharacter].
            characterInfo.maxHealth).ToString(); // 체력 출력
            charDMGText.text = ((int)PlayerController.INSTANCE.controllableModels
                [(int)charselectBtnManager.PrevCharBtn.eCharacter].characterInfo.
                defaultAttackDamage).ToString(); // 데미지 출력
        }

        charDefenceText.text = InventoryManager.instance.debugCharInfo[(int)charselectBtnManager.PrevCharBtn.eCharacter].actualDEF.ToString();  // 아직 info에 defence 변수 없음 디버깅용
    }

    public void OnClickLevelUpBTN()
    {
        if (InventoryManager.instance.debugCharInfo[(int)charselectBtnManager.PrevCharBtn.eCharacter].actualLevel >= 50)
        {
            return;
        }

        else if ( //현재 레벨이 레벨 상한선과 같다면 캐릭터 진급창을 띄우기
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
