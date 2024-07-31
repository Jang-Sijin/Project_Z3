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

    private Build_Item _itemSlot;

    private void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(ShowItemPreview);
    }

    public void RefreshSlot(Build_Item itemSlot)
    {
        _itemSlot = itemSlot;
        _itemIMG.sprite = itemSlot.itemIcon;
    }

    private void ShowItemPreview()
    {
        UIManager.Instance.WeaponUI.ShowItemPreview(_itemSlot);
    }
}
