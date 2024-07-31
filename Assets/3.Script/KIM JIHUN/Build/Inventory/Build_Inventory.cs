using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;

public class Build_Inventory : MonoBehaviour
{
    private List<Build_ItemSlot> _inventory;
    public List<Build_ItemSlot> Inventory => _inventory;

    public Build_Inventory()
    {
        _inventory = new List<Build_ItemSlot>();
    }

    public bool AddToInventory(Build_Item itemToAdd, int amountToAdd, int level = 1, int levelRank = 1)
    {

        /*
        같은 아이템이 있는지 확인
            있다면 추가했을때 아이템 갯수가 최대 갯수를 넘어가는지 판단
                최대 갯수를 안넘는다면 추가 갯수만큼 추가
                최대 갯수를 넘는다면 현재 갯수 + 추가 갯수 - 최대 갯수만큼 새로운 슬롯 생성
        없다면 인벤토리에 공간이 있는지 확인
            없다면 추가 실패
            공간이 있다면 새로운 슬롯 생성
        */
        int index = FindSameItem(itemToAdd);
        if (index != -1)
        {
            if (amountToAdd + _inventory[index].Amount <= itemToAdd.maxAmount)
            {
                _inventory[index].AddAmount(amountToAdd);
                return true;
            }
            else
            {
                int remainAmount = _inventory[index].Amount + amountToAdd - itemToAdd.maxAmount;
                _inventory[index].AddAmount(itemToAdd.maxAmount - _inventory[index].Amount);
                _inventory.Add(new Build_ItemSlot(itemToAdd, remainAmount, level, levelRank));
                return true;
            }
        }
        else
        {
            if (_inventory.Count <= 999)
            {
                _inventory.Add(new Build_ItemSlot(itemToAdd, amountToAdd, level, levelRank));
                return true;
            }
            else return false;
        }
    }

    public bool RemoveFromInventory(Build_Item itemToRemove, int amountToRemove)
    {
        /*
        같은 아이템이 있는지 확인
            amountToRemove보다 많은지 확인
                적다면 false
            amountToRemove 삭제
                amount가 0이하라면 슬롯 삭제
        없다면
            false
        */

        int index = FindSameItem(itemToRemove);
        if (index != -1)
        {
            if (_inventory[index].Amount < amountToRemove)
                return false;
            else
            {
                _inventory[index].RemoveAmount(amountToRemove);
                if (_inventory[index].Amount <= 0)
                    _inventory.RemoveAt(index);
                return true;
            }
        }
        else
            return false;
    }

    public int FindSameItem(Build_Item itemToFind)
    {
        for (int i = 0; i < _inventory.Count; i++)
        {
            var slot = _inventory[i];
            if (slot.ItemData.itemName == itemToFind.itemName && slot.Amount < slot.ItemData.maxAmount)
            {
                return i;
            }
        }
        return -1;
    }
}
