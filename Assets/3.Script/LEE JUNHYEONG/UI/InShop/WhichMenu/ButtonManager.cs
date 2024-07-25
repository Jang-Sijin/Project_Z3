using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEditor;
using UnityEngine;

public class ButtonManager : MonoBehaviour
{
    [SerializeField] private ButtonSelectedEff firstBtn;
    [SerializeField] private ButtonSelectedEff secondBtn;

    private void Start()
    {
        prevBtn = secondBtn;
        ClickBtn(firstBtn);
    }

    private ButtonSelectedEff prevBtn;

    public void ClickBtn(ButtonSelectedEff clickedBtn)
    {
        if (clickedBtn == prevBtn)
            return;

        if (prevBtn != null)
            prevBtn.turnOff();

            clickedBtn.OnClickButton();
            prevBtn = clickedBtn;
    }
}
