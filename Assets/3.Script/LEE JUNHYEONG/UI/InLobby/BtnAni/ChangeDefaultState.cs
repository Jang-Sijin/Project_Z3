using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDefaultState : MonoBehaviour
{
    private Image BtnIMG;
    private RectTransform rectTransform;

    [SerializeField]private Sprite originSprite; // »çÁø Ä³½Ì
    [SerializeField]private Vector3 originScale; // ½ºÄÉÀÏ Ä³½Ì

    private void Awake()
    {
        BtnIMG = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();

        originSprite = BtnIMG.sprite;
        originScale = rectTransform.localScale;
    }

    private void OnDisable()
    {
        BtnIMG.sprite = originSprite;
        rectTransform.localScale = originScale;
    }
}
