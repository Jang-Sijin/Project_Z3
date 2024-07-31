using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using Unity.VisualScripting;

public class Shop : MonoBehaviour
{
    private GameObject itemTemplate;
    private GameObject g;

    [SerializeField] private Transform shopScrollView;
    [SerializeField] private List<Build_Item> shopItemsList;
    public Sprite[] itemRankIcon;
    private Button itemBtn;
    [Header("구매 버튼")]
    [SerializeField] private Button purchaseBtn;

    [Header("팝업창")]
    [SerializeField] private QuestionBox questionBox;

    [Header("아이템 선택시 표시할 텍스트")]
    // 무기 선택 시 아이템 info 출력
    #region ChangeInfoText
    [SerializeField] private TextMeshProUGUI WeaponNameInfo; // 클릭한 아이템의 이름 info
    [SerializeField] private TextMeshProUGUI itemAttackStat;
    [SerializeField] private TextMeshProUGUI itemDefenceStat;
    [SerializeField] private TextMeshProUGUI itemHealthStat;
    [SerializeField] private TextMeshProUGUI PriceInfo; //가격 Info
    private string[] typeKorean = { "공격력", "체력" }; // 타입 Info
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
        PrintInitText();
        Destroy(itemTemplate);
        */
    }
    private Build_Item selectedItem = null;

    private void PrintInitText()
    {
        WeaponNameInfo.text = " ";
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
        Debug.Log($"구매 완료 {Build_InventoryManager.INSTANCE.WeaponInventory.Inventory.Count}");

        //PrintWalletAndPrice(itemIndex);
    }

    private void OnClickAccept(int itemIndex)
    {
        //InventoryManager.instance.RemoveMoneyFromWallet(shopItemsList[itemIndex].buyPrice);
        //InventoryManager.instance.AddItem(shopItemsList[itemIndex]);

        //PrintWalletAndPrice(itemIndex);
    }
}
