using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ButtonSelectedEff : MonoBehaviour
{
    /*
     * 메뉴 버튼중 한 번 클릭하면 계속해서 재생할 애니를 위한 스크립트입니다.
     * 버튼에 붙이며 버튼 하위에 있는 이미지의 애니를 조작합니다.
     * 또한 띄우고 싶은 메뉴
     * 지우고 싶은 메뉴를 인스펙터창에서 넣어주면 됩니다.
     */
    private TextMeshProUGUI buttontext;
    private Animator buttonAni;

    [SerializeField] private GameObject wantShowMenu;
    
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
        wantShowMenu.SetActive(true);
    }

    public void turnOff()
    {
        buttonAni.SetTrigger("Normal");
        buttontext.color = Color.white;
        wantShowMenu.SetActive(false);
    }
}
