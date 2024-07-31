//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;

//public class LopenetLevel : MonoBehaviour
//{
//    public int currentExperience = 0; // ���� ����ġ
//    public int currentLevel = 1; // ���� ����
//    public int maxExperience; // ���� �������� �ʿ��� �ִ� ����ġ

//    public Slider experienceBar; // ����ġ �� UI
//    public Text experienceText; // ����ġ �ؽ�Ʈ UI
//    public Text levelText; // ���� �ؽ�Ʈ UI
//    public Text gainedExperienceText; // ȹ���� ����ġ �ؽ�Ʈ UI

//    void Start()
//    {
//        maxExperience = currentLevel * 1000; // �ʱ� �ִ� ����ġ ����
//        UpdateExperienceUI(); // UI ������Ʈ
//    }

//    // ����ġ�� ȹ���ϴ� �޼���
//    public void GainExperience(int amount)
//    {
//        currentExperience += amount; // ����ġ �߰�

//        // ���� ����ġ�� �ִ� ����ġ�� ���� �� ������
//        while (currentExperience >= maxExperience)
//        {
//            LevelUp(); // ������ ó��
//        }

//        UpdateExperienceUI(); // UI ������Ʈ
//    }

//    // �������� ó���ϴ� �޼���
//    void LevelUp()
//    {
//        currentExperience -= maxExperience; // �ʰ��� ����ġ�� ���� ������ �̿�
//        currentLevel++; // ���� ����
//        maxExperience = currentLevel * 1000; // ���ο� �ִ� ����ġ ����
//    }

//    // UI�� ������Ʈ�ϴ� �޼���
//    void UpdateExperienceUI()
//    {
//        experienceBar.maxValue = maxExperience; // ����ġ ���� �ִ밪 ����
//        experienceBar.value = currentExperience; // ����ġ ���� ���簪 ����

//        // �ؽ�Ʈ UI ������Ʈ
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
//        // ���� Ŭ���� �� ����ġ 1000 �߰�
//        playerExperience.GainExperience(1000);
//    }
//}
//�߰��ؾ���
// */
