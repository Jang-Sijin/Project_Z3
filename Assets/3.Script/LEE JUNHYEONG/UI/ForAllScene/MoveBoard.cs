using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

/*
 * UI가 밖에서부터 들어오는 효과를 이 컴포넌트를 붙이면 발동할 수 있습니다.
 */

public class MoveBoard : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 MovePos; // 원하는 곳을 적으세요.
    [SerializeField] private Ease ease; // ease 유형 작성
    private Vector2 StartPos; // 시작위치를 저장합니다.
    private float duration = 1f; // 이동시간

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        StartPos = rectTransform.anchoredPosition;
    }

    [ContextMenu("Open Menu For debug")]
    public void OpenMenu()
    {
        rectTransform.DOAnchorPos(MovePos, duration).SetEase(ease); // SetEase 메소드를 이용하여 진행 유형을 바꿀 수 있어요!
    }

    [ContextMenu("Close Menu For debug")]
    public void CloseMenu()
    {
        rectTransform.DOAnchorPos(StartPos, duration).SetEase(ease);
    }
}
