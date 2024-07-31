using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Item;
using UnityEditor.MemoryProfiler;

public class ScrollViewArrowControl : MonoBehaviour
{

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
    }


    // 스크롤 뷰의 효과
    #region ScrollViewEFF
    private ScrollRect scrollRect; // 스크롤뷰 오브젝트의 최하단 확인을 위해서
    [SerializeField]private GameObject downPointer; // 스크롤뷰의 화살표

    private void OnScrollValueChanged(Vector2 scrollPosition) // 최하단 도착시 화살표를 없앱니다.
    {
        if (scrollRect.verticalNormalizedPosition <= 0) // 하단 도착
        {
            downPointer.SetActive(false); // 비활
        }

        else // 하단이 아닐 시
        {
            downPointer.SetActive(true); // 활성화
        }
    }
    #endregion

}
