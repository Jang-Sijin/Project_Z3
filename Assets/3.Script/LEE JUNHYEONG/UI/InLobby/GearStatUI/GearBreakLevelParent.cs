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
//    #region �ؽ�Ʈ
//    [Header("�θ� �ؽ�Ʈ")]
//    [SerializeField] protected TextMeshProUGUI itemTypeText;
//    [SerializeField] protected TextMeshProUGUI[] itemStatText; // index  = 0 -> ���� ���� index = 1 -> ���� ����
//    [SerializeField] protected TextMeshProUGUI itemAmountText;
//    #endregion
//
//    #region �̹���
//    [Header("�θ� �̹���")]
//    [SerializeField] protected Image itemImage; // ��ȭ ������
//    #endregion
//
//    #region ����
//    protected float expectedStat; // ���� ����ġ
//    protected int amountOfItem; // ������ ����
//
//    protected readonly int[] amountOfRequireItem = { 2, 4, 4, 6 }; // ���޽� �ʿ��� ������ ��
//    // 20���� : A������ 2��
//    // 30���� : A������ 4��
//    // 40���� : S������ 4��
//    // 50���� : S������ 6��
//    #endregion
//    [Header("�θ� ��ȭ ������")]
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
//    protected void AssignAmountOfItem() // �������� �� ���� ĳ��
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
//                Debug.Log("���������� ���� �����Դϴ�.");
//                break;
//        }
//    }
//}
