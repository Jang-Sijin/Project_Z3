using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEditor;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
    [SerializeField] private SelectbtnEff firstBtn;
    [SerializeField] private SelectbtnEff secondBtn;

    private void Start()
    {
        if (secondBtn != null)
            prevBtn = secondBtn;

        ClickBtn(firstBtn);
    }

    private SelectbtnEff prevBtn;

    public void ClickBtn(SelectbtnEff clickedBtn)
    {
        if (clickedBtn == prevBtn)
            return;

        if (prevBtn != null)
            prevBtn.turnOff();

        clickedBtn.OnClickButton();
        prevBtn = clickedBtn;
    }
}