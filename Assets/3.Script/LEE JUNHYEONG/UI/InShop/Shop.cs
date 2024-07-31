using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;

public class Shop : MonoBehaviour
{
    private GameObject itemTemplate;
    private GameObject g;

    [SerializeField] private Transform shopScrollView;
    [SerializeField] private List<Item> shopItemsList;
    private Button itemBtn;
    [Header("구매 버튼")]
    [SerializeField]private Button purchaseBtn;

    [Header("팝업창")]
    [SerializeField] private QuestionBox questionBox;

    [Header("아이템 선택시 표시할 텍스트")]
    // 무기 선택 시 아이템 info 출력
    #region ChangeInfoText
    [SerializeField] private TextMeshProUGUI WeaponNameInfo; // 클릭한 아이템의 이름 info
    [SerializeField] private TextMeshProUGUI itemTypeInfo; // 클릭한 아이템 종류 info
    [SerializeField] private TextMeshProUGUI statInfo; //클릭한 아이템의  스탯 info
    [SerializeField] private TextMeshProUGUI PriceInfo; //가격 Info
    private string[] typeKorean = { "공격력", "체력" }; // 타입 Info
    #endregion

    private void Start()
    {
        itemTemplate = shopScrollView.GetChild(0).gameObject;

        int len = shopItemsList.Count;

        for (int i = 0; i < len; i++)
        {
            int index = i;

            g = Instantiate(itemTemplate, shopScrollView);
            g.transform.GetChild(0).GetChild(0).GetComponent<Image>().sprite = shopItemsList[i].itemIcon;
            g.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>().text = shopItemsList[i].name_;
            g.transform.GetChild(0).GetChild(2).GetComponent<Image>().sprite = shopItemsList[i].itemRankIcon;

            g.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnClickShopItemBtn(index)); ;
        }
        PrintInitText();
        Destroy(itemTemplate);
    }

    private void PrintInitText()
    {
        WeaponNameInfo.text = " ";
        itemTypeInfo.text = " ";
        statInfo.text = " ";
        PriceInfo.text = " ";
    }

    private void PrintItemStat(int itemIndex)
    {
        Debug.Log(itemIndex);
        WeaponNameInfo.text = shopItemsList[itemIndex].name_;
        itemTypeInfo.text = typeKorean[(int)shopItemsList[itemIndex].itemType];
        statInfo.text = ((int)shopItemsList[itemIndex].stat).ToString();
        PrintWalletAndPrice(itemIndex);
    }

    private void PrintWalletAndPrice(int itemIndex)
    {
        PriceInfo.text = $"{InventoryManager.instance.Wallet} / {shopItemsList[itemIndex].buyPrice}";
    }

    private void OnClickShopItemBtn(int itemIndex)
    {
        purchaseBtn.onClick.RemoveAllListeners();
        purchaseBtn.onClick.AddListener(() => OnClickBuyBtn(itemIndex));

        PrintItemStat(itemIndex);
    }

    private void OnClickBuyBtn(int itemIndex)
    {
        if (InventoryManager.instance.Wallet < shopItemsList[itemIndex].buyPrice)
        {
            return;
        }

        InventoryManager.instance.RemoveMoneyFromWallet(shopItemsList[itemIndex].buyPrice);
        InventoryManager.instance.AddItem(shopItemsList[itemIndex]);

        PrintWalletAndPrice(itemIndex);
    }

    private void OnClickAccept(int itemIndex)
    {
        InventoryManager.instance.RemoveMoneyFromWallet(shopItemsList[itemIndex].buyPrice);
        InventoryManager.instance.AddItem(shopItemsList[itemIndex]);

        PrintWalletAndPrice(itemIndex);
    }
}
