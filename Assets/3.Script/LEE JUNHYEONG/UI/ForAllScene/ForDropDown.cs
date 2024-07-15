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
                Debug.Log("해상도를 1920x1080(전체화면)으로 바꿉니다.");
                break;

            case 1:
                Screen.SetResolution(1280, 720, true);
                Debug.Log("해상도를 1280x720(전체화면)으로 바꿉니다.");
                break;

            case 2:
                Screen.SetResolution(1920, 1080, false);
                Debug.Log("해상도를 1920x1080(창모드)으로 바꿉니다.");
                break;

            case 3:
                Screen.SetResolution(1280, 720, false);
                Debug.Log("해상도를 1280x720(창모드)으로 바꿉니다.");
                break;
        }
    }
}
