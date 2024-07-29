using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    public Item[] rankSItems; // Rank S ������ ���
    public Item[] rankBItems; // Rank B ������ ���
    public Item[] weaponItems;
    public int currentStage; // ���� �������� ��ȣ
    public ShowItemInfo itemInfo; // �ν����Ϳ��� �Ҵ��� �� �ֵ��� public���� ����
  

    public float weaponDropPercent;

    public void DropItem()
    {
        int getWeapon = Random.Range(0, 100);
        Item[] possibleItems = GetPossibleItems();

        if (currentStage == 5 && getWeapon < weaponDropPercent)
        {
            // �������� 5���� ��� �������� ����� Ȯ���� weaponDropPercent���� ������ ��� ������ ���
            if (weaponItems.Length > 0)
            {
                Item selectWeapon = weaponItems[Random.Range(0, weaponItems.Length)];
                Debug.Log(selectWeapon);

                itemInfo.UpdateUI(selectWeapon); // ������ ������ UI�� ������Ʈ

                // �κ��丮�� �߰��ϴ� ���� ���� �� 
                return;
            }
        }

        if (possibleItems.Length > 0)
        {
            // �������� ����
            Item selectItem = possibleItems[Random.Range(0, possibleItems.Length)];
            Debug.Log(selectItem);

            itemInfo.UpdateUI(selectItem); // ������ ������ UI�� ������Ʈ

            // �κ��丮�� �߰��ϴ� ���� ���� �� 
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
