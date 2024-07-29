using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance = null;

    private Item[] equipment; //����Դϴ�.
    [SerializeField]private List<Item> inventory; // �κ��丮�Դϴ�.
    private int wallet; // �����Դϴ�.

    public struct DebugCharInfo // �÷��̾ �߰��� �ʿ��� �ɷ�ġ
    {
        public float actualmaxEXP; // ������ : �÷��̾� �ִ� ����ġ
        public float actualcurEXP; // ������ : �÷��̾� ���� ����ġ
        public float actualDEF; // ������ : �÷��̾� ����
        public int actualLevel;// ������ : ���� ����
        public int actualMaxLevel;// ������ : ���� ����
    };

    public DebugCharInfo[] debugCharInfo; // ������

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
            debugCharInfo = new DebugCharInfo[3]; // ������

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

    public void WearEquipment(Item item, ECharacter eCharacter) // ��� ���� �޼ҵ�
    {
        equipment[(int)eCharacter] = item;
    }

    public void CancleEquipment(ECharacter eCharacter) // ��� ���� �޼ҵ�
    {
        equipment[(int)eCharacter] = null;
    }

    public void AddItemToInvenByDrop(Item item) // �κ��丮�� ��ӵ� ��� �߰�
    {
        inventory.Add(item);
    }

    public void AddItemToInvenByBuy(Item item, int cost) // �κ��丮�� ������ ��� �߰� + ��� ����
    {
        inventory.Add(item);
        wallet -= cost;
    }

    public int GetAmountOfItemByTypeAndRank(Item.EItemType eItemType, Item.EItemRank eItemRank) // ���� �κ��� �ִ� Ư�� Ÿ�԰� ��ũ�� ������ ���� ��ȯ
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

    public void RemoveEXPItemByTypeAndRank(Item.EItemType eItemType, Item.EItemRank eItemRank, int amount) // ���� Ȥ�� �������� �κ��丮���� �ش� �������� �����մϴ�.
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
