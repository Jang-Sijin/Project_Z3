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
    [SerializeField] public MovePanel charLevelUpUI;
    [SerializeField] public MovePanel charPromoteUI;

    private int[] charLevels; // 디버깅용 : 캐릭터 레벨
    private int[] charMaxLevels = {20, 20, 20}; // 디버깅용 : 캐릭터 최대 

    [Header("각 텍스트들")]
    [SerializeField] private GameObject[] factions; // 출력할 세력 
    [SerializeField] private TextMeshProUGUI charNameText; // 캐릭터의 이름 출력
    [SerializeField] private TextMeshProUGUI charLevelText; // 캐릭터 레벨 출력
    [SerializeField] private TextMeshProUGUI charMaxLevelText; // 캐릭터 최대 레벨 출력
    [SerializeField] private TextMeshProUGUI charHPText; // 캐릭터 체력 출력
    [SerializeField] private TextMeshProUGUI charDefenceText; // 캐릭터 방어력 출력
    [SerializeField] private TextMeshProUGUI charDMGText; // 캐릭터 데미지 출력

    private string[] charkoreanName = // 캐릭터 한글 이름 출력을 위한 변수
    {
        "코린 위크스",
        "앤비 데마라",
        "11호"
    };

    private void Awake() // 디버깅용 
    {
        charLevels = new int[3];// 디버깅용

        for (int i = 0; i < charLevels.Length; i++)// 디버깅용
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

        factions[(int)charselectBtnManager.PrevCharBtn.eCharacter].SetActive(true); // 캐릭터 팩션 출력
        charNameText.text = charkoreanName[(int)charselectBtnManager.PrevCharBtn.eCharacter]; // 캐릭터 이름 출력
        charLevelText.text = $"LV.{charLevels[(int)charselectBtnManager.PrevCharBtn.eCharacter]}"; // 캐릭터 레벨 출력 // 디버깅용 수정 필요
        charMaxLevelText.text = charMaxLevels[(int)charselectBtnManager.PrevCharBtn.eCharacter].ToString(); // 캐릭터 레벨 출력 // 디버깅용 수정 필요

        charHPText.text = ((int)PlayerController.INSTANCE.
            controllableModels[(int)charselectBtnManager.PrevCharBtn.eCharacter].
            characterInfo.maxHealth).ToString(); // 체력 출력

        charDefenceText.text = "12";  // 아직 info에 defence 변수 없음 디버깅용

        charDMGText.text = ((int)PlayerController.INSTANCE.controllableModels
            [(int)charselectBtnManager.PrevCharBtn.eCharacter].characterInfo.
            defaultAttackDamage).ToString(); // 데미지 출력
    }

    public void OnClickLevelUpBTN()
    {
        charPromoteUI.gameObject.SetActive(true);
        charPromoteUI.GoToTargetPos();
        mainCityMenuUIManager.emenuState = MainCityMenuUIManager.EMenuState.CharPromteMenu;
    }


}
