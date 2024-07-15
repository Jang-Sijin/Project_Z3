using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
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
    [SerializeField] private UnCharHpSp unChar2;
    [SerializeField] private Image[] changeEffectIMGs;
    [SerializeField] private Animator[] changeEffectAni;

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

    public void ChangeChar(TempChar newselChar, TempChar newunChar1, TempChar newunChar2) // ĳ���͸� �ٲٴ� �޼ҵ�
    {
        /*
         * ���� ĳ������ �����͸� ���� �ͼ�
         */
        Debug.Log(newunChar1.ToString());
        Debug.Log(newunChar2.ToString());

        //����� ���Դϴ�.
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
        //������
    }

    private void Change_Profile(Image profile, string charName)
    {
        Debug.Log(charName);
        profile = Resources.Load<Image>(charName); // �������Դϴ�.

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
