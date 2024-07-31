using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class Item : ScriptableObject
{
    

    [SerializeField] private string Name; // 아이템 이름
    public string name_ {  get { return Name; } }

    [SerializeField] private int Level; // 아이템 레벨
    public int level { get { return Level; } set { Level = value; } }

    [SerializeField] private int maxLevel; // 레벨 제한
    public int MaxLevel { get { return maxLevel; } set { maxLevel = value; } }

    [SerializeField] private float Stat; // 가중치
    public float stat { get { return Stat; } set { Stat = value; } }

    [SerializeField] private int BuyPrice; // 구매 가격
    public int buyPrice { get { return BuyPrice; }}

    [SerializeField] private int SellPrice; // 판매 가격
    public int sellPrice { get { return SellPrice; } set { SellPrice = value; } } 

    [SerializeField] private EItemType ItemType;
    public EItemType itemType { get { return ItemType; } } 

    [SerializeField] private EItemRank Rank; // 아이템 등급 A S
    public EItemRank rank { get { return Rank; } }

    [SerializeField]private Sprite ItemIcon; // 아이템 아이콘
    public Sprite itemIcon { get { return ItemIcon; } }

    [SerializeField] private Sprite ItemRankIcon; // 아이템 랭크 아이콘
    public Sprite itemRankIcon { get { return ItemRankIcon; } }

    public enum EItemRank
    {
        S = 0,
        A = 1
    };

    public enum EItemType
    {
        DAMAGE = 0,
        HEALTH = 1,
        EXP_CHARACTER = 2,
        EXP_WEAPON = 3,
        LIMITBREAK_CHARACTER = 4,
        LIMITBREAK_WEAPON = 5
    };
}
