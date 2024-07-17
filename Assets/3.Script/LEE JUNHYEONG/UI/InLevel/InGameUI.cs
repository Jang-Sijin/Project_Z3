using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.ProBuilder.Shapes;
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
    [SerializeField] private Image unCharIMG1;
    [SerializeField] private UnCharHpSp unChar2;
    [SerializeField] private Image unCharIMG2;
    [SerializeField] private Image[] changeEffectIMGs;
    [SerializeField] private CharIMGData_CS CharIMGData;


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

        for (int i = 0; i < chars.Length; i++)
        {
            chars[i].curHP = Random.Range(0.5f, 1f);
            chars[i].maxHP = 1f;
            chars[i].curSP = Random.Range(0.5f, 1f);
            chars[i].maxSP = 1f;
        }
    }

    private void Update()
    {
        OpenAndClosePause();

        if (Input.GetKeyDown(KeyCode.Space)) // 디버깅용입니다.
        {
            ChangeChar((TempChar)Random.Range(0, 3), (TempChar)Random.Range(0, 3), (TempChar)Random.Range(0, 3));
            Change_Effect();
        }

        if(Input.GetMouseButtonDown(1))
        {
            RenewalMouseRight();
        }// 디버깅용입니다.

        if (Input.GetKeyDown(KeyCode.E)) // 디버깅용
        {
            PressE();
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

        //디버깅 용입니다.
        Change_Profile(selectedChar.Profile, newselChar);
        selectedChar.CurHP = chars[(int)newselChar].curHP;
        selectedChar.MaxHP = chars[(int)newselChar].maxHP;
        selectedChar.CurSP = chars[(int)newselChar].curSP;
        selectedChar.MaxSP = chars[(int)newselChar].maxSP;
        selectedChar.RefreshHealth(true);

        Change_Profile(unCharIMG1, newunChar1);
        unChar1.CurHp = chars[(int)newunChar1].curHP;
        unChar1.MaxHp = chars[(int)newunChar1].maxHP;
        unChar1.CurSp = chars[(int)newunChar1].curSP;
        unChar1.MaxSp = chars[(int)newunChar1].maxSP;
        unChar1.Refresh_Hpbar();
        unChar1.Refresh_Spbar();

        Change_Profile(unCharIMG2, newunChar2);
        unChar2.CurHp = chars[(int)newunChar2].curHP;
        unChar2.MaxHp = chars[(int)newunChar2].maxHP;
        unChar2.CurSp = chars[(int)newunChar2].curSP;
        unChar2.MaxSp = chars[(int)newunChar2].maxSP;
        unChar2.Refresh_Hpbar();
        unChar2.Refresh_Spbar();
        //디버깅용
    }

    private void Change_Profile(Image profile, TempChar charInfo)
    {
        Debug.Log(CharIMGData.sprites[(int)charInfo]);
        profile.sprite = CharIMGData.sprites[(int)charInfo];
    }

    private void Change_Effect()
    {
        Color tempColor;
        for (int i = 0; i < changeEffectIMGs.Length; i++)
        {
            tempColor = changeEffectIMGs[i].color;
            tempColor.a = 1f;
            changeEffectIMGs[i].color = tempColor;

            changeEffectIMGs[i].DOFade(0f, 0.2f).SetEase(Ease.InOutQuad);
        }
    }

    //**************************************************************************************************************************
    // 스킬, 공격 키 입력시 혹은 수치 변환시 나타날 효과 메소드들입니다.

    [SerializeField] private Image mouseRightCool;
    private bool isRightClicked = false;
    [SerializeField] private TextMeshProUGUI mouseRightText;

    [SerializeField] private Image chargedE;
    [SerializeField] private Animator Eeffect;
    [SerializeField] private Image Q;

    public void RenewalMouseRight()
    {
        if (!isRightClicked)
        {
            StartCoroutine(RenewalMouseRight_co());    
        }
    }

    private Tween mouseRightcooltween;

    private float coolTime = 2f;
    private IEnumerator RenewalMouseRight_co()
    {
        isRightClicked = true;

        mouseRightCool.gameObject.SetActive(true);
        mouseRightText.gameObject.SetActive(true);

        mouseRightcooltween = mouseRightCool.DOFillAmount(0, coolTime).SetEase(Ease.Linear).OnUpdate(() => UpdateText());
       
        yield return mouseRightcooltween.WaitForCompletion();
        mouseRightText.gameObject.SetActive(false);
        mouseRightCool.gameObject.SetActive(false);
        isRightClicked = false;
        mouseRightCool.fillAmount = 1f;
    }

    private void UpdateText()
    {
        mouseRightText.text = $"{mouseRightCool.fillAmount*2f:F2}";
    }

    public void RenewalE(bool isCharged)
    {
        if (isCharged)
        {
            chargedE.gameObject.SetActive(true);
        }

        else
        {
            chargedE.gameObject.SetActive(false);
        }
    }

    public void PressE() // E키 사용 메소드
    {
        if(!Eeffect.gameObject.activeSelf)
        {
            StartCoroutine(PressE_co());
        }
    }

    private IEnumerator PressE_co()
    {
        chargedE.gameObject.SetActive(true);
        Eeffect.gameObject.SetActive(true);

        WaitForSeconds wfs = new WaitForSeconds(Eeffect.GetCurrentAnimatorStateInfo(0).length);

        yield return wfs;

        Eeffect.gameObject.SetActive(false);
        chargedE.gameObject.SetActive(false);
    }

    public void RenewalQ(bool isCharged)
    {
        if (isCharged)
        {
            chargedE.gameObject.SetActive(true);
        }

        else
        {
            chargedE.gameObject.SetActive(false);
        }
    }
    //**************************************************************************************************************************
}
