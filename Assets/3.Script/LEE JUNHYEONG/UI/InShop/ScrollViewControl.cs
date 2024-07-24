using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScrollViewControl : MonoBehaviour
{
    private ScrollRect scrollRect;
    [SerializeField]private GameObject downPointer;

    private void Start()
    {
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
    }

    private void OnScrollValueChanged(Vector2 scrollPosition)
    {
        if (scrollRect.verticalNormalizedPosition <= 0)
        {
            downPointer.SetActive(false);
        }

        else
        {
            downPointer.SetActive(true);
        }
    }
}
