using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Build_InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryUI;

    private Build_ItemSlotUI[] itemSlots;
    [SerializeField] private Transform _slotHolder;

    [Header("Show Item Infomation")]
    [SerializeField] private TextMeshProUGUI _itemNameTXT;
    [SerializeField] private Image _itemIconIMG;
    [SerializeField] private TextMeshProUGUI _itemLevelTXT;
    [SerializeField] private TextMeshProUGUI _itemAttackTXT;
    [SerializeField] private TextMeshProUGUI _itemDefenceTXT;
    [SerializeField] private TextMeshProUGUI _itemHealthTXT;

    private void Start()
    {
        itemSlots = _slotHolder.GetComponentsInChildren<Build_ItemSlotUI>();
    }
    public void OpenInventoryUI()
    {
        _inventoryUI.SetActive(true);
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].gameObject.SetActive(false);
        }

        int weaponInventoryCount = Build_InventoryManager.INSTANCE.WeaponInventory.Inventory.Count;
        int expInventoryCount = Build_InventoryManager.INSTANCE.ExpInventory.Inventory.Count;
        int rankUpInventoryCount = Build_InventoryManager.INSTANCE.RankUpInventory.Inventory.Count;

        for (int i = 0; i < weaponInventoryCount; i++)
        {
            itemSlots[i].gameObject.SetActive(true);
            itemSlots[i].RefreshSlot(Build_InventoryManager.INSTANCE.WeaponInventory.Inventory[i]);
            itemSlots[i].GetComponent<Animator>().SetTrigger("Normal");
        }
        for (int i = 0; i < expInventoryCount; i++)
        {
            itemSlots[i + weaponInventoryCount].gameObject.SetActive(true);
            itemSlots[i + weaponInventoryCount].RefreshSlot(Build_InventoryManager.INSTANCE.ExpInventory.Inventory[i]);
            itemSlots[i + weaponInventoryCount].GetComponent<Animator>().SetTrigger("Normal");
        }
        for (int i = 0; i < rankUpInventoryCount; i++)
        {
            itemSlots[i + weaponInventoryCount + expInventoryCount].gameObject.SetActive(true);
            itemSlots[i + weaponInventoryCount + expInventoryCount].RefreshSlot(Build_InventoryManager.INSTANCE.RankUpInventory.Inventory[i]);
            itemSlots[i + weaponInventoryCount + expInventoryCount].GetComponent<Animator>().SetTrigger("Normal");
        }
    }

    public void ShowItemInfomation(Build_ItemSlot itemToShow)
    {
        _itemNameTXT.text = itemToShow.ItemData.itemName;
        _itemIconIMG.sprite = itemToShow.ItemData.itemIcon;
        _itemLevelTXT.text = $"LV.{itemToShow.Level}/{10 * itemToShow.LevelRank}";
        _itemAttackTXT.text = (itemToShow.ItemData.attackStat * itemToShow.Level).ToString();
        _itemDefenceTXT.text = (itemToShow.ItemData.defenceStat * itemToShow.Level).ToString();
        _itemHealthTXT.text = (itemToShow.ItemData.healthStat * itemToShow.Level).ToString();
    }
}
