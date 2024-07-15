using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class NewBehaviourScript : MonoBehaviour
{

    public void Resolution_Dropdown(int index)
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
                Debug.Log("�ػ󵵸� 1920x1080(��üȭ��)���� �ٲߴϴ�.");
                break;

            case 1:
                Screen.SetResolution(1280, 720, true);
                Debug.Log("�ػ󵵸� 1280x720(��üȭ��)���� �ٲߴϴ�.");
                break;

            case 2:
                Screen.SetResolution(1920, 1080, false);
                Debug.Log("�ػ󵵸� 1920x1080(â���)���� �ٲߴϴ�.");
                break;

            case 3:
                Screen.SetResolution(1280, 720, false);
                Debug.Log("�ػ󵵸� 1280x720(â���)���� �ٲߴϴ�.");
                break;
        }
    }
}
