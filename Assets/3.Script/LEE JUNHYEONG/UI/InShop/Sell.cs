using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Sell : MonoBehaviour
{
    [SerializeField] private GameObject itemTemplate;

    [SerializeField] private Transform itemScrollView;
    [Header("�Ǹ� ��ư")]
    [SerializeField] private Button sellBtn;

    [Header("������ ���ý� ǥ���� �ؽ�Ʈ")]
    // ���� ���� �� ������ info ���
    #region ChangeInfoText
    [SerializeField] protected TextMeshProUGUI WeaponNameInfo; // Ŭ���� �������� �̸� info
    [SerializeField] private TextMeshProUGUI itemAttackStat;
    [SerializeField] private TextMeshProUGUI itemDefenceStat;
    [SerializeField] private TextMeshProUGUI itemHealthStat;
    [SerializeField] private TextMeshProUGUI PriceInfo; //���� Info
    protected string[] typeKorean = { "���ݷ�", "ü��" }; // Ÿ�� Info
    #endregion

    [SerializeField] private Build_ShopSlotUI[] itemSlots;

    /*
        protected virtual void OnEnable()
        {
            for (int i = 0; i < InventoryManager.instance.Inventory.Count; i++)
            {
                if (InventoryManager.instance.Inventory[i].itemType.Equals(Item.EItemType.DAMAGE) || InventoryManager.instance.Inventory[i].itemType.Equals(Item.EItemType.HEALTH))
                {
                    AddItemToSellMenu(InventoryManager.instance.Inventory[i]);
                }
            }

            PrintInitText();
        }
        */

    public void RefreshInventory()
    {
        foreach (var item in itemSlots)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < Build_InventoryManager.INSTANCE.WeaponInventory.Inventory.Count; i++)
        {
            if (Build_PlayerManager.INSTANCE.Anbi.Equipment == itemSlots[i]) continue;
            if (Build_PlayerManager.INSTANCE.Corin.Equipment == itemSlots[i]) continue;
            if (Build_PlayerManager.INSTANCE.Longinus.Equipment == itemSlots[i]) continue;
            itemSlots[i].gameObject.SetActive(true);
            itemSlots[i].AssignItem(Build_InventoryManager.INSTANCE.WeaponInventory.Inventory[i]);
        }
    }

    /*
    public void AddItemToSellMenu(Item item)
    {
        Debug.Log(item);
        Debug.Log(itemTemplate);
        Debug.Log(itemScrollView);
        GameObject g = Instantiate(itemTemplate, itemScrollView);
        g.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = item.itemIcon;
        g.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"LV.{item.level}";
        g.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>().sprite = item.itemRankIcon;

        g.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnClickMyItemBtn(item, g));
    }
    */

    protected virtual void PrintInitText()
    {
        WeaponNameInfo.text = " ";
        itemAttackStat.text = " ";
        itemDefenceStat.text = " ";
        itemHealthStat.text = " ";
    }

    private Build_Item itemData;
    public void PrintItemStat(Build_Item item)
    {
        itemData = item;
        //Debug.Log(item.name_);
        WeaponNameInfo.text = itemData.itemName;
        itemAttackStat.text = itemData.attackStat.ToString();
        itemDefenceStat.text = itemData.defenceStat.ToString();
        itemHealthStat.text = itemData.healthStat.ToString();
        PrintWalletAndPrice(item);
    }

    private void PrintWalletAndPrice(Build_Item item)
    {
        PriceInfo.text = $"{item.sellPrice}";
    }

    private void OnClickMyItemBtn(Item item, GameObject g)
    {
        sellBtn.onClick.RemoveAllListeners();
        //sellBtn.onClick.AddListener(() => OnClickSellBtn(item, g));

        //PrintItemStat(item);
    }

    public void OnClickSellBtn()
    {
        Build_InventoryManager.INSTANCE.IncreaseWallet(itemData.sellPrice);
        Build_InventoryManager.INSTANCE.RemoveFromInventory(itemData);
        RefreshInventory();
        //PrintWalletAndPrice(item);

        //Destroy(g);
    }

    private void OnDisable()
    {
        foreach (Transform child in itemScrollView)
        {
            //Destroy(child.gameObject);
        }
    }
}
