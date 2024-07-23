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

    private void Start()
    {
        ClickBtn(firstBtn);
        prevBtn = firstBtn;
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
