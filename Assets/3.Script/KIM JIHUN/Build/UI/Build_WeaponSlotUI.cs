using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Build_WeaponSlotUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Image _itemIMG;
    [SerializeField] private GameObject _itemLevelFrame;
    [SerializeField] private TextMeshProUGUI _itemLevelTXT;
    private Button button;

    private Build_ItemSlot _itemSlot;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowItemPreview);
    }

    public void RefreshSlot(Build_ItemSlot itemSlot)
    {
        _itemSlot = itemSlot;
        _itemIMG.sprite = itemSlot.ItemData.itemIcon;

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

    private void ShowItemPreview()
    {
        UIManager.Instance.WeaponUI.ShowItemPreview(_itemSlot);
    }
}
