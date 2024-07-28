using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using DG.Tweening.Core.Easing;

public class MovePanel : MonoBehaviour
{
    [Header("��ǥ ������")]
    [SerializeField]
    private Vector2 targetPos;

    [Header("���� ������")]
    [SerializeField]
    private Vector2 originPos;

    [Header("���� ������")]
    [SerializeField]
    private Vector2 endPos;

    [Header("Ʈ���� �Ⱓ")]
    [SerializeField]
    private float duration;

    [Header("������ �ð�")]
    [SerializeField] private float targetDelay;
    [SerializeField] private float originDelay;
    [SerializeField] private float endDelay;

    [SerializeField] private Ease ease;

    RectTransform rectTransform;

    Tween tween;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void GoToTargetPos()
    {
        if (tween != null)
        {
            tween.Kill();
        }

        tween = rectTransform.DOAnchorPos(targetPos, duration).SetEase(ease).SetDelay(targetDelay);
    }

    public void GoToOriginPos()
    {
        if (tween != null)
        {
            tween.Kill();
        }
    
        tween = rectTransform.DOAnchorPos(originPos, duration).SetEase(ease).SetDelay(originDelay);
    }

    public void GoToEndPos()
    {
        if (tween != null)
        {
            tween.Kill();
        }

        tween = rectTransform.DOAnchorPos(endPos, duration).SetEase(ease).SetDelay(endDelay);
    }

    private void OnDisable()
    {
        if (tween != null)
        {
            tween.Kill();
        }
        
        rectTransform.anchoredPosition = originPos;
    }
}
