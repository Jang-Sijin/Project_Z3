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
public enum TempChar // ������
{ 
    ANBY = 0,
    SOUKAKU = 1,
    CORIN =2
};

public class InGameUI : MonoBehaviour
{
    /* key �Է¿� ���� ���� ȿ������ ��������
     �� ���ӿ��� �۵��ϴ� UI Ű ��� ��������
ESC -Pause
���콺 (��) - �Ϲݰ���
ȿ�� ����

   "      (��) - �뽬
	�������� �� �� �� ������ �뽬 ���� �뽬�� ��Ÿ���� �ִ�
	���� �� ���� �ȉ�

 E              - ��ų
���� ���� �Ѿ��ϴ� ����Ʈ�� ���� - ���� ���ϸ��̼� ����

Space 	  -  ����ġ
����ġ�� ������ ���ʿ��� ������ ������ �ٲ��
������ �÷��̾ �� ������ �ٲ� �� �ִ�.
     */

    /*
     * ĳ������ ���� ��ġ�� �ݿ��Ϸ��� �Ʒ� �������� �ҷ��� ����ϸ� �˴ϴ�.
     */
    [SerializeField] private SelectedChar selectedChar;
    [SerializeField] private UnCharHpSp unChar1;
    [SerializeField] private Image unCharIMG1;
    [SerializeField] private UnCharHpSp unChar2;
    [SerializeField] private Image unCharIMG2;
    [SerializeField] private Image[] changeEffectIMGs;
    [SerializeField] private CharIMGData_CS CharIMGData;


    private struct CharInfo // ������
    {
        public TempChar name;
        public float curHP;
        public float maxHP;
        public float curSP;
        public float maxSP;
    };
    CharInfo[] chars = new CharInfo[3]; // ������ ĳ���� ����

    private void Start() // ������
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

        if (Input.GetKeyDown(KeyCode.Space)) // �������Դϴ�.
        {
            ChangeChar((TempChar)Random.Range(0, 3), (TempChar)Random.Range(0, 3), (TempChar)Random.Range(0, 3));
            Change_Effect();
        }

        if(Input.GetMouseButtonDown(1))
        {
            RenewalMouseRight();
        }// �������Դϴ�.

        if (Input.GetKeyDown(KeyCode.E)) // ������
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

    public void ChangeChar(TempChar newselChar, TempChar newunChar1, TempChar newunChar2) // ĳ���͸� �ٲٴ� �޼ҵ�
    {
        /*
         * ���� ĳ������ �����͸� ���� �ͼ�
         */

        //����� ���Դϴ�.
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
        //������
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
    // ��ų, ���� Ű �Է½� Ȥ�� ��ġ ��ȯ�� ��Ÿ�� ȿ�� �޼ҵ���Դϴ�.

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

    public void PressE() // EŰ ��� �޼ҵ�
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
