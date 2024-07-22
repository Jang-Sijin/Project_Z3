using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class MainCityUI : MonoBehaviour
{
    [SerializeField] private Animator FbtnAni;
    [SerializeField] private Animator Mouse1Ani;
    [SerializeField] private Animator MenuAni;

    private void Update()
    {
        btnAni(KeyCode.Mouse1);
        btnAni(KeyCode.F);
        btnAni(KeyCode.Escape);
    }
    public void btnAni(KeyCode key) //다음 메소드를 불러 버튼 애니메이션을 작동하면 됩니다.
    {
        switch (key)
        {
            case (KeyCode.F):
                btnAni(FbtnAni, key);
                break;

            case (KeyCode.Mouse1):
                MousebtnAct();
                break;

            case (KeyCode.Escape):
                btnAni(MenuAni, key);
                break;

            default:
                Debug.Log("없는 키 입니다.");
                break;
        }
    }

    private void btnAni(Animator ani, KeyCode key)
    {
        if(Input.GetKeyDown(key))
            ani.SetTrigger("Press");
            
        else if(Input.GetKeyUp(key))
            ani.SetTrigger("Default");
    }

    private void MousebtnAct()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Mouse1Ani.SetTrigger("Press");
        }

        if (Input.GetMouseButtonUp(1))
        {
            Mouse1Ani.SetTrigger("Default");
        }
    }
}
