using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    public Item[] rankSItems; // Rank S 아이템 목록
    public Item[] rankBItems; // Rank B 아이템 목록
    public int currentStage; // 현재 스테이지 번호
    public ShowItemInfo itemInfo; // 인스펙터에서 할당할 수 있도록 public으로 변경
    private bool hasChooseItem = false;

    public float weaponDropPercent;

    public void DropItem()
    {
        if (itemInfo == null)
        {
            Debug.LogError("itemInfo is not assigned!");
            return;
        }

        int getWeapon = Random.Range(0, 100);
        Item[] possibleItems = GetPossibleItems();
        if (possibleItems.Length > 0 && !hasChooseItem)
        {
            // 랜덤으로 선택
            Item selectItem = possibleItems[Random.Range(0, possibleItems.Length)];
            Debug.Log(selectItem);

            itemInfo.UpdateUI(selectItem); // 아이템 정보를 UI에 업데이트
            hasChooseItem = true;

            // 인벤토리에 추가하는 로직 넣을 것 
        }
    }

    private Item[] GetPossibleItems()
    {
        if (currentStage == 1 || currentStage == 2) return rankBItems;
        else if (currentStage == 3 || currentStage == 4) return rankSItems;
        else if (currentStage == 5) return rankSItems;
        else return new Item[0];
    }
}
