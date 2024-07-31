using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Build_ItemSlotUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _itemIMG;
    [SerializeField] private TextMeshProUGUI _itemAmountTXT;
    [SerializeField] private GameObject _itemLevelFrame;
    [SerializeField] private TextMeshProUGUI _itemLevelTXT;
    private Button button;

    private Build_ItemSlot _itemSlot;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowItemInfomation);
    }

    public void RefreshSlot(Build_ItemSlot itemSlot)
    {
        _itemSlot = itemSlot;
        _itemIMG.sprite = itemSlot.ItemData.itemIcon;

        if (itemSlot.Amount == 1)
        {
            _itemAmountTXT.text = "";
        }
        else
        {
            _itemAmountTXT.text = itemSlot.Amount.ToString();
        }

        if (itemSlot.ItemData.itemType == Build_Item.EItemType.EQUIPMENT)
        {
            _itemLevelFrame.SetActive(true);
            _itemLevelTXT.text = "LV." + itemSlot.Level.ToString();
        }
        else
        {
            _itemLevelFrame.SetActive(false);
        }
    }

    private void ShowItemInfomation()
    {
        UIManager.Instance.InventoryUI.ShowItemInfomation(_itemSlot);
    }
}
