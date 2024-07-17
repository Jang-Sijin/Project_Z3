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
    private int Amount = 2;
    private float curHp;
    private float maxHp;
    private float curSp;
    private float maxSp;

    public float CurHp
    {
        get
        {
            return curHp;
        }

        set
        {
            curHp = value;
        }
    }
    public float MaxHp
    {
        get
        {
            return maxHp;
        }

        set
        {
            maxHp = value;
        }
    }
    public float CurSp
    {
        get
        {
            return curSp;
        }

        set
        {
            curSp = value;
        }
    }
    public float MaxSp
    {
        get
        {
            return maxSp;
        }

        set
        {
            maxSp= value;
        }
    }
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
    public void Refresh_Hpbar() // �� ������Ʈ
    {
        hpBar.value = curHp / maxHp;
    }

    public void Refresh_Spbar() // Sp ������Ʈ
    {
        spBar.value = curSp / maxSp;
    }
}
