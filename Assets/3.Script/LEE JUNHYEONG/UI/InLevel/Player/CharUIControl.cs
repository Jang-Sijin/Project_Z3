using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*
 * <���õ��� ���� ĳ���Ϳ�>
 Hp �����̴��� ��Ʈ���մϴ�
    Hp �����̴��� �� ���� ���� �ݿ��Ͽ� ����˴ϴ�.
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

    // ĳ������ ���� enum���� �ٲ۴ٸ� ���⼭ UI�� �Ҵ����ش�.
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
