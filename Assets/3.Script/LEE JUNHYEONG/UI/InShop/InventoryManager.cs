using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance = null;

    private Item equipment_ForAnbi; // �غ��� ����Դϴ�.
    private Item equipment_ForCorin; // �ڸ��� ����Դϴ�.
    private Item equipment_ForLonginus; // 11ȣ�� ����Դϴ�.
    [SerializeField]private List<Item> inventory; // �κ��丮�Դϴ�.
    private int wallet; // �����Դϴ�.

    public Item Equipment_ForAnbi { get{  return equipment_ForAnbi;  }}
    public List<Item> INventory{get{  return  inventory; }}
    public int Wallet{ get{ return wallet; }}


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
            inventory = new List<Item>();
        }

        else
        {
            Destroy(gameObject);
        }
    }

    public void WearEquipment(Item item, string charName) // ��� ���� �޼ҵ�
    {
        switch (charName)
        {
            case "Anbi":
                equipment_ForAnbi = item;
                break;

            case "Corin":
                equipment_ForCorin = item;
                break;

            case "Longinus":
                equipment_ForLonginus = item;
                break;

            default:
                Debug.Log($"{charName} �̷� ĳ���ʹ� �����ϴ�.");
                break;
        }
    }

    public void CancleEquipment(string charName) // ��� ���� �޼ҵ�
    {
        switch (charName)
        {
            case "Anbi":
                equipment_ForAnbi = null;
                break;

            case "Corin":
                equipment_ForCorin = null;
                break;

            case "Longinus":
                equipment_ForLonginus = null;
                break;

            default:
                Debug.Log($"{charName} �̷� ĳ���ʹ� �����ϴ�.");
                break;
        }
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
