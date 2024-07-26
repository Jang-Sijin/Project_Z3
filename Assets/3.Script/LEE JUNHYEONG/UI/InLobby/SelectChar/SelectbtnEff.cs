using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SelectbtnEff : MonoBehaviour
{
    private Animator buttonAni;
    [SerializeField]private GameObject SelectChar;
    public string SelectCharName;

    private void Awake()
    {
        buttonAni = GetComponentInChildren<Animator>();
        SelectCharName = SelectChar.name;
    }

    public void OnClickButton()
    {
        StartAni();
    }

    private void StartAni()
    {
        SelectChar.SetActive(true);
        buttonAni.SetTrigger("Selected");
    }

    public void turnOff()
    {
        SelectChar.SetActive(false);
        buttonAni.SetTrigger("Normal");
    }
}
