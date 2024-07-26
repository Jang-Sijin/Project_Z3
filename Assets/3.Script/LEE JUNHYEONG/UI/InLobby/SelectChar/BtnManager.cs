using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEditor;
using UnityEngine;
using Unity.VisualScripting;

public class BtnManager : MonoBehaviour
{
    private SelectbtnEff prevBtn;
    [SerializeField] private MainCityMenuUIManager mainCityMenuUIManager;
    [SerializeField] private GameObject EquipmentUI;
    [SerializeField] private GameObject StatUI;

    public void ClickCharBtn(SelectbtnEff clickedBtn)
    {
        if (clickedBtn == prevBtn)
            return;

        if (prevBtn != null)
            prevBtn.turnOff();

        clickedBtn.OnClickButton();
        prevBtn = clickedBtn;
    }

    public void ClickEquipmentBTN()
    {
        if (prevBtn == null) return;
        mainCityMenuUIManager.TurnOnMenuByOBJ(EquipmentUI);
    }

    public void ClickStatBTN()
    {
        if (prevBtn == null) return;
        mainCityMenuUIManager.ChangeToOtherMenuEFF();
        EquipmentUI.gameObject.SetActive(true);
    }
}