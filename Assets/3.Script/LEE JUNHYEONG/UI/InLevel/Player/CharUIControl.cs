using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*
 * <선택되지 않은 캐릭터용>
 Hp 슬라이더를 컨트롤합니다
    Hp 슬라이더는 한 번에 값을 반영하여 변경됩니다.
 */

public class CharUIControl : MonoBehaviour
{
    private int unCharAmount = 2;
    private UnCharHpSp[] unselectedChars;

    private SelectedChar PlayerHpSp;

    private Image[] charImages;
    private int charAmount = 3;


    private void Start()
    {
        unselectedChars = new UnCharHpSp[unCharAmount];
        charImages = new Image[charAmount];

        unselectedChars = GetComponentsInChildren<UnCharHpSp>();
        PlayerHpSp = GetComponentInChildren<SelectedChar>();
    }

    // 캐릭터의 값을 enum으로 바꾼다면 여기서 UI에 할당해준다.
    //public void Assign_Char(int[] layer)
    //{
    //    for(int i=0;i < layer.Length; i++)
    //    {
    //        switch (layer[i])
    //        {
    //            case 0:
    //                PlayerHpSp
    //            break;
    //
    //            case 1:
    //
    //              
    //            break;
    //
    //            case 2:
    //
    //            break;
    //        }
    //    }
    //}
}
