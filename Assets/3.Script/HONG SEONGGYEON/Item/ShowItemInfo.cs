using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShowItemInfo: MonoBehaviour
{
    //public List<FieldItem> items; // �÷��̾��� ������ ���
    public Text itemUIText; // UI�� ǥ�õ� �ؽ�Ʈ
    public Image itemUIImage; // UI�� ǥ�õ� �̹���

    void Start()
    {
      //  items = new List<FieldItem>();
    }

    public void AddItem(FieldItem item)
    {
    //   items.Add(item);
        Debug.Log("Item added: " + item.itemName);
    }

    public void UpdateUI(FieldItem item)
    {
 //       if (itemUIText != null)
 //       {
 //           itemUIText.text = item.itemName+ "ȹ��";
 //       }

        if (itemUIImage != null)
        {
            itemUIImage.sprite = item.icon;
            itemUIImage.enabled = true; // �̹����� Ȱ��ȭ
        }
    }
}
