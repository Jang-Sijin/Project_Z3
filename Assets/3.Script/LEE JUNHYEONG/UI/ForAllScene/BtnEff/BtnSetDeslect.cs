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
        button.Select(); // 버튼을 선택 상태로 변경
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (isPointerDown)
        {
            // 버튼 클릭 처리
            Debug.Log("Button Clicked");
        }
        isPointerDown = false;
        button.OnDeselect(null); // 버튼 선택 해제
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (isPointerDown)
        {
            isPointerDown = false;
            button.OnDeselect(null); // 버튼 선택 해제
        }
    }
}