using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeDefaultState : MonoBehaviour
{
    private Image BtnIMG;
    private RectTransform rectTransform;

    [SerializeField]private Sprite originSprite; // ���� ĳ��
    [SerializeField]private Vector3 originScale; // ������ ĳ��

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
