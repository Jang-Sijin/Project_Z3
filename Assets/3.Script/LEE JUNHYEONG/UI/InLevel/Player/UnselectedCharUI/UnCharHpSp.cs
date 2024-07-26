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
    private Slider spPointer;

    

    private void Start()
    {

        Slider[] sliders = new Slider[3];
            
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

                case "SpPointer":
                    spPointer = sliders[i];
                    break;
            }
        }
    }
    public void InitSpPointer(float minimumForSkill, CharInfo charInfo)
    {
        spPointer.value = minimumForSkill / charInfo.maxSP;
    }

    public void Refresh_Hpbar(CharInfo charInfo) // 피 업데이트
    {
        hpBar.value = charInfo.curHP / charInfo.maxHP;
    }

    public void Refresh_Spbar(CharInfo charInfo) // Sp 업데이트
    {
        spBar.value = charInfo.curSP / charInfo.maxSP;
    }
}
