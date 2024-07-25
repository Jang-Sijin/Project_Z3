using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectbtnEff : MonoBehaviour
{
    private Animator buttonAni;

    private void Awake()
    {
        buttonAni = GetComponentInChildren<Animator>();
    }

    public void OnClickButton()
    {
        StartAni();
    }

    private void StartAni()
    {
        buttonAni.SetTrigger("Selected");
    }

    public void turnOff()
    {
        buttonAni.SetTrigger("Normal");
    }
}
