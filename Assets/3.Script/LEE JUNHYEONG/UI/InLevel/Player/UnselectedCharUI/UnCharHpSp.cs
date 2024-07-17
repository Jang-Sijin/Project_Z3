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
    public void Refresh_Hpbar() // 피 업데이트
    {
        hpBar.value = curHp / maxHp;
    }

    public void Refresh_Spbar() // Sp 업데이트
    {
        spBar.value = curSp / maxSp;
    }
}
