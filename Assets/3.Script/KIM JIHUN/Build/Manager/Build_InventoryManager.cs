using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_InventoryManager : SingleMonoBase<Build_InventoryManager>
{
    [Header("Debug")]
    public Build_Item[] debugItem;

    private Build_Inventory weaponInventory;
    //private Build_Inventory expInventory;
    //private Build_Inventory rankUpInventory;
    private int wallet; //지갑

    public Build_Inventory WeaponInventory => weaponInventory;
    //public Build_Inventory ExpInventory => expInventory;
    //public Build_Inventory RankUpInventory => rankUpInventory;
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
            Build_Item tmp = item;
            AddToInventory(tmp);
        }
    }

    private void SetInventory()
    {
        weaponInventory = new Build_Inventory();
    }

    public void AddToInventory(Build_Item itemToAdd)
    {
        bool successToAdd = false;
        successToAdd = weaponInventory.AddToInventory(itemToAdd);
        if (successToAdd)
        {
            //추가 성공
        }
        else
        {
            //추가 실패
        }
    }

    public void RemoveFromInventory(Build_Item itemToRemove)
    {
        bool successToRemove = false;
        successToRemove = weaponInventory.RemoveFromInventory(itemToRemove);
        if (successToRemove)
        {
            //Remove 성공
        }
        else
        {
            //Remove 실패
        }
    }

    public void IncreaseWallet(int money)
    {
        wallet += money;
    }

    public void DecreaseWallet(int money)
    {
        wallet -= money;
    }
}