using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GearStatUI : MonoBehaviour
{
    [SerializeField] private GameObject itemTemplate;

    [SerializeField] private Transform itemScrollView;

    [SerializeField] private TextMeshProUGUI gearLevelText;

    [Header("���� �޴� UI �޴���")]
    [SerializeField] private MainCityMenuUIManager mainCityMenuUIManager;
    [SerializeField] private CharselectBtnManager charselectBtnManager;
    private ECharacter mECharacter;

    [Header("���� UI��")]
    [SerializeField] private MovePanel gearLevelUI;
    [SerializeField] private MovePanel gearLimitBreakUI;

    #region ���� �ؽ�Ʈ
    [SerializeField] protected TextMeshProUGUI WeaponNameInfo; // Ŭ���� �������� �̸� info
    [SerializeField] protected TextMeshProUGUI itemTypeInfo; // Ŭ���� ������ ���� info
    [SerializeField] protected TextMeshProUGUI statInfo; //Ŭ���� ��������  ���� info
    [SerializeField] private TextMeshProUGUI PriceInfo; //���� Info
    protected string[] typeKorean = { "���ݷ�", "ü��" }; // Ÿ�� Info
    #endregion

    #region ��� �̹��� ����
    [Header("������ ������ �̹���")]
    [SerializeField] private Image wearedItemIMG;
    [SerializeField] private Image wearedItemRank;
    #endregion

    private void OnEnable()
    {
        if (charselectBtnManager != null)
            mECharacter = charselectBtnManager.PrevCharBtn.eCharacter;

        else
            mECharacter = ECharacter.Anbi;

        PrintInitText();

        for (int i = 0; i < InventoryManager.instance.Inventory.Count; i++)
        {
            if (InventoryManager.instance.Inventory[i].itemType.Equals(Item.EItemType.DAMAGE) || InventoryManager.instance.Inventory[i].itemType.Equals(Item.EItemType.HEALTH))
            {
                AddItemToSellMenu(InventoryManager.instance.Inventory[i]);
            }
        }
    }

    private void AddItemToSellMenu(Item item) // ���������� ������ ���� public
    {
        GameObject g = Instantiate(itemTemplate, itemScrollView);
        g.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = item.itemIcon;
        g.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>().text = $"LV.{item.level}";
        g.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<Image>().sprite = item.itemRankIcon;

        //g.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnClickMyItemBtn(item, g));
    }

    private void OnClickMyItemBtn()
    {

    }

    private void PrintInitText()
    {

        if (InventoryManager.instance.Equipments[(int)mECharacter] == null)
        {
            WeaponNameInfo.text = " ";
            itemTypeInfo.text = " ";
            statInfo.text = " ";
        }

        else
        {
            PrintItemStat(InventoryManager.instance.Equipments[(int)mECharacter]);
        }
    }

    private void PrintItemStat(Item item)
    {
        WeaponNameInfo.text = item.name_;
        itemTypeInfo.text = typeKorean[(int)item.itemType];
        statInfo.text = ((int)item.stat).ToString();

        wearedItemRank.sprite = item.itemRankIcon;
        wearedItemIMG.sprite = item.itemIcon;
    }
}
