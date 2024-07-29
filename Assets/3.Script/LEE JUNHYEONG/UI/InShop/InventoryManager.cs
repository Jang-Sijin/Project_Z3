using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance = null;

    private Item[] equipment; //장비입니다.
    [SerializeField]private List<Item> inventory; // 인벤토리입니다.
    private int wallet; // 지갑입니다.

    public struct DebugCharInfo // 플레이어에 추가가 필요한 능력치
    {
        public float actualmaxEXP; // 디버깅용 : 플레이어 최대 경험치
        public float actualcurEXP; // 디버깅용 : 플레이어 현재 경험치
        public float actualDEF; // 디버깅용 : 플레이어 방어력
        public int actualLevel;// 디버깅용 : 실제 레벨
        public int actualMaxLevel;// 디버깅용 : 돌파 구간
    };

    public DebugCharInfo[] debugCharInfo; // 디버깅용

    public Item[] Equipment_ForAnbi { get{  return equipment;  }}
    public List<Item> Inventory{get{  return  inventory; }}
    public int Wallet{ get{ return wallet; }}


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);

            equipment = new Item[3];
            debugCharInfo = new DebugCharInfo[3]; // 디버깅용

            for (int i = 0; i < debugCharInfo.Length; i++)
            {
                debugCharInfo[i].actualcurEXP = 0;
                debugCharInfo[i].actualmaxEXP = 600;
                debugCharInfo[i].actualDEF = 12f;
                debugCharInfo[i].actualLevel = 8 + i;
                debugCharInfo[i].actualMaxLevel = 10;
            }
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void WearEquipment(Item item, ECharacter eCharacter) // 장비 착용 메소드
    {
        equipment[(int)eCharacter] = item;
    }

    public void CancleEquipment(ECharacter eCharacter) // 장비 해제 메소드
    {
        equipment[(int)eCharacter] = null;
    }

    public void AddItemToInvenByDrop(Item item) // 인벤토리에 드롭된 장비 추가
    {
        inventory.Add(item);
    }

    public void AddItemToInvenByBuy(Item item, int cost) // 인벤토리에 구매한 장비 추가 + 비용 차감
    {
        inventory.Add(item);
        wallet -= cost;
    }

    public int GetAmountOfItemByTypeAndRank(Item.EItemType eItemType, Item.EItemRank eItemRank) // 현재 인벤에 있는 특정 타입과 랭크의 아이템 개수 반환
    {
        int amount = 0;

        foreach (Item item in inventory)
        {
            if (item.itemType.Equals(eItemType) && item.rank.Equals(eItemRank))
            {
                amount += 1;
            }
        }

        return amount;

    }

    public void RemoveEXPItemByTypeAndRank(Item.EItemType eItemType, Item.EItemRank eItemRank, int amount) // 돌파 혹은 레벨업시 인벤토리에서 해당 아이템을 제거합니다.
    {
        foreach (Item item in inventory)
        {
            if (amount <= 0)
                return;

            if (item.itemType.Equals(eItemType) && item.itemType.Equals(eItemRank))
            {
                inventory.Remove(item);
                amount--;
            }
        }
    }
}
