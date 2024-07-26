using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    public FieldItem[] rankAItems; // Rank A 아이템 목록
    public FieldItem[] rankBItems; // Rank B 아이템 목록
    public int currentStage; // 현재 스테이지 번호
    private ShowItemInfo itemInfo;

    public float weaponDropPercent;


    public void DropItem()
    {
        int getWeapon = Random.Range(0, 100);
     //   if(getWeapon< weaponDropPercent) {}    장비 인벤토리에 추가하는 매서드호출
        FieldItem[] possibleItems = GetPossibleItems();
        if(possibleItems.Length>0)
        {
            //랜덤으로 선택
            FieldItem selectItem= possibleItems[Random.Range(0, possibleItems.Length)];
            Debug.Log(selectItem);

           // itemInfo.UpdateUI(selectItem);

            //인벤토리에 추가하는 로직 넣을 것 

        }
    }

    private FieldItem[] GetPossibleItems()
    {
        if (currentStage == 1 || currentStage == 2) return rankBItems;
        else if (currentStage == 2 || currentStage == 3) return rankAItems;
        else if (currentStage == 5) return rankAItems;  // 
        else return new FieldItem[0];
    }
}
