using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public enum TempChar // 디버깅용
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
    [SerializeField] private UnCharHpSp unChar1;
    [SerializeField] private UnCharHpSp unChar2;
    [SerializeField] private Image[] changeEffectIMGs;
    [SerializeField] private Animator[] changeEffectAni;

    private struct CharInfo // 디버깅용
    {
        public TempChar name;
        public float curHP;
        public float maxHP;
        public float curSP;
        public float maxSP;
    };
        CharInfo[] chars = new CharInfo[3]; // 디버깅용 캐릭터 정보

    private void Start() // 디버깅용
    {

        for(int i=0; i<chars.Length; i++)
        {
            chars[i].curHP = Random.Range(500, 1000);
            chars[i].maxHP = Random.Range(1000, 1350);
        }
    }

    private void OnEnable()
    {
        for (int i = 0; i < changeEffectAni.Length; i++)
        {
            changeEffectAni[i].SetBool("Run", true);
        }
    }


    private void Update()
    {
        OpenAndClosePause();

        if(Input.GetKeyDown(KeyCode.Space))
        {
            ChangeChar(TempChar.CORIN, TempChar.SOUKAKU, TempChar.ANBY);
            Change_Effect();
        }
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

    public void ChangeChar(TempChar newselChar, TempChar newunChar1, TempChar newunChar2) // 캐릭터를 바꾸는 메소드
    {
        /*
         * 현재 캐릭터의 데이터를 갖고 와서
         */
        Debug.Log(newunChar1.ToString());
        Debug.Log(newunChar2.ToString());

        //디버깅 용입니다.
        Change_Profile(selectedChar.Profile, newselChar.ToString());
        selectedChar.CurHP = chars[(int)newselChar].curHP;
        selectedChar.MaxHP = chars[(int)newselChar].maxHP;
        selectedChar.CurSP = chars[(int)newselChar].curSP;
        selectedChar.MaxSP = chars[(int)newselChar].maxSP;
        selectedChar.RefreshHealth(true);
        Debug.Log(newselChar.ToString());

        Change_Profile(unChar1.Profile, newunChar1.ToString());
        unChar1.CurHp = chars[(int)newunChar1].curHP;
        unChar1.MaxHp = chars[(int)newunChar1].maxHP;
        unChar1.CurSp = chars[(int)newunChar1].curSP;
        unChar1.MaxSp = chars[(int)newunChar1].maxSP;
        unChar1.Refresh_Hpbar();
        unChar1.Refresh_Spbar();

        Change_Profile(unChar2.Profile, newunChar2.ToString());
        unChar1.CurHp = chars[(int)newunChar2].curHP;
        unChar1.MaxHp = chars[(int)newunChar2].maxHP;
        unChar1.CurSp = chars[(int)newunChar2].curSP;
        unChar1.MaxSp = chars[(int)newunChar2].maxSP;
        unChar1.Refresh_Hpbar();
        unChar2.Refresh_Spbar();
        //디버깅용
    }

    private void Change_Profile(Image profile, string charName)
    {
        Debug.Log(charName);
        profile = Resources.Load<Image>(charName); // 디버깅용입니다.

        Debug.Log(Resources.Load<Image>("Resour/LEE JUNHYEONG/" + charName));
    }

    private void Change_Effect()
    {
        Color tempColor;
        for (int i = 0; i < changeEffectIMGs.Length; i++)
        {
            tempColor = changeEffectIMGs[i].color;
            tempColor.a = 1f;
            changeEffectIMGs[i].color = tempColor;

            changeEffectIMGs[i].DOFade(0f, 1f).SetEase(Ease.InOutQuad);
        }
    }
}
