using System.Collections;
using System.Collections.Generic;
using Unity.Entities.UniversalDelegates;
using UnityEngine;

[CreateAssetMenu(fileName = "Build_ItemData", menuName = "Build_Item/Build_ItemData")]
public class Build_Item : ScriptableObject
{
    [Header("아이템 정보")]
    public EItemRank itemRank;
    public string itemName;
    public Sprite itemIcon;
    public int maxAmount;
    [TextArea(5, 5)]
    public string itemInfoTXT;


    [Header("아이템 가격")]
    public int buyPrice;
    public bool canSell;
    public int sellPrice;


    [Header("아이템 스탯")]
    public EItemType itemType;
    public float attackStat;
    public float defenceStat;
    public float healthStat;
    public float expStat;
    public enum EItemRank
    {
        S = 0,
        A = 1
    };

    public enum EItemType
    {
        EQUIPMENT,
        EXP_CHARACTER,
        EXP_WEAPON,
        LIMITBREAK_CHARACTER,
        LIMITBREAK_WEAPON
    };
}
