using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using static Item;
using UnityEditor.MemoryProfiler;

public class ScrollViewControl : MonoBehaviour
{


    private void Start()
    {
        items = new List<Item>();
        scrollRect = GetComponent<ScrollRect>();
        scrollRect.onValueChanged.AddListener(OnScrollValueChanged);
    }

    private List<Item> items; // 아이템의 정보들입니다.

    // 스크롤 뷰의 효과
    #region ScrollViewEFF
    private ScrollRect scrollRect; // 스크롤뷰 오브젝트의 최하단 확인을 위해서
    [SerializeField]private GameObject downPointer; // 스크롤뷰의 화살표

    private void OnScrollValueChanged(Vector2 scrollPosition) // 최하단 도착시 화살표를 없앱니다.
    {
        if (scrollRect.verticalNormalizedPosition <= 0) // 하단 도착
        {
            downPointer.SetActive(false); // 비활
        }

        else // 하단이 아닐 시
        {
            downPointer.SetActive(true); // 활성화
        }
    }
    #endregion

    // 무기 선택 시 아이템 info 출력
    #region ChangeInfoText
    [SerializeField] private TextMeshProUGUI WeaponNameInfo; // 클릭한 아이템의 이름 info
    [SerializeField] private TextMeshProUGUI itemTypeInfo; // 클릭한 아이템 종류 info 
    [SerializeField] private TextMeshProUGUI DMGInfo; //클릭한 아이템의 공격력 info
    [SerializeField] private TextMeshProUGUI PriceInfo; //가격 Info
    private string[] type = { "공격력", "체력" };


    protected virtual void ChangeWeaponInfo(Item item) // 무기 선택 시 info를 띄우는 메소드
    {
        WeaponNameInfo.text = item.name;
        DMGInfo.text = ((int)item.stat).ToString();
        PriceInfo.text = item.sellPrice.ToString();
        itemTypeInfo.text = type[(int)item.itemType];
    }
    #endregion
}
