using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.Search;
using UnityEditor;
using UnityEngine;

public class BtnManager : MonoBehaviour
{
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