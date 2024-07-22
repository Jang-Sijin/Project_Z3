using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitchColorChange : ToggleSwitch
{
    [Header("Elements to Recolor")]
    [SerializeField] private Image BackGroundImage;

    [Space]
    [SerializeField] private bool recolorBackground;

    [Header("Colors")]
    [SerializeField] private Color backGroundColorOff;
    [SerializeField] private Color backGroundColorOn;

    private bool isBackGroundimgNotNull;

    private void OnEnable()
    {
        transitionEffect += ChangeColors;
    }

    private void OnDisable()
    {
        transitionEffect -= ChangeColors;
    }

    protected override void Awake()
    {
        base.Awake();

        CheckForNull();
        ChangeColors();
    }

    private void CheckForNull()
    {
        isBackGroundimgNotNull = backGroundColorOff != null;
    }

    private void ChangeColors()
    {
        if (recolorBackground && isBackGroundimgNotNull)
        {
            BackGroundImage.color = Color.Lerp(backGroundColorOff, backGroundColorOn, SliderValue);
        }
    }
}
