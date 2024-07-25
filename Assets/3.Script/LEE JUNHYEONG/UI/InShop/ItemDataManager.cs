using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    /*
     * ����Ʈ�� ��� �������� ������ ��� �ִ´�
    */
    public static ItemDataManager instance = null;

    [SerializeField] private List<Item> itemInfo;
    public List<Item> ItemInfo
    {
        get
        {
            return itemInfo;
        }
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }

        else
        {
            Destroy(this.gameObject);
        }
    }
}
