using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum EInventoryCategory
{
    All,
    Weapon,
    Exp,
    RankUp
}

public class Build_InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject _inventoryUI;

    [SerializeField] private Build_ItemSlotUI[] itemSlots;
    [SerializeField] private Transform _slotHolder;

    [Header("Wallet")]
    [SerializeField] private TextMeshProUGUI _walletTXT;

    [Header("Show Item Infomation")]
    [SerializeField] private TextMeshProUGUI _itemNameTXT;
    [SerializeField] private Image _itemIconIMG;
    [SerializeField] private TextMeshProUGUI _itemAttackTXT;
    [SerializeField] private TextMeshProUGUI _itemDefenceTXT;
    [SerializeField] private TextMeshProUGUI _itemHealthTXT;

    [Header("무기 종류 설명 창")]
    [SerializeField] private GameObject weaponInfoObject;

    public void OpenInventoryUI()
    {
        _inventoryUI.SetActive(true);
        ChangeCategory(0);
        ShowItemInfomation(null);
    }

    public void RefreshInventory(EInventoryCategory category)
    {
        _walletTXT.text = Build_InventoryManager.INSTANCE.Wallet.ToString();
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].gameObject.SetActive(false);
        }

        int weaponInventoryCount = Build_InventoryManager.INSTANCE.WeaponInventory.Inventory.Count;
        for (int i = 0; i < weaponInventoryCount; i++)
        {
            Debug.Log(i);
            itemSlots[i].gameObject.SetActive(true);
            itemSlots[i].RefreshSlot(Build_InventoryManager.INSTANCE.WeaponInventory.Inventory[i]);
            itemSlots[i].GetComponent<Animator>().SetTrigger("Normal");
        }
        /*
        if (category == EInventoryCategory.All ||
            category == EInventoryCategory.Weapon)
        {
            for (int i = 0; i < weaponInventoryCount; i++)
            {
                Debug.Log(i);
                itemSlots[i].gameObject.SetActive(true);
                itemSlots[i].RefreshSlot(Build_InventoryManager.INSTANCE.WeaponInventory.Inventory[i]);
                itemSlots[i].GetComponent<Animator>().SetTrigger("Normal");
            }
        }
        */
        /*
        if (category == EInventoryCategory.All ||
            category == EInventoryCategory.Exp)
        {
            for (int i = 0; i < expInventoryCount; i++)
            {
                itemSlots[i + weaponInventoryCount].gameObject.SetActive(true);
                itemSlots[i + weaponInventoryCount].RefreshSlot(Build_InventoryManager.INSTANCE.ExpInventory.Inventory[i]);
                itemSlots[i + weaponInventoryCount].GetComponent<Animator>().SetTrigger("Normal");
            }
        }
        if (category == EInventoryCategory.All ||
        category == EInventoryCategory.RankUp)
        {
            for (int i = 0; i < rankUpInventoryCount; i++)
            {
                itemSlots[i + weaponInventoryCount + expInventoryCount].gameObject.SetActive(true);
                itemSlots[i + weaponInventoryCount + expInventoryCount].RefreshSlot(Build_InventoryManager.INSTANCE.RankUpInventory.Inventory[i]);
                itemSlots[i + weaponInventoryCount + expInventoryCount].GetComponent<Animator>().SetTrigger("Normal");
            }
        }
        */
        /*
        switch (category)
        {
            case EInventoryCategory.All:
                for (int i = 0; i < weaponInventoryCount; i++)
                {
                    Debug.Log(i);
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
                return;
            case EInventoryCategory.Weapon:
                for (int i = 0; i < weaponInventoryCount; i++)
                {
                    itemSlots[i].gameObject.SetActive(true);
                    itemSlots[i].RefreshSlot(Build_InventoryManager.INSTANCE.WeaponInventory.Inventory[i]);
                    itemSlots[i].GetComponent<Animator>().SetTrigger("Normal");
                }
                return;
            case EInventoryCategory.Exp:
                for (int i = 0; i < expInventoryCount; i++)
                {
                    itemSlots[i].gameObject.SetActive(true);
                    itemSlots[i].RefreshSlot(Build_InventoryManager.INSTANCE.ExpInventory.Inventory[i]);
                    itemSlots[i].GetComponent<Animator>().SetTrigger("Normal");
                }
                return;
            case EInventoryCategory.RankUp:
                for (int i = 0; i < rankUpInventoryCount; i++)
                {
                    itemSlots[i].gameObject.SetActive(true);
                    itemSlots[i].RefreshSlot(Build_InventoryManager.INSTANCE.RankUpInventory.Inventory[i]);
                    itemSlots[i].GetComponent<Animator>().SetTrigger("Normal");
                }
                return;
        }
        */
    }

    public void ChangeCategory(int index)
    {
        switch (index)
        {
            case 0://All
                RefreshInventory(EInventoryCategory.All);
                break;
            case 1://Weapon
                RefreshInventory(EInventoryCategory.Weapon);
                break;
            case 2://Exp
                RefreshInventory(EInventoryCategory.Exp);
                break;
            case 3://rankup
                RefreshInventory(EInventoryCategory.RankUp);
                break;
        }
    }

    public void ShowItemInfomation(Build_Item itemToShow)
    {

        if (itemToShow == null)
        {
            _itemNameTXT.text = "";
            _itemIconIMG.color = new Color(1, 1, 1, 0);
            weaponInfoObject.SetActive(false);
            return;
        }
        _itemNameTXT.text = itemToShow.itemName;
        _itemIconIMG.color = new Color(1, 1, 1, 1);
        _itemIconIMG.sprite = itemToShow.itemIcon;


        if (itemToShow.itemType == Build_Item.EItemType.EQUIPMENT)
        {
            weaponInfoObject.SetActive(true);
            _itemAttackTXT.text = itemToShow.attackStat.ToString();
            _itemDefenceTXT.text = itemToShow.defenceStat.ToString();
            _itemHealthTXT.text = itemToShow.healthStat.ToString();
        }
    }
}
