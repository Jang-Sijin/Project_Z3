using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonSelectedEff : MonoBehaviour
{
    /*
     * �޴� ��ư�� �� �� Ŭ���ϸ� ����ؼ� ����� �ִϸ� ���� ��ũ��Ʈ�Դϴ�.
     * ��ư�� ���̸� ��ư ������ �ִ� �̹����� �ִϸ� �����մϴ�.
     * ���� ���� ���� �޴�
     * ����� ���� �޴��� �ν�����â���� �־��ָ� �˴ϴ�.
     */
    private TextMeshProUGUI buttontext;
    private Animator buttonAni;

    [SerializeField] private Animator wantShowMenu;
    
    private void Awake()
    {
        buttontext = GetComponentInChildren<TextMeshProUGUI>();
        buttonAni = GetComponentInChildren<Animator>();
    }

    public void OnClickButton()
    {
            StartAni();
    }

    private void StartAni()
    {
        if (buttonAni.gameObject.activeSelf)
            buttonAni.gameObject.SetActive(true);

        buttonAni.SetTrigger("Selected");
        buttontext.color = Color.black;
        wantShowMenu.gameObject.SetActive(true);

        wantShowMenu.SetTrigger("Open");
    }

    public void turnOff()
    {
        buttonAni.SetTrigger("Normal");
        buttontext.color = Color.white;
        wantShowMenu.gameObject.SetActive(false);

        wantShowMenu.SetTrigger("Close");
    }
}
