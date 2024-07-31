using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Build_ItemSlotUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _itemIMG;
    private Button button;

    private Build_Item _itemSlot;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowItemInfomation);
    }

    public void RefreshSlot(Build_Item itemData)
    {
        _itemSlot = itemData;
        _itemIMG.sprite = itemData.itemIcon;
    }

    private void ShowItemInfomation()
    {
        UIManager.Instance.InventoryUI.ShowItemInfomation(_itemSlot);
    }
}
