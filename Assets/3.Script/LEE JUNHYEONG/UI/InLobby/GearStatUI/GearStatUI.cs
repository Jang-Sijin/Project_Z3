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

    [Header("메인 메뉴 UI 메니저")]
    [SerializeField] private MainCityMenuUIManager mainCityMenuUIManager;
    [SerializeField] private CharselectBtnManager charselectBtnManager;
    private ECharacter mECharacter;

    [Header("하위 UI들")]
    [SerializeField] private MovePanel gearLevelUI;
    [SerializeField] private MovePanel gearLimitBreakUI;

    #region 각종 텍스트
    [SerializeField] protected TextMeshProUGUI WeaponNameInfo; // 클릭한 아이템의 이름 info
    [SerializeField] protected TextMeshProUGUI itemTypeInfo; // 클릭한 아이템 종류 info
    [SerializeField] protected TextMeshProUGUI statInfo; //클릭한 아이템의  스탯 info
    [SerializeField] private TextMeshProUGUI PriceInfo; //가격 Info
    protected string[] typeKorean = { "공격력", "체력" }; // 타입 Info
    #endregion

    #region 출력 이미지 변수
    [Header("장착한 아이템 이미지")]
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

    private void AddItemToSellMenu(Item item) // 상점에서도 갱신을 위해 public
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
