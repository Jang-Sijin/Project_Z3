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


    // ��ũ�� ���� ȿ��
    #region ScrollViewEFF
    private ScrollRect scrollRect; // ��ũ�Ѻ� ������Ʈ�� ���ϴ� Ȯ���� ���ؼ�
    [SerializeField]private GameObject downPointer; // ��ũ�Ѻ��� ȭ��ǥ

    private void OnScrollValueChanged(Vector2 scrollPosition) // ���ϴ� ������ ȭ��ǥ�� ���۴ϴ�.
    {
        if (scrollRect.verticalNormalizedPosition <= 0) // �ϴ� ����
        {
            downPointer.SetActive(false); // ��Ȱ
        }

        else // �ϴ��� �ƴ� ��
        {
            downPointer.SetActive(true); // Ȱ��ȭ
        }
    }
    #endregion

}
