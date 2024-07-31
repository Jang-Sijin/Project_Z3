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

    [SerializeField]private Item[] equipments; //장비입니다.
    [SerializeField]private List<Item> inventory; // 인벤토리입니다.
    private int wallet = 300000; // 지갑입니다.  디버깅
    public Action moneyAction;

    public class DebugCharInfo // 플레이어에 추가가 필요한 능력치
    {
        public float actualmaxEXP; // 디버깅용 : 플레이어 최대 경험치
        public float actualcurEXP; // 디버깅용 : 플레이어 현재 경험치
        public float actualDEF; // 디버깅용 : 플레이어 방어력
        public int actualLevel;// 디버깅용 : 실제 레벨
        public int actualMaxLevel;// 디버깅용 : 돌파 구간
    };

    public DebugCharInfo[] debugCharInfo; // 디버깅용
    
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

            debugCharInfo = new DebugCharInfo[(int)ECharacter.Longinus + 1]; // 디버깅용
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

    public void WearEquipment(Item item, ECharacter eCharacter) // 장비 착용 메소드
    {
        equipments[(int)eCharacter] = item;
    }

    public void CancleEquipment(ECharacter eCharacter) // 장비 해제 메소드
    {
        equipments[(int)eCharacter] = null;
    }

    public void AddItem(Item item) // 인벤토리에 장비 추가
    {
        inventory.Add(item);
    }

    public void RemoveItem(Item item) // 인벤토리에 아이템 제거
    {
        inventory.Remove(item);
    }

    public void AddMoneyToWallet(int money) // 수익 지갑에 넣는 메소드
    {
        wallet += money;
        moneyAction?.Invoke();
    }

    public void RemoveMoneyFromWallet(int cost) // 비용 차감 메소드
    {
        wallet -= cost;
        moneyAction?.Invoke();
    }

    public int GetAmountOfItemByItem(Item Item) // 현재 인벤에 있는 특정 아이템 개수 반환
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

    public void RemoveItemsByAmount(Item item, int amount) // 해당 아이템을 개수 만큼 제거.
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

    public int GetAmountOfItemByTypeAndRank(Item.EItemType type, Item.EItemRank rank)// 현재 인벤에 있는 특정 타입과 랭크의 아이템 개수 반환
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

    public void RemoveItemsByTypeAndRank(Item.EItemType type, Item.EItemRank rank, int amount) //특정 타입과 랭크의 아이템 개수만큼 제거
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
