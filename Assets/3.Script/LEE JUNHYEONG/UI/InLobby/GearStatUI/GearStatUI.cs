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
//    [Header("메인 메뉴 UI 메니저")]
//    [SerializeField] private MainCityMenuUIManager mainCityMenuUIManager;
//    [SerializeField] private CharselectBtnManager charselectBtnManager;
//    private ECharacter mECharacter;
//
//    [Header("하위 UI들")]
//    [SerializeField] private MovePanel gearLevelUI;
//    [SerializeField] private MovePanel gearLimitBreakUI;
//    [SerializeField] private GameObject childBlackBG;
//
//    #region 각종 텍스트
//    [Header("상태 출력용 텍스트")]
//    [SerializeField] private TextMeshProUGUI WeaponNameInfoText; // 클릭한 아이템의 이름 info
//    [SerializeField] private TextMeshProUGUI itemTypeInfoText; // 클릭한 아이템 종류 info
//    [SerializeField] private TextMeshProUGUI statInfoText; //클릭한 아이템의  스탯 info
//    [SerializeField] private TextMeshProUGUI levelInfoText; // 아이템 레벨 Info
//    protected string[] typeKorean = { "공격력", "체력" }; // 타입 Info
//    #endregion
//
//    [Header("캐릭터 아이콘")]
//    [SerializeField] private Sprite[] charIcons;
//
//    #region 출력 이미지 변수
//    [Header("장착한 아이템 이미지")]
//    [SerializeField] private Image wearedItemIMG;
//    [SerializeField] private Image wearedItemRank;
//    #endregion
//
//    #region 상호작용 버튼
//    [Header("상호작용 버튼")]
//    [SerializeField] private Button wearBtn;
//    [SerializeField] private Button unwearBtn;
//    [SerializeField] private Button levelUpBtn;
//    #endregion
//
//    #region 애니메이터
//    [Header("애니메이터")]
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
//    private void AddItemToSellMenu(Item item) // 상점에서도 갱신을 위해 public
//    { 
//        GameObject g = Instantiate(itemTemplate, itemScrollView); // 아이템 생성
//        g.transform.GetChild(0).GetChild(1).GetComponent<Image>().sprite = item.itemIcon; // 아이템 고유 아이콘
//        g.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>().text = $"LV.{item.level}"; // 아이템 레벨
//        g.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<Image>().sprite = item.itemRankIcon; // 아이템 랭크
//
//        g.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => OnClickMyItemBtn(item)); // 아이템 클릭 로직 구독
//        ItemHolder itemHolder = g.GetComponent<ItemHolder>();
//        itemHolder.item = item;
//
//        PrintCharIcon(itemHolder);
//    }
//    #region 클릭 메소드
//    #endregion
//
//
//    #region 출력 메소드
//    private void PrintInitText() // 본 객체가 불릴 시 초기화하는 메소드
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
//    private void PrintItemStat(Item item) // 선택한 아이템의 스탯을 보여줍니다.
//    {
//        WeaponNameInfoText.text = item.name_;
//        itemTypeInfoText.text = typeKorean[(int)item.itemType];
//        statInfoText.text = ((int)item.stat).ToString();
//        levelInfoText.text = $"LV.{item.level}/{item.MaxLevel}";
//
//        wearedItemRank.sprite = item.itemRankIcon;
//    }
//
//    private void PrintCharIcon(ItemHolder newItem) // 캐릭터의 장착 여부 UI에 표시
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
//    #region 버튼 이벤트
//    private void OnClickMyItemBtn(Item item) // 아이템을 클릭했을 때
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
//    private void OnClickWearBtn(Item item) // 아이템 착용 버튼
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
//    private void OnClickUnwearBtn() // 아이템 해제 메소드
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
