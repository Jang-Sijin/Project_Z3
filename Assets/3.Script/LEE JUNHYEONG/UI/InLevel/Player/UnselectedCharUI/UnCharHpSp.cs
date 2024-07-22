using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

/*
 * <���õ��� ���� ĳ���Ϳ�>
 Hp Sp �����̴��� ��Ʈ���մϴ�
    Hp Sp �����̴��� �� ���� ���� �ݿ��Ͽ� ����˴ϴ�.
 */

public class UnCharHpSp : MonoBehaviour
{
    private Slider hpBar;
    private Slider spBar;

    private void Start()
    {

        Slider[] sliders = new Slider[2];
            
        sliders = GetComponentsInChildren<Slider>();
        
        for(int i=0; i < 2;i++)
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
    public void Refresh_Hpbar(CharInfo charInfo) // �� ������Ʈ
    {
        hpBar.value = charInfo.curHP / charInfo.maxHP;
    }

    public void Refresh_Spbar(CharInfo charInfo) // Sp ������Ʈ
    {
        spBar.value = charInfo.curSP / charInfo.maxSP;
    }
}
