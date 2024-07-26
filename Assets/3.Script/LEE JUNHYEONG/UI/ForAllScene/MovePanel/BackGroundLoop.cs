using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundLoop : MonoBehaviour
{
    private Vector3 endPos;
    private Vector3 startPos;
    [SerializeField] private float speed = 1f;
    private RectTransform rectTransform;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();

        endPos = new Vector2(-rectTransform.rect.width, 0f);
        startPos = new Vector2(rectTransform.rect.width, 0f);
    }

    private void Update()
    {
        rectTransform.anchoredPosition += Vector2.left * speed;

        if (rectTransform.anchoredPosition.Equals(endPos))
        {
            rectTransform.anchoredPosition = startPos;
        }
    }
}
