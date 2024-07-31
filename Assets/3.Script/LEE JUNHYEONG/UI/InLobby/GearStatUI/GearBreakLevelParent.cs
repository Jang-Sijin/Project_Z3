//using System.Collections;
//using System.Collections.Generic;
//using TMPro;
//using UnityEngine;
//using UnityEngine.TextCore.Text;
//using UnityEngine.UI;
//
//public class GearBreakLevelParent : MonoBehaviour
//{
//    protected Item selectedItem;
//
//    #region 텍스트
//    [Header("부모 텍스트")]
//    [SerializeField] protected TextMeshProUGUI itemTypeText;
//    [SerializeField] protected TextMeshProUGUI[] itemStatText; // index  = 0 -> 현재 스탯 index = 1 -> 예상 스탯
//    [SerializeField] protected TextMeshProUGUI itemAmountText;
//    #endregion
//
//    #region 이미지
//    [Header("부모 이미지")]
//    [SerializeField] protected Image itemImage; // 강화 아이템
//    #endregion
//
//    #region 변수
//    protected float expectedStat; // 예상 가중치
//    protected int amountOfItem; // 아이템 개수
//
//    protected readonly int[] amountOfRequireItem = { 2, 4, 4, 6 }; // 진급시 필요한 아이템 수
//    // 20레벨 : A아이템 2개
//    // 30레벨 : A아이템 4개
//    // 40레벨 : S아이템 4개
//    // 50레벨 : S아이템 6개
//    #endregion
//    [Header("부모 강화 아이템")]
//    [SerializeField] protected Item itemA;
//    [SerializeField] protected Item itemS;
//
//    private void OnEnable()
//    {
//        AssignAmountOfItem();
//    }
//
//    public void GetSelectedItem(Item item)
//    {
//        selectedItem = item;
//    }
//
//    protected void AssignAmountOfItem() // 아이템의 총 개수 캐싱
//    {
//        switch (selectedItem.level)
//        {
//            case 20:
//                amountOfItem = InventoryManager.instance.GetAmountOfItemByItem(itemA);
//                break;
//
//            case 30:
//                amountOfItem = InventoryManager.instance.GetAmountOfItemByItem(itemA);
//                break;
//
//            case 40:
//                amountOfItem = InventoryManager.instance.GetAmountOfItemByItem(itemS);
//                break;
//
//            case 50:
//                amountOfItem = InventoryManager.instance.GetAmountOfItemByItem(itemS);
//                break;
//
//            default:
//                Debug.Log("비정상적인 레벨 제한입니다.");
//                break;
//        }
//    }
//}
