using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class FieldItem : ScriptableObject
{
    public string itemName;
    public int stat;
    public Sprite icon;

    public ItemRank itemRank;
    public ItemType itemType;

    public enum ItemRank
    {
        Rank_A,
        Rank_B
    };

    public enum ItemType
    {
        WeaponEXP,
        WeaponLimitBreak,
        AgentEXP,
        AgentLimitBreak
    };
}
