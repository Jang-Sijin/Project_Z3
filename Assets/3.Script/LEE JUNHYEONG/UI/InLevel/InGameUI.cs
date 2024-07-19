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
    CORIN = 0,
    ELEVEN = 1,
    ANBY =2
};

public struct CharInfo // 디버깅용
{
    public TempChar name;
    public float curHP;
    public float maxHP;
    public float curSP;
    public float maxSP;
};

public class InGameUI : MonoBehaviour
{
    [SerializeField] private SelectedChar selectedChar; // 선택된 캐릭터 상태
    [SerializeField] private UnCharHpSp[] unChar; // 대기 캐릭터 2개
    [SerializeField] private Image[] unCharIMG; // 대기 캐릭터 이미지
    [SerializeField] private Image[] changeEffectIMGs; // 바꾸는 효과 이미지
    private Tween[] fadeTween; // 효과 애니 변수
    [SerializeField] private CharIMGData_CS CharIMGData;

    CharInfo[] debugchars = new CharInfo[3]; // 디버깅용 캐릭터 정보

    private void Start() // 디버깅용
    {
        for (int i = 0; i < debugchars.Length; i++)
        {
            debugchars[i].name = (TempChar)i;
            debugchars[i].curHP = Random.Range(600f, 1300f);
            debugchars[i].maxHP = 1300f;
            debugchars[i].curSP = Random.Range(0.5f, 1f);
            debugchars[i].maxSP = 1f;
        }

        fadeTween = new Tween[changeEffectIMGs.Length];
        UIManager.instance.Creat_UI(WhichUI.pauseMenuUI);
    }

    private void DeBugshufflechar() // 디버깅용 : 캐릭터 변경을 위해 이름을 바꿔주는 메소드입니다.
    {
        CharInfo temp = debugchars[0];

        for (int i = 0; i < debugchars.Length - 1; i++)
        {
            debugchars[i] = debugchars[i+1];
        }

        debugchars[debugchars.Length - 1] = temp;
    }

    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space)) // 디버깅용입니다.
        {
            DeBugshufflechar();
            ChangeChar(debugchars);
            Change_Effect();
            PressSpace();
        }
        
        if(Input.GetMouseButtonDown(1))// 디버깅용입니다.
        {
            RenewalMouseRight();
        }// 디버깅용입니다.
        
        if (Input.GetKeyDown(KeyCode.E)) // 디버깅용
        {
            PressE();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UIManager.instance.OpenAndClosePause();
        }
        
        if(Input.GetKeyDown(KeyCode.Q))
        {
            RenewalQ(true);
        }
        // 
        //************************************************************************
    }

    public void ChangeChar(CharInfo[] tempChars) // 캐릭터를 바꾸는 메소드
    {
        /*
         * 현재 캐릭터의 데이터를 갖고 와서
         */

        //디버깅 용입니다.
        Change_Profile(selectedChar.Profile, tempChars[0].name);
        selectedChar.RefreshHealth(tempChars[0], true);
        selectedChar.RefreshSp(tempChars[0]);

        for (int i = 0; i < unChar.Length; i++)
        {
            Change_Profile(unCharIMG[i], tempChars[i+1].name);
            unChar[i].Refresh_Hpbar(tempChars[i+1]);
            unChar[i].Refresh_Spbar(tempChars[i+1]);
        }
        //디버깅용
    }

    private void Change_Profile(Image profile, TempChar charInfo) // 디버깅용입니다.
    {
        profile.sprite = CharIMGData.sprites[(int)charInfo];
    }

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

    public void RenewalMouseRight() // 우클릭시 부르면 됩니다.
    {
        if (!isRightClicked)
        {
            StartCoroutine(RenewalMouseRight_co());    
        }
    }

    private Tween mouseRightcooltween;
    [SerializeField]private float dashCoolTime;

    private IEnumerator RenewalMouseRight_co() // 쿨타임 세는 메소드
    {
        isRightClicked = true;

        mouseRightCool.gameObject.SetActive(true);
        mouseRightText.gameObject.SetActive(true);

        mouseRightcooltween = mouseRightCool.DOFillAmount(0, dashCoolTime).SetEase(Ease.Linear).OnUpdate(() => UpdateText());
       
        yield return mouseRightcooltween.WaitForCompletion();
        mouseRightText.gameObject.SetActive(false);
        mouseRightCool.gameObject.SetActive(false);
        isRightClicked = false;
        mouseRightCool.fillAmount = 1f;
    }

    private void UpdateText()
    {
        mouseRightText.text = $"{mouseRightCool.fillAmount* dashCoolTime:F2}";
    }

    public void RenewalE(bool canUse) // 스킬 사용가능할 때 표시될 UI입니다.
    {
        if (canUse)
        {
            EbtnAni.gameObject.SetActive(true);
            EbtnAni.SetTrigger("ChargedE");
        }

        else
        {
            EbtnAni.gameObject.SetActive(false);
        }
    }

    public void PressE() // E키 사용 메소드
    {
        if(!EbtnAni.gameObject.activeSelf)
        {
            StartCoroutine(PressE_co());
        }
    }

    private IEnumerator PressE_co()
    {
        EbtnAni.gameObject.SetActive(true);
        EbtnAni.SetTrigger("ChargedE");

        WaitForSeconds wfs = new WaitForSeconds(EbtnAni.GetCurrentAnimatorStateInfo(0).length);

        yield return wfs;
        EbtnAni.gameObject.SetActive(false);
    }

    public void RenewalQ(bool canUse)
    {
        if (canUse)
        {
            QbtnAni.gameObject.SetActive(true);
            QbtnAni.SetTrigger("UltCharged");
        }

        else
        {
            QbtnAni.gameObject.SetActive(false);
        }
    }

    public void PressSpace()
    {
        SpacebtnAni.SetTrigger("PressSpace");
    }
    //**************************************************************************************************************************
}
