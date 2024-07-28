using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance; // Singleton 인스턴스
    private float startTime; // 시작 시간
    public static float elapsedTime; // 경과 시간
    public Text timeText; // 시간을 표시할 UI 텍스트

    void Awake()
    {
        // Singleton 패턴 구현
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // 시작 시간 기록
        startTime = Time.time;
    }

    void Update()
    {
        // 경과 시간 계산
        elapsedTime = Time.time - startTime;
    }

    public void ChangeScene(string sceneName)
    {
        // 씬 전환 시 경과 시간 저장
        elapsedTime = Time.time - startTime;
        SceneManager.LoadScene(sceneName);
    }

    void OnEnable()
    {
        // 씬 전환 후 시간 표시 (timeText가 있는 씬에서만 동작)
        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
            timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }
}
