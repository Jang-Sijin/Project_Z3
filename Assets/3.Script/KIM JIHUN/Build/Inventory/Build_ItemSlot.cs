using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Build_ItemSlot
{
    [Header("Debug Item Info")]
    [SerializeField] private Build_Item _itemData;
    [SerializeField] private int _amount;
    [SerializeField] private int _level;
    [SerializeField] private int _levelRank;

    public Build_Item ItemData => _itemData;
    public int Amount => _amount;
    public int Level => _level;
    public int LevelRank => _levelRank;

    public Build_ItemSlot(Build_Item itemData, int amount, int level, int levelRank)
    {
        this._itemData = itemData;
        this._amount = amount;
        this._level = level;
        this._levelRank = levelRank;
    }
    public void AddAmount(int amountToAdd)
    {
        _amount += amountToAdd;
    }
    public void RemoveAmount(int amountToRemove)
    {
        _amount -= amountToRemove;
    }

    public void IncreaseRank()
    {
        _levelRank += 1;
    }
}

