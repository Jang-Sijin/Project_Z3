using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Item;
using UnityEditor.MemoryProfiler;

public class ScrollViewControl : MonoBehaviour
{


    private void Start()
    {
        items = new List<Item>();
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
    }

    private List<Item> items; // �������� �������Դϴ�.

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

    // ���� ���� �� ������ info ���
    #region ChangeInfoText
    [SerializeField] private TextMeshProUGUI WeaponNameInfo; // Ŭ���� �������� �̸� info
    [SerializeField] private TextMeshProUGUI itemTypeInfo; // Ŭ���� ������ ���� info 
    [SerializeField] private TextMeshProUGUI DMGInfo; //Ŭ���� �������� ���ݷ� info
    [SerializeField] private TextMeshProUGUI PriceInfo; //���� Info
    private string[] type = { "���ݷ�", "ü��" };


    protected virtual void ChangeWeaponInfo(Item item) // ���� ���� �� info�� ���� �޼ҵ�
    {
        WeaponNameInfo.text = item.name;
        DMGInfo.text = ((int)item.stat).ToString();
        PriceInfo.text = item.sellPrice.ToString();
        itemTypeInfo.text = type[(int)item.itemType];
    }
    #endregion
}
