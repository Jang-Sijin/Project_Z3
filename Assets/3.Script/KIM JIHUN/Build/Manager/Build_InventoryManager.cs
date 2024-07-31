using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_InventoryManager : SingleMonoBase<Build_InventoryManager>
{
    [Header("Debug")]
    public Build_Item[] debugItem;

    private Build_Inventory weaponInventory;
    private Build_Inventory expInventory;
    private Build_Inventory rankUpInventory;
    private int wallet; //지갑

    public Build_Inventory WeaponInventory => weaponInventory;
    public Build_Inventory ExpInventory => expInventory;
    public Build_Inventory RankUpInventory => rankUpInventory;
    public int Wallet => wallet;


    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(INSTANCE);
    }

    private void Start()
    {
        SetInventory();
        foreach (var item in debugItem)
        {
            Build_ItemSlot tmp = new Build_ItemSlot(item, Random.Range(1, 5), Random.Range(5, 10), 0);
            AddToInventory(tmp);
        }
    }

    private void SetInventory()
    {
        weaponInventory = new Build_Inventory();
        expInventory = new Build_Inventory();
        rankUpInventory = new Build_Inventory();
    }

    public void AddToInventory(Build_ItemSlot itemToAdd)
    {
        bool successToAdd = false;
        switch (itemToAdd.ItemData.itemType)
        {
            case Build_Item.EItemType.EQUIPMENT:
                successToAdd = weaponInventory.AddToInventory(itemToAdd.ItemData, itemToAdd.Amount, itemToAdd.Level);
                break;
            case Build_Item.EItemType.EXP_CHARACTER:
            case Build_Item.EItemType.EXP_WEAPON:
                successToAdd = expInventory.AddToInventory(itemToAdd.ItemData, itemToAdd.Amount, itemToAdd.Level);
                break;
            case Build_Item.EItemType.LIMITBREAK_CHARACTER:
            case Build_Item.EItemType.LIMITBREAK_WEAPON:
                successToAdd = rankUpInventory.AddToInventory(itemToAdd.ItemData, itemToAdd.Amount, itemToAdd.Level);
                break;
        }
        if (successToAdd)
        {
            //추가 성공
        }
        else
        {
            //추가 실패
        }
    }

    public void RemoveFromInventory(Build_ItemSlot itemToRemove)
    {
        bool successToRemove = false;
        switch (itemToRemove.ItemData.itemType)
        {
            case Build_Item.EItemType.EQUIPMENT:
                successToRemove = weaponInventory.RemoveFromInventory(itemToRemove.ItemData, itemToRemove.Amount);
                break;
            case Build_Item.EItemType.EXP_CHARACTER:
            case Build_Item.EItemType.EXP_WEAPON:
                successToRemove = expInventory.RemoveFromInventory(itemToRemove.ItemData, itemToRemove.Amount);
                break;
            case Build_Item.EItemType.LIMITBREAK_CHARACTER:
            case Build_Item.EItemType.LIMITBREAK_WEAPON:
                successToRemove = rankUpInventory.RemoveFromInventory(itemToRemove.ItemData, itemToRemove.Amount);
                break;
        }
        if (successToRemove)
        {
            //Remove 성공
        }
        else
        {
            //Remove 실패
        }
    }

}