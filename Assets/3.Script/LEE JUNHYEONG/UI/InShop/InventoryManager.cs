using NUnit.Framework.Interfaces;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance = null;

    [SerializeField]private Item[] equipments; //����Դϴ�.
    [SerializeField]private List<Item> inventory; // �κ��丮�Դϴ�.
    private int wallet = 300000; // �����Դϴ�.  �����
    public Action moneyAction;

    public class DebugCharInfo // �÷��̾ �߰��� �ʿ��� �ɷ�ġ
    {
        public float actualmaxEXP; // ������ : �÷��̾� �ִ� ����ġ
        public float actualcurEXP; // ������ : �÷��̾� ���� ����ġ
        public float actualDEF; // ������ : �÷��̾� ����
        public int actualLevel;// ������ : ���� ����
        public int actualMaxLevel;// ������ : ���� ����
    };

    public DebugCharInfo[] debugCharInfo; // ������
    
    public Item[] Equipments { get{  return equipments;  }}
    public List<Item> Inventory{get{  return  inventory; }}
    public int Wallet{ get{ return wallet; }}


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            equipments = new Item[(int)ECharacter.Longinus + 1];

            debugCharInfo = new DebugCharInfo[(int)ECharacter.Longinus + 1]; // ������
            //List<Item> list = new List<Item>();

            for (int i = 0; i < debugCharInfo.Length; i++)
            {
                debugCharInfo[i] = new DebugCharInfo();
                debugCharInfo[i].actualcurEXP = 0;
                debugCharInfo[i].actualmaxEXP = 600;
                debugCharInfo[i].actualDEF = 12f;
                debugCharInfo[i].actualLevel = 18 + i;
                debugCharInfo[i].actualMaxLevel = 20;
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void WearEquipment(Item item, ECharacter eCharacter) // ��� ���� �޼ҵ�
    {
        equipments[(int)eCharacter] = item;
    }

    public void CancleEquipment(ECharacter eCharacter) // ��� ���� �޼ҵ�
    {
        equipments[(int)eCharacter] = null;
    }

    public void AddItem(Item item) // �κ��丮�� ��� �߰�
    {
        inventory.Add(item);
    }

    public void RemoveItem(Item item) // �κ��丮�� ������ ����
    {
        inventory.Remove(item);
    }

    public void AddMoneyToWallet(int money) // ���� ������ �ִ� �޼ҵ�
    {
        wallet += money;
        moneyAction?.Invoke();
    }

    public void RemoveMoneyFromWallet(int cost) // ��� ���� �޼ҵ�
    {
        wallet -= cost;
        moneyAction?.Invoke();
    }

    public int GetAmountOfItemByItem(Item Item) // ���� �κ��� �ִ� Ư�� ������ ���� ��ȯ
    {
        int amount = 0;

        foreach (Item item in inventory)
        {
            if (item.Equals(Item))
            {
                amount += 1;
            }
        }

        return amount;

    }

    public void RemoveItemsByAmount(Item item, int amount) // �ش� �������� ���� ��ŭ ����.
    {
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            if (amount <= 0)
                return;

            if (inventory[i].Equals(item))
            {
                inventory.RemoveAt(i);
                amount -= 1;
            }
        }
    }

    public int GetAmountOfItemByTypeAndRank(Item.EItemType type, Item.EItemRank rank)// ���� �κ��� �ִ� Ư�� Ÿ�԰� ��ũ�� ������ ���� ��ȯ
    {
        int amount = 0;

        foreach (Item item in inventory)
        {
            if (item.itemType.Equals(type) && item.rank.Equals(rank))
            {
                amount += 1;
            }
        }

        return amount;
    }

    public void RemoveItemsByTypeAndRank(Item.EItemType type, Item.EItemRank rank, int amount) //Ư�� Ÿ�԰� ��ũ�� ������ ������ŭ ����
    {
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            if (amount <= 0)
                return;

            if (inventory[i].itemType.Equals(type) && inventory[i].rank.Equals(rank))
            {
                inventory.RemoveAt(i);
                amount -= 1;
            }
        }
    }
}
