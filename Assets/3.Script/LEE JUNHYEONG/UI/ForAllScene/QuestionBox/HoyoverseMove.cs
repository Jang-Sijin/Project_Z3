using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoyoverseMove : MonoBehaviour
{
    private RectTransform rectTransform;
    private Vector2 direction = Vector2.left;
    private float speed = 10f;

    private Vector3 startPos;
    private Vector3 endPos;


    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startPos = new Vector3(960, 1.6f, 0f);
        endPos = new Vector3(-2880, 1.6f, 0f);
    }
    private void Update()
    {
        MovementBG();

        if (rectTransform.anchoredPosition.Equals(endPos))
        {
            rectTransform.anchoredPosition = startPos;
        }
    }

    private void MovementBG()
    {
        rectTransform.anchoredPosition += direction * speed;
    }
}
