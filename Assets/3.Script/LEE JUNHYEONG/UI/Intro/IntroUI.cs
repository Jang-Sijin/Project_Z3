using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroUI : MonoBehaviour
{
    [SerializeField]private Intro_TVContorl TV;


    private void Start()
    {
        TV = FindObjectOfType<Intro_TVContorl>();
    }

    public void OnTVClick()
    {
        TV.nextVideo(TV._player);
    }

    public void OnClickGameStart() // debug
    {
        Debug.Log("���� �� ��ȯ �߰� �ʿ�");
    }
}
