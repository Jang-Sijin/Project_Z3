using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager instance = null;

    private Item equipment_ForAnbi; // 앤비의 장비입니다.
    private Item equipment_ForCorin; // 코린의 장비입니다.
    private Item equipment_ForLonginus; // 11호의 장비입니다.
    [SerializeField]private List<Item> inventory; // 인벤토리입니다.
    private int wallet; // 지갑입니다.

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

    public void WearEquipment(Item item, string charName) // 장비 착용 메소드
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
                Debug.Log($"{charName} 이런 캐릭터는 없습니다.");
                break;
        }
    }

    public void CancleEquipment(string charName) // 장비 해제 메소드
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
                Debug.Log($"{charName} 이런 캐릭터는 없습니다.");
                break;
        }
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
