using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;


public class Build_IngameUI : MonoBehaviour
{

    [SerializeField] private SelectedChar selectedChar; // 선택된 캐릭터 상태
    [SerializeField] private UnCharHpSp[] unChar; // 대기 캐릭터 2개
    [SerializeField] private Image[] unCharIMG; // 대기 캐릭터 이미지
    [SerializeField] private Image[] changeEffectIMGs; // 바꾸는 효과 이미지
    private Tween[] fadeTween; // 효과 애니 변수
    [SerializeField] private CharIMGData_CS CharIMGData;
    [SerializeField] private TextMeshProUGUI UltStat; // 궁극기 수치입니다.

    //CharInfo[] debugchars = new CharInfo[3]; // 디버깅용 캐릭터 정보

    /*
    private void Start() // 디버깅용
    {
        for (int i = 0; i < debugchars.Length; i++)
        {
            debugchars[i].name = (TempChar)i;
            debugchars[i].curHP = Random.Range(600f, 1300f);
            debugchars[i].maxHP = PlayerController.INSTANCE.playerModel.playerStatus.MaxHealth;
            debugchars[i].curSP = Random.Range(0.5f, 1f);
            debugchars[i].maxSP = PlayerController.INSTANCE.playerModel.playerStatus.MaxSkillPoint;
        }

        fadeTween = new Tween[changeEffectIMGs.Length];
    }


    private void DeBugshufflechar() // 디버깅용 : 캐릭터 변경을 위해 이름을 바꿔주는 메소드입니다.
    {
        CharInfo temp = debugchars[0];

        for (int i = 0; i < debugchars.Length - 1; i++)
        {
            debugchars[i] = debugchars[i + 1];
        }

        debugchars[debugchars.Length - 1] = temp;
    }
    */

    /// <summary>
    /// 스테이지 처음 시작할때 UI 세팅
    /// </summary>
    public void SetIngameUI()
    {
        //첫번째 플레이어 
        selectedChar
    }

    public void ChangeChar(CharInfo[] tempChars) // 캐릭터를 바꾸는 메소드
    {
        /*
         * 현재 캐릭터의 데이터를 갖고 와서
         */

        //디버깅 용입니다.
        //Change_Profile(selectedChar.Profile, tempChars[0].name);
        //selectedChar.RefreshHealth(tempChars[0], true);
        //selectedChar.RefreshSp(tempChars[0]);
        //
        //for (int i = 0; i < unChar.Length; i++)
        //{
        //    Change_Profile(unCharIMG[i], tempChars[i + 1].name);
        //    unChar[i].Refresh_Hpbar(tempChars[i + 1]);
        //    unChar[i].Refresh_Spbar(tempChars[i + 1]);
        //}
        //디버깅용



    }
    /*
    private void Change_Profile(Image profile, TempChar charInfo) // 디버깅용입니다.
    {
        profile.sprite = CharIMGData.sprites[(int)charInfo];
    }
    */

    private void Change_Effect() // 바꿀 때 생기는 UI효과를 두트윈으로 구현했습니다.
    {
        Color tempColor;
        for (int i = 0; i < changeEffectIMGs.Length; i++)
        {
            tempColor = changeEffectIMGs[i].color;
            tempColor.a = 1f;
            changeEffectIMGs[i].color = tempColor;

            if (fadeTween[i] != null && fadeTween[i].IsActive())
            {
                fadeTween[i].Kill();
            }

            fadeTween[i] = changeEffectIMGs[i].DOFade(0f, 0.2f).SetEase(Ease.InOutQuad).SetAutoKill(true);
        }
    }

    //**************************************************************************************************************************
    // 스킬, 공격 키 입력시 혹은 수치 변환시 나타날 효과 메소드들입니다.

    [SerializeField] private Image mouseRightCool;
    private bool isRightClicked = false;
    [SerializeField] private TextMeshProUGUI mouseRightText;

    [SerializeField] private Animator EbtnAni;
    [SerializeField] private Animator QbtnAni;
    [SerializeField] private Animator SpacebtnAni;

    public void Pressbtn(KeyCode keyCode) // 스킬 발동시 해당 메소드를 호출하면됩니다.
    {
        switch (keyCode)
        {
            case KeyCode.Q:
                StartCoroutine(PressQ_co());
                break;

            case KeyCode.E:
                StartCoroutine(PressE_co());
                break;

            case KeyCode.Space:
                SpacebtnAni.SetTrigger("PressSpace");
                break;
            case KeyCode.Mouse1:
                PressMouseRight();
                break;
        }
    }

    public void RenewalSkillbtn(KeyCode keyCode)// 스킬이 다 찬 effect는 해당 메소드입니다.
    {
        switch (keyCode)
        {
            case (KeyCode.Q):
                QbtnAni.gameObject.SetActive(true);
                break;

            case (KeyCode.E):
                EbtnAni.gameObject.SetActive(true);
                break;
        }
    }

    private void PressMouseRight()
    {
        if (!isRightClicked)
        {
            StartCoroutine(RenewalMouseRight_co());
        }
    }

    [SerializeField] private float dashCoolTime; // 대쉬 쿨타임 변수입니다.

    private IEnumerator RenewalMouseRight_co() // 쿨타임 세는 메소드
    {
        isRightClicked = true;

        mouseRightCool.gameObject.SetActive(true);
        mouseRightText.gameObject.SetActive(true);

        Tween mouseRightcooltween = mouseRightCool.DOFillAmount(0, dashCoolTime).SetEase(Ease.Linear).OnUpdate(() => UpdateText());

        yield return mouseRightcooltween.WaitForCompletion();
        mouseRightText.gameObject.SetActive(false);
        mouseRightCool.gameObject.SetActive(false);
        isRightClicked = false;
        mouseRightCool.fillAmount = 1f;
    }

    private void UpdateText()
    {
        mouseRightText.text = $"{mouseRightCool.fillAmount * dashCoolTime:F2}";
    }

    private IEnumerator PressE_co()
    {
        EbtnAni.gameObject.SetActive(true);
        EbtnAni.SetTrigger("PressE");

        yield return new WaitForSeconds(EbtnAni.GetCurrentAnimatorStateInfo(0).length);

        EbtnAni.gameObject.SetActive(false);
    }

    private IEnumerator PressQ_co()
    {
        QbtnAni.gameObject.SetActive(true);
        QbtnAni.SetTrigger("PressQ");

        yield return new WaitForSeconds(QbtnAni.GetCurrentAnimatorStateInfo(0).length);

        QbtnAni.gameObject.SetActive(false);
    }
    //**************************************************************************************************************************

    public void RenewalUltStat(float curUltNum) // 궁극기 카운트UI를 갱신하는 메소드입니다.
    {
        if (curUltNum < 10)
        {
            UltStat.text = "000" + (int)curUltNum;
        }

        else if (curUltNum < 100)
        {
            UltStat.text = "00" + (int)curUltNum;
        }

        else
        {
            UltStat.text = ((int)curUltNum).ToString();
        }
    }
}