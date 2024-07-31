using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    [SerializeField] private Transform shopScrollView;
    [SerializeField] private List<Build_Item> shopItemsList;
    public Sprite[] itemRankIcon;
    private Button itemBtn;
    [Header("���� ��ư")]
    [SerializeField] private Button purchaseBtn;

    [Header("�˾�â")]
    [SerializeField] private QuestionBox questionBox;

    [Header("������ ���ý� ǥ���� �ؽ�Ʈ")]
    // ���� ���� �� ������ info ���
    #region ChangeInfoText
    [SerializeField] private TextMeshProUGUI WeaponNameInfo; // Ŭ���� �������� �̸� info
    [SerializeField] private TextMeshProUGUI itemAttackStat;
    [SerializeField] private TextMeshProUGUI itemDefenceStat;
    [SerializeField] private TextMeshProUGUI itemHealthStat;
    [SerializeField] private TextMeshProUGUI PriceInfo; //���� Info
    private string[] typeKorean = { "���ݷ�", "ü��" }; // Ÿ�� Info
    #endregion

    [SerializeField] private Build_ShopSlotUI[] shopItemSlots;
    public void OpenShop()
    {
        foreach (var item in shopItemSlots)
        {
            item.gameObject.SetActive(false);
        }

        for (int i = 0; i < shopItemsList.Count; i++)
        {
            shopItemSlots[i].gameObject.SetActive(true);
            shopItemSlots[i].AssignItem(shopItemsList[i]);
        }
        PrintInitText();

        /*
        itemTemplate = shopScrollView.GetChild(0).gameObject;

        int len = shopItemsList.Count;

        for (int i = 0; i < len; i++)
        {
            int index = i;

            g = Instantiate(itemTemplate, shopScrollView);
            g.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = shopItemsList[i].itemIcon;
            g.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = shopItemsList[i].itemName;
            g.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = itemRankIcon[(int)shopItemsList[i].itemRank];

            g.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnClickShopItemBtn(index)); ;
        }
        Destroy(itemTemplate);
        */
    }
    private Build_Item selectedItem = null;

    private void PrintInitText()
    {
        WeaponNameInfo.text = "404 NOT FOUND";
        itemAttackStat.text = " ";
        itemDefenceStat.text = " ";
        itemHealthStat.text = " ";
        PriceInfo.text = " ";
    }

    public void PrintItemStat(Build_Item item)
    {
        //Debug.Log(itemIndex);
        selectedItem = item;
        WeaponNameInfo.text = item.itemName;
        itemAttackStat.text = item.attackStat.ToString();
        itemDefenceStat.text = item.defenceStat.ToString();
        itemHealthStat.text = item.healthStat.ToString();
        PrintWalletAndPrice(item);
    }

    private void PrintWalletAndPrice(Build_Item item)
    {
        PriceInfo.text = $"{Build_InventoryManager.INSTANCE.Wallet} / {item.buyPrice}";
    }

    private void OnClickShopItemBtn(int itemIndex)
    {
        purchaseBtn.onClick.RemoveAllListeners();
        //purchaseBtn.onClick.AddListener(() => OnClickBuyBtn(itemIndex));

        //PrintItemStat(itemIndex);
    }

    public void OnClickBuyBtn()
    {
        Debug.Log($"Buy");
        if (Build_InventoryManager.INSTANCE.Wallet < selectedItem.buyPrice)
        {
            return;
        }


        Build_InventoryManager.INSTANCE.DecreaseWallet(selectedItem.buyPrice);
        Build_InventoryManager.INSTANCE.AddToInventory(selectedItem);
        Debug.Log($"���� �Ϸ� {Build_InventoryManager.INSTANCE.WeaponInventory.Inventory.Count}");

        //PrintWalletAndPrice(itemIndex);
    }

    private void OnClickAccept(int itemIndex)
    {
        //InventoryManager.instance.RemoveMoneyFromWallet(shopItemsList[itemIndex].buyPrice);
        //InventoryManager.instance.AddItem(shopItemsList[itemIndex]);

        //PrintWalletAndPrice(itemIndex);
    }
}
