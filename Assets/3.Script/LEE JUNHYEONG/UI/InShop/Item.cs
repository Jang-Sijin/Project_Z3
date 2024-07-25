using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemData", menuName = "Scriptable Object/ItemData")]
public class Item : ScriptableObject
{
    

    [SerializeField] private string Name;
    public string name_ {  get { return Name; } }

    [SerializeField] private float Stat;
    public float stat { get { return Stat; } }

    [SerializeField] private int BuyPrice;
    public int buyPrice { get { return BuyPrice; } }

    [SerializeField] private int SellPrice;
    public int sellPrice { get { return SellPrice; } } 

    [SerializeField] private ItemType _itemType;
    public ItemType itemType { get { return _itemType; } }

    [SerializeField] private ItemRank rank;
    public ItemRank Rank { get { return rank; } }

    [SerializeField]private Sprite ItemIcon;
    public Sprite itemIcon { get { return ItemIcon; } }

    public enum ItemRank
    {
        S = 0,
        A = 1,
        B = 2
    };

    public enum ItemType
    {
        DAMAGE = 0,
        HEALTH = 1
    };
}
