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
    [Header("���� ��ư")]
    [SerializeField]private Button purchaseBtn;

    [Header("�˾�â")]
    [SerializeField] private QuestionBox questionBox;

    [Header("������ ���ý� ǥ���� �ؽ�Ʈ")]
    // ���� ���� �� ������ info ���
    #region ChangeInfoText
    [SerializeField] private TextMeshProUGUI WeaponNameInfo; // Ŭ���� �������� �̸� info
    [SerializeField] private TextMeshProUGUI itemTypeInfo; // Ŭ���� ������ ���� info
    [SerializeField] private TextMeshProUGUI statInfo; //Ŭ���� ��������  ���� info
    [SerializeField] private TextMeshProUGUI PriceInfo; //���� Info
    private string[] typeKorean = { "���ݷ�", "ü��" }; // Ÿ�� Info
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
