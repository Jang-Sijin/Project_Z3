//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using Unity.VisualScripting;
//using UnityEngine;
//using UnityEngine.UI;
//
//public class GearStatUI : MonoBehaviour
//{
//    [SerializeField] private GameObject itemTemplate;
//
//    [SerializeField] private Transform itemScrollView;
//
//    [SerializeField] private TextMeshProUGUI gearLevelText;
//
//    [Header("���� �޴� UI �޴���")]
//    [SerializeField] private MainCityMenuUIManager mainCityMenuUIManager;
//    [SerializeField] private CharselectBtnManager charselectBtnManager;
//    private ECharacter mECharacter;
//
//    [Header("���� UI��")]
//    [SerializeField] private MovePanel gearLevelUI;
//    [SerializeField] private MovePanel gearLimitBreakUI;
//    [SerializeField] private GameObject childBlackBG;
//
//    #region ���� �ؽ�Ʈ
//    [Header("���� ��¿� �ؽ�Ʈ")]
//    [SerializeField] private TextMeshProUGUI WeaponNameInfoText; // Ŭ���� �������� �̸� info
//    [SerializeField] private TextMeshProUGUI itemTypeInfoText; // Ŭ���� ������ ���� info
//    [SerializeField] private TextMeshProUGUI statInfoText; //Ŭ���� ��������  ���� info
//    [SerializeField] private TextMeshProUGUI levelInfoText; // ������ ���� Info
//    protected string[] typeKorean = { "���ݷ�", "ü��" }; // Ÿ�� Info
//    #endregion
//
//    [Header("ĳ���� ������")]
//    [SerializeField] private Sprite[] charIcons;
//
//    #region ��� �̹��� ����
//    [Header("������ ������ �̹���")]
//    [SerializeField] private Image wearedItemIMG;
//    [SerializeField] private Image wearedItemRank;
//    #endregion
//
//    #region ��ȣ�ۿ� ��ư
//    [Header("��ȣ�ۿ� ��ư")]
//    [SerializeField] private Button wearBtn;
//    [SerializeField] private Button unwearBtn;
//    [SerializeField] private Button levelUpBtn;
//    #endregion
//
//    #region �ִϸ�����
//    [Header("�ִϸ�����")]
//    [SerializeField] private Animator equipAni;
//    #endregion
//
//    private void OnEnable()
//    {
//        if (charselectBtnManager != null)
//            mECharacter = charselectBtnManager.PrevCharBtn.eCharacter;
//
//        else
//            mECharacter = ECharacter.Anbi;
//
//        PrintInitText();
//
//        for (int i = 0; i < InventoryManager.instance.Inventory.Count; i++)
//        {
//            if (InventoryManager.instance.Inventory[i].itemType.Equals(Item.EItemType.DAMAGE) || InventoryManager.instance.Inventory[i].itemType.Equals(Item.EItemType.HEALTH))
//            {
//                AddItemToSellMenu(InventoryManager.instance.Inventory[i]);
//            }
//        }
//    }
//
//    private void AddItemToSellMenu(Item item) // ���������� ������ ���� public
//    { 
//        GameObject g = Instantiate(itemTemplate, itemScrollView); // ������ ����
//        g.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = item.itemIcon; // ������ ���� ������
//        g.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = $"LV.{item.level}"; // ������ ����
//        g.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>().sprite = item.itemRankIcon; // ������ ��ũ
//
//        g.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnClickMyItemBtn(item)); // ������ Ŭ�� ���� ����
//        ItemHolder itemHolder = g.GetComponent<ItemHolder>();
//        itemHolder.item = item;
//
//        PrintCharIcon(itemHolder);
//    }
//    #region Ŭ�� �޼ҵ�
//    #endregion
//
//
//    #region ��� �޼ҵ�
//    private void PrintInitText() // �� ��ü�� �Ҹ� �� �ʱ�ȭ�ϴ� �޼ҵ�
//    {
//        if (InventoryManager.instance.Equipments[(int)mECharacter] == null)
//        {
//            WeaponNameInfoText.text = " ";
//            itemTypeInfoText.text = " ";
//            statInfoText.text = " ";
//            equipAni.SetTrigger("UnEquip");
//        }
//
//        else
//        {
//            PrintItemStat(InventoryManager.instance.Equipments[(int)mECharacter]);
//            equipAni.SetTrigger("Equip");
//        }
//    }
//
//    private void PrintItemStat(Item item) // ������ �������� ������ �����ݴϴ�.
//    {
//        WeaponNameInfoText.text = item.name_;
//        itemTypeInfoText.text = typeKorean[(int)item.itemType];
//        statInfoText.text = ((int)item.stat).ToString();
//        levelInfoText.text = $"LV.{item.level}/{item.MaxLevel}";
//
//        wearedItemRank.sprite = item.itemRankIcon;
//    }
//
//    private void PrintCharIcon(ItemHolder newItem) // ĳ������ ���� ���� UI�� ǥ��
//    {
//        Image tempSprite = newItem.gameObject.transform.GetChild(1).GetComponent<Image>();
//        tempSprite.gameObject.SetActive(false);
//
//        for (int i = 0; i < (int)ECharacter.Longinus + 1; i++)
//        {
//            if (InventoryManager.instance.Equipments[i] != null)
//            {
//                if (InventoryManager.instance.Equipments[i].Equals(newItem.item))
//                {
//                    tempSprite.gameObject.SetActive(true);
//                    tempSprite.sprite = charIcons[i];
//                }
//            }
//        }
//    }
//
//    #endregion
//
//    #region ��ư �̺�Ʈ
//    private void OnClickMyItemBtn(Item item) // �������� Ŭ������ ��
//    {
//        wearBtn.onClick.RemoveAllListeners();
//        unwearBtn.onClick.RemoveAllListeners();
//        levelUpBtn.onClick.RemoveAllListeners();
//
//        PrintItemStat(item);
//
//        wearBtn.onClick.AddListener (() => OnClickWearBtn (item));
//        unwearBtn.onClick.AddListener(OnClickUnwearBtn);
//
//        if (item.level.Equals(item.MaxLevel))
//        {
//            levelUpBtn.onClick.AddListener(() => gearLimitBreakUI.GetComponent<GearBreakUI>().GetSelectedItem(item));
//        }
//
//        else
//        {
//            levelUpBtn.onClick.AddListener(() => gearLevelUI.GetComponent<GearLevelUpUI>().GetSelectedItem(item));
//        }
//
//        levelUpBtn.onClick.AddListener(() => OnClickLevelUpBtn(item));
//    }
//
//    private void OnClickWearBtn(Item item) // ������ ���� ��ư
//    {
//        InventoryManager.instance.Equipments[(int)mECharacter] = item;
//
//        foreach (Transform itemHolder in itemScrollView)
//        {
//            PrintCharIcon(itemHolder.GetComponent<ItemHolder>());
//        }
//        wearedItemIMG.gameObject.SetActive(true);
//        wearedItemIMG.sprite = item.itemIcon;
//
//        equipAni.SetTrigger("Equip");
//    }
//
//    private void OnClickUnwearBtn() // ������ ���� �޼ҵ�
//    {
//        InventoryManager.instance.Equipments[(int)mECharacter] = null;
//        equipAni.SetTrigger("UnEquip");
//    }
//
//    private void OnClickLevelUpBtn(Item item)
//    {
//        levelUpBtn.onClick.RemoveAllListeners();
//        childBlackBG.SetActive(true);
//
//        if (item.level.Equals(item.MaxLevel))
//        {
//            gearLimitBreakUI.gameObject.SetActive(true);
//            gearLimitBreakUI.GoToTargetPos();
//        }
//
//        else
//        {
//            gearLevelUI.gameObject.SetActive(true);
//            gearLevelUI.GoToTargetPos();
//        }
//    }
//
//    public void TurnOffBlackBG()
//    {
//        childBlackBG.SetActive(false);
//    }
//
//    #endregion
//}
