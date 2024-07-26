using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class ShowItemInfo: MonoBehaviour
{
    //public List<FieldItem> items; // 플레이어의 아이템 목록
    public Text itemUIText; // UI에 표시될 텍스트
    public Image itemUIImage; // UI에 표시될 이미지

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
 //           itemUIText.text = item.itemName+ "획득";
 //       }

        if (itemUIImage != null)
        {
            itemUIImage.sprite = item.icon;
            itemUIImage.enabled = true; // 이미지를 활성화
        }
    }
}
