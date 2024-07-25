using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CustomButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Button button;
    private bool isPointerDown = false;

    void Start()
    {
        button = GetComponent<Button>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isPointerDown = true;
        button.Select(); // ��ư�� ���� ���·� ����
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPointerDown)
        {
            // ��ư Ŭ�� ó��
            Debug.Log("Button Clicked");
        }
        isPointerDown = false;
        button.OnDeselect(null); // ��ư ���� ����
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPointerDown)
        {
            isPointerDown = false;
            button.OnDeselect(null); // ��ư ���� ����
        }
    }
}