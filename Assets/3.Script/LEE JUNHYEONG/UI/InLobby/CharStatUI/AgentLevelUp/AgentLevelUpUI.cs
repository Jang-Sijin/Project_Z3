using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class AgentLevelUpUI : MonoBehaviour
{
    #region 각종텍스트

    //**************************************************************************************************************
    // index -> 0 은 현재 스탯을 index -> 1은 레벨업 후 수치를
    [SerializeField] private TextMeshProUGUI[] healthText; // 체력 텍스트
    [SerializeField] private TextMeshProUGUI[] damageText; // 데미지 텍스트
    [SerializeField] private TextMeshProUGUI[] defenceText; // 방어력 텍스트
    //**************************************************************************************************************


    [SerializeField] private TextMeshProUGUI curLevelText; // 현재레벨 텍스트
    [SerializeField] private TextMeshProUGUI nextLevelText; // 다음레벨 텍스트
    [SerializeField] private TextMeshProUGUI amountOfEXP; // (예상 경험치 / 다음 레벨 경험치) 경험치 텍스트
    [SerializeField] private TextMeshProUGUI amountOfItemA; // A랭크 경험치 아이템 텍스트
    [SerializeField] private TextMeshProUGUI amountOfItemS; // S랭크 경험치 아이템 텍스트

    private float maxEXP = 600f; // 디버깅용 : 플레이어 최대 경험치
    private float curEXP = 0f; // 디버깅용 : 플레이어 현재 경험치
    private float Armor = 10f; // 디버깅용 : 플레이어 방어력
    #endregion

    /*
     * 레벨업 계산 미리 계산하는 메소드
     * 레벨업 미리 계산 시 이미지 fill바꾸는 메소드
     * 빼는 것도 가능해야함
     * 
     * 레벨업 시 변할 텍스트들
     * 체력, 
     */

    //private void Calculate()
}
