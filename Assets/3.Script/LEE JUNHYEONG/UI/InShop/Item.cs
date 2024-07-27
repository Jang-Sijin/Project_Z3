using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class Item : ScriptableObject
{
    

    [SerializeField] private string Name;
    public string name_ {  get { return Name; } }

    [SerializeField] private int Level;
    public int level { get { return Level; } set { Level = value; } } 

    [SerializeField] private float Stat;
    public float stat { get { return Stat; } set { Stat = value; } }

    [SerializeField] private int BuyPrice;
    public int buyPrice { get { return BuyPrice; }}

    [SerializeField] private int SellPrice;
    public int sellPrice { get { return SellPrice; } set { SellPrice = value; } } 

    [SerializeField] private EItemType ItemType;
    public EItemType itemType { get { return ItemType; } }

    [SerializeField] private EItemRank Rank;
    public EItemRank rank { get { return Rank; } }

    [SerializeField]private Sprite ItemIcon;
    public Sprite itemIcon { get { return ItemIcon; } }


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
