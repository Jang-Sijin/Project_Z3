using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum TempChar
{ 
    ANBY = 0,
    SOUKAKU = 1,
    CORIN =2
};

public class InGameUI : MonoBehaviour
{
    /* key 입력에 따른 여러 효과들을 적용하자
     인 게임에서 작동하는 UI 키 모두 구현하자
ESC -Pause
마우스 (좌) - 일반공격
효과 없음

   "      (우) - 대쉬
	돌기전에 한 번 더 누르면 대쉬 다음 대쉬는 쿨타임이 있다
	연속 두 번이 안됌

 E              - 스킬
뭔가 존나 뿌앙하는 이펙트가 있음 - 추측 에니메이션 같음

Space 	  -  스위치
스위치의 순서는 왼쪽에서 오른쪽 순으로 바뀐다
하지만 플레이어가 그 순서를 바꿀 수 있다.
     */

    /*
     * 캐릭터의 현재 수치를 반영하려면 아래 변수들을 불러서 사용하면 됩니다.
     */
    [SerializeField] private SelectedChar selectedChar;
    [SerializeField] private UnCharHpSp unCharHpSp1;
    [SerializeField] private UnCharHpSp unCharHpSp2;

    private void Update()
    {
        OpenAndClosePause();
    }

    private void OpenAndClosePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !UIManager.instance.isCloseOrOpen)
        {
            if (UIManager.instance.isPause)
            {
                UIManager.instance.pauseMenuUI.OnClickClose();
            }

            else
            {
                UIManager.instance.pauseMenuUI.gameObject.SetActive(true);
                StartCoroutine(UIManager.instance.pauseMenuUI.CallPauseMenu_co());
            }
        }
    }

    public void ChangeChar(TempChar selectedChar, TempChar unselectedChar1, TempChar unselectedChar2)
    {
        /*
         * 현재 캐릭터의 데이터를 갖고 와서
         */


    }
}
