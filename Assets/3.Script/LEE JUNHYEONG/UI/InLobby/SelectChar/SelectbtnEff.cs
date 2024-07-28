using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectbtnEff : MonoBehaviour
{
    private Animator buttonAni;
    public Image AniIMG; 
    [SerializeField]private GameObject SelectChar;

    public ECharacter eCharacter;

    private void Awake()
    {
        buttonAni = GetComponentInChildren<Animator>();
        AniIMG = GetComponent<Image>();

        switch (SelectChar.name)
        {
            case "Corin":
                eCharacter = ECharacter.Corin;
                break;

            case "Anbi":
                eCharacter = ECharacter.Anbi;
                break;

            case "Longinus":
                eCharacter = ECharacter.Longinus;
                break;
        }
    }

    private void OnEnable()
    {
        AniIMG.enabled = false;
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

    public void TurnOff()
    {
        buttonAni.SetTrigger("Normal");
        SelectChar?.SetActive(false);
    }
}
