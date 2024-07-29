using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    private static TimeManager instance; // Singleton �ν��Ͻ�
    private float startTime; // ���� �ð�
    public static float elapsedTime; // ��� �ð�
    public Text timeText; // �ð��� ǥ���� UI �ؽ�Ʈ

    void Awake()
    {
        // Singleton ���� ����
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
        // ���� �ð� ���
        startTime = Time.time;
    }

    void Update()
    {
        // ��� �ð� ���
        elapsedTime = Time.time - startTime;
    }

    public void ChangeScene(string sceneName)
    {
        // �� ��ȯ �� ��� �ð� ����
        elapsedTime = Time.time - startTime;
        SceneManager.LoadScene(sceneName);
    }

    void OnEnable()
    {
        // �� ��ȯ �� �ð� ǥ�� (timeText�� �ִ� �������� ����)
        if (timeText != null)
        {
            int minutes = Mathf.FloorToInt(elapsedTime / 60F);
            int seconds = Mathf.FloorToInt(elapsedTime - minutes * 60);
            int milliseconds = Mathf.FloorToInt((elapsedTime * 1000) % 1000);
            timeText.text = string.Format("{0:00}:{1:00}:{2:000}", minutes, seconds, milliseconds);
        }
    }
}
