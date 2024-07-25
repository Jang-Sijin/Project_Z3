using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class ItemDataManager : MonoBehaviour
{
    /*
     * 리스트로 모든 아이템의 정보를 들고 있는다
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
