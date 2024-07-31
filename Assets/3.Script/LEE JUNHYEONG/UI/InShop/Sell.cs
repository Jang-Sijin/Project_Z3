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
    [SerializeField] protected TextMeshProUGUI itemTypeInfo; // Ŭ���� ������ ���� info
    [SerializeField] private TextMeshProUGUI itemAttackStat;
    [SerializeField] private TextMeshProUGUI itemDefenceStat;
    [SerializeField] private TextMeshProUGUI itemHealthStat;
    [SerializeField] private TextMeshProUGUI PriceInfo; //���� Info
    protected string[] typeKorean = { "���ݷ�", "ü��" }; // Ÿ�� Info
    #endregion



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

    protected virtual void PrintInitText()
    {
        WeaponNameInfo.text = " ";
        itemTypeInfo.text = " ";
        statInfo.text = " ";
        PriceInfo.text = " ";
    }

    protected virtual void PrintItemStat(Item item)
    {
        Debug.Log(item.name_);
        WeaponNameInfo.text = item.name_;
        itemTypeInfo.text = typeKorean[(int)item.itemType];
        statInfo.text = ((int)item.stat).ToString();
        PrintWalletAndPrice(item);
    }

    private void PrintWalletAndPrice(Item item)
    {
        PriceInfo.text = $"{InventoryManager.instance.Wallet} / {item.sellPrice}";
    }

    private void OnClickMyItemBtn(Item item, GameObject g)
    {
        sellBtn.onClick.RemoveAllListeners();
        sellBtn.onClick.AddListener(() => OnClickSellBtn(item, g));

        PrintItemStat(item);
    }

    private void OnClickSellBtn(Item item, GameObject g)
    {
        if (g == null)
            return;

        InventoryManager.instance.AddMoneyToWallet(item.sellPrice);
        InventoryManager.instance.RemoveItem(item);

        PrintWalletAndPrice(item);

        Destroy(g);
    }

    private void OnDisable()
    {
        foreach (Transform child in itemScrollView)
        {
            Destroy(child.gameObject);
        }
    }
}
