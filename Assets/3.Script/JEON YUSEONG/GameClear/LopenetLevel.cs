//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class LopenetLevel : MonoBehaviour
//{
//    public int currentExperience = 0; // 현재 경험치
//    public int currentLevel = 1; // 현재 레벨
//    public int maxExperience; // 다음 레벨업에 필요한 최대 경험치

//    public Slider experienceBar; // 경험치 바 UI
//    public Text experienceText; // 경험치 텍스트 UI
//    public Text levelText; // 레벨 텍스트 UI
//    public Text gainedExperienceText; // 획득한 경험치 텍스트 UI

//    void Start()
//    {
//        maxExperience = currentLevel * 1000; // 초기 최대 경험치 설정
//        UpdateExperienceUI(); // UI 업데이트
//    }

//    // 경험치를 획득하는 메서드
//    public void GainExperience(int amount)
//    {
//        currentExperience += amount; // 경험치 추가

//        // 현재 경험치가 최대 경험치를 넘을 때 레벨업
//        while (currentExperience >= maxExperience)
//        {
//            LevelUp(); // 레벨업 처리
//        }

//        UpdateExperienceUI(); // UI 업데이트
//    }

//    // 레벨업을 처리하는 메서드
//    void LevelUp()
//    {
//        currentExperience -= maxExperience; // 초과된 경험치를 다음 레벨로 이월
//        currentLevel++; // 레벨 증가
//        maxExperience = currentLevel * 1000; // 새로운 최대 경험치 설정
//    }

//    // UI를 업데이트하는 메서드
//    void UpdateExperienceUI()
//    {
//        experienceBar.maxValue = maxExperience; // 경험치 바의 최대값 설정
//        experienceBar.value = currentExperience; // 경험치 바의 현재값 설정

//        // 텍스트 UI 업데이트
//        experienceText.text = currentExperience + " / " + maxExperience;
//        levelText.text = " " + currentLevel;
//        gainedExperienceText.text = "+ " + (currentExperience - (currentLevel - 1) * 1000) + " Exp";
//    }
//}
///*
// public class GameManager : MonoBehaviour
//{
//    public PlayerExperience playerExperience;

//    void GameClear()
//    {
//        // 게임 클리어 시 경험치 1000 추가
//        playerExperience.GainExperience(1000);
//    }
//}
//추가해야함
// */
