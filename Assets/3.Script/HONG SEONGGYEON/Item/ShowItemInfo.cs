using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShowItemInfo: MonoBehaviour
{
    //public List<FieldItem> items; // �÷��̾��� ������ ���
    public TMP_Text itemUIText; // UI�� ǥ�õ� �ؽ�Ʈ
    public Image itemUIImage; // UI�� ǥ�õ� �̹���

    void Start()
    {
        //  items = new List<FieldItem>();
        itemUIImage = GetComponentInChildren<Image>();
        itemUIText = GetComponentInChildren<TMP_Text>();
    }

    public void AddItem(Item item)
    {
    //   items.Add(item);
        Debug.Log("Item added: " + item.name);
    }

    public void UpdateUI(Item item)
    {
      if (itemUIText != null)
      {
          itemUIText.text = item.name_+ " ȹ��!";
      }

        if (itemUIImage != null)
        {
            itemUIImage.sprite = item.itemIcon;
            itemUIImage.enabled = true; // �̹����� Ȱ��ȭ
        }

        // UI Ȱ��ȭ
        gameObject.SetActive(true);
    }
    public void HideUI()
    {
        // UI ��Ȱ��ȭ
        gameObject.SetActive(false);
    }

}
