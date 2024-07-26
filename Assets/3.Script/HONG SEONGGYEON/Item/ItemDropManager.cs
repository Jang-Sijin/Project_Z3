using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    public FieldItem[] rankAItems; // Rank A ������ ���
    public FieldItem[] rankBItems; // Rank B ������ ���
    public int currentStage; // ���� �������� ��ȣ
    private ShowItemInfo itemInfo;

    public float weaponDropPercent;


    public void DropItem()
    {
        int getWeapon = Random.Range(0, 100);
     //   if(getWeapon< weaponDropPercent) {}    ��� �κ��丮�� �߰��ϴ� �ż���ȣ��
        FieldItem[] possibleItems = GetPossibleItems();
        if(possibleItems.Length>0)
        {
            //�������� ����
            FieldItem selectItem= possibleItems[Random.Range(0, possibleItems.Length)];
            Debug.Log(selectItem);

           // itemInfo.UpdateUI(selectItem);

            //�κ��丮�� �߰��ϴ� ���� ���� �� 

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
