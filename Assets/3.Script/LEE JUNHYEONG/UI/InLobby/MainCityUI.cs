using System.Collections;
using System.Collections.Generic;
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
    public void btnAni(KeyCode key) //���� �޼ҵ带 �ҷ� ��ư �ִϸ��̼��� �۵��ϸ� �˴ϴ�.
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
                Debug.Log("���� Ű �Դϴ�.");
                break;
        }
    }

    private void btnAni(Animator ani, KeyCode key)
    {
        if (Input.GetKeyDown(key))
            ani.SetTrigger("Press");

        else if (Input.GetKeyUp(key))
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
