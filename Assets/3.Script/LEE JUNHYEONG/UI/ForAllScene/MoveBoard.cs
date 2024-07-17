using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UI;

/*
 * UI�� �ۿ������� ������ ȿ���� �� ������Ʈ�� ���̸� �ߵ��� �� �ֽ��ϴ�.
 */

public class MoveBoard : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 MovePos; // ���ϴ� ���� ��������.
    [SerializeField] private Ease ease; // ease ���� �ۼ�
    private Vector2 StartPos; // ������ġ�� �����մϴ�.
    private float duration = 1f; // �̵��ð�

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        StartPos = rectTransform.anchoredPosition;
    }

    [ContextMenu("Open Menu For debug")]
    public void OpenMenu()
    {
        rectTransform.DOAnchorPos(MovePos, duration).SetEase(ease); // SetEase �޼ҵ带 �̿��Ͽ� ���� ������ �ٲ� �� �־��!
    }

    [ContextMenu("Close Menu For debug")]
    public void CloseMenu()
    {
        rectTransform.DOAnchorPos(StartPos, duration).SetEase(ease);
    }
}
