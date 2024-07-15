using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

/*
 * <선택되지 않은 캐릭터용>
 Hp Sp 슬라이더를 컨트롤합니다
    Hp Sp 슬라이더는 한 번에 값을 반영하여 변경됩니다.
 */

public class UnCharHpSp : MonoBehaviour
{
    private Slider hpBar;
    private Slider spBar;
    private int Amount = 2;

    private void Start()
    {

        Slider[] sliders = new Slider[2];
            
        sliders = GetComponentsInChildren<Slider>();
        
        for(int i=0; i < Amount;i++)
        {
            switch(sliders[i].name)
            {
                case "Hp":
                    hpBar = sliders[i];
                    break;

                case "Sp":
                    spBar = sliders[i];
                    break;
            }
        }
    }

    public void Refresh_Hpbar(float nowHealth, float maxHealth)
    {
        hpBar.value = nowHealth / maxHealth;
    }

    public void Refresh_Spbar(float nowSp, float maxSp)
    {
        spBar.value = nowSp / maxSp;
    }
}
