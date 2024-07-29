using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryUI : MonoBehaviour
{
    public Slot[] slots;
    public Transform slotHolder;

    private void Start()
    {
        slots = slotHolder.GetComponentsInChildren<Slot>();
    }
}
