using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropManager : MonoBehaviour
{
    public Item[] rankSItems; // Rank S ������ ���
    public Item[] rankBItems; // Rank B ������ ���
    public int currentStage; // ���� �������� ��ȣ
    public ShowItemInfo itemInfo; // �ν����Ϳ��� �Ҵ��� �� �ֵ��� public���� ����
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
            // �������� ����
            Item selectItem = possibleItems[Random.Range(0, possibleItems.Length)];
            Debug.Log(selectItem);

            itemInfo.UpdateUI(selectItem); // ������ ������ UI�� ������Ʈ
            hasChooseItem = true;

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
