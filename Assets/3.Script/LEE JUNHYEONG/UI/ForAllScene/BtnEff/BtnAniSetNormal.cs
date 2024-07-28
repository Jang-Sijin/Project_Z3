using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BtnAniSetNormal : MonoBehaviour
{
    private Animator BTNAni;

    private void Awake()
    {
        BTNAni = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        BTNAni.SetTrigger("Normal");
    }

    private void OnDisable()
    {
        BTNAni.SetTrigger("Normal");
    }

    
}
