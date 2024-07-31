using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Build_ShopSlotUI : MonoBehaviour
{
    private Build_Item itemData;
    public Image icon;
    public TextMeshProUGUI itemName;
    public Image rankIcon;

    public void AssignItem(Build_Item item)
    {
        itemData = item;
        icon.sprite = itemData.itemIcon;
        itemName.text = itemData.itemName;
        rankIcon.sprite = UIManager.Instance.ShopUI.shopScript.itemRankIcon[(int)itemData.itemRank];
    }

    public void ClearItem()
    {
        itemData = null;
    }

    public void ShowItemInfo()
    {
        UIManager.Instance.ShopUI.shopScript.PrintItemStat(itemData);
    }
}
