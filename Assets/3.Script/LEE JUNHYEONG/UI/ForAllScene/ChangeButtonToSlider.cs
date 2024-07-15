using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ChangeButtonToSlider : MonoBehaviour
{
    [SerializeField] private Image Fill;
    [SerializeField] private Image handle;
    private RectTransform handleRect;

    public void TurnOnOrOff()
    {
        if(Fill.fillAmount.Equals(0))
        {
            Fill.DOFillAmount(1f,0.1f).SetEase(Ease.InOutQuad);

            handle.rectTransform.DOAnchorPosX(30f, 0.1f).SetEase(Ease.InOutQuad);
        }

        else
        {
            Fill.DOFillAmount(0f, 0.1f).SetEase(Ease.InOutQuad);

            handle.rectTransform.DOAnchorPosX(0f, 0.1f).SetEase(Ease.InOutQuad);
        }
    }
}
