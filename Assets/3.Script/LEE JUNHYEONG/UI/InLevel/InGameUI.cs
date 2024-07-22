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
    CORIN = 0,
    ELEVEN = 1,
    ANBY =2
};

public struct CharInfo // ������
{
    public TempChar name;
    public float curHP;
    public float maxHP;
    public float curSP;
    public float maxSP;
};

public class InGameUI : MonoBehaviour
{
    [SerializeField] private SelectedChar selectedChar; // ���õ� ĳ���� ����
    [SerializeField] private UnCharHpSp[] unChar; // ��� ĳ���� 2��
    [SerializeField] private Image[] unCharIMG; // ��� ĳ���� �̹���
    [SerializeField] private Image[] changeEffectIMGs; // �ٲٴ� ȿ�� �̹���
    private Tween[] fadeTween; // ȿ�� �ִ� ����
    [SerializeField] private CharIMGData_CS CharIMGData;
    [SerializeField] private TextMeshProUGUI UltStat; // �ñر� ��ġ�Դϴ�.

    CharInfo[] debugchars = new CharInfo[3]; // ������ ĳ���� ����

    private void Start() // ������
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

    private void DeBugshufflechar() // ������ : ĳ���� ������ ���� �̸��� �ٲ��ִ� �޼ҵ��Դϴ�.
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

        if (Input.GetKeyDown(KeyCode.Space)) // �����
        {
            DeBugshufflechar();
            ChangeChar(debugchars);
            Change_Effect();
            Pressbtn(KeyCode.Space);
        }
        
        if(Input.GetMouseButtonDown(1))// �����
        {
            Pressbtn(KeyCode.Mouse1);
        }// �������Դϴ�.
        
        if (Input.GetKeyDown(KeyCode.E)) // �����
        {
            RenewalSkillbtn(KeyCode.E);
        }
        
        if (Input.GetKeyDown(KeyCode.Escape)) // �����
        {
            UIManager.instance.OpenAndClosePause();
        }
        
        if(Input.GetKeyDown(KeyCode.Q))// �����
        {
            RenewalSkillbtn(KeyCode.Q);
        }
        // 
        //************************************************************************
    }

    public void ChangeChar(CharInfo[] tempChars) // ĳ���͸� �ٲٴ� �޼ҵ�
    {
        /*
         * ���� ĳ������ �����͸� ���� �ͼ�
         */

        //����� ���Դϴ�.
        Change_Profile(selectedChar.Profile, tempChars[0].name);
        selectedChar.RefreshHealth(tempChars[0], true);
        selectedChar.RefreshSp(tempChars[0]);

        for (int i = 0; i < unChar.Length; i++)
        {
            Change_Profile(unCharIMG[i], tempChars[i+1].name);
            unChar[i].Refresh_Hpbar(tempChars[i+1]);
            unChar[i].Refresh_Spbar(tempChars[i+1]);
        }
        //������
    }

    private void Change_Profile(Image profile, TempChar charInfo) // �������Դϴ�.
    {
        profile.sprite = CharIMGData.sprites[(int)charInfo];
    }

    private void Change_Effect() // �ٲ� �� ����� UIȿ���� ��Ʈ������ �����߽��ϴ�.
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
    // ��ų, ���� Ű �Է½� Ȥ�� ��ġ ��ȯ�� ��Ÿ�� ȿ�� �޼ҵ���Դϴ�.

    [SerializeField] private Image mouseRightCool;
    private bool isRightClicked = false;
    [SerializeField] private TextMeshProUGUI mouseRightText;

    [SerializeField] private Animator EbtnAni;
    [SerializeField] private Animator QbtnAni;
    [SerializeField] private Animator SpacebtnAni;

    public void Pressbtn(KeyCode keyCode) // ��ų �ߵ��� �ش� �޼ҵ带 ȣ���ϸ�˴ϴ�.
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

    public void RenewalSkillbtn(KeyCode keyCode)// ��ų�� �� �� effect�� �ش� �޼ҵ��Դϴ�.
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

    [SerializeField]private float dashCoolTime; // �뽬 ��Ÿ�� �����Դϴ�.

    private IEnumerator RenewalMouseRight_co() // ��Ÿ�� ���� �޼ҵ�
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
        mouseRightText.text = $"{mouseRightCool.fillAmount* dashCoolTime:F2}";
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

    public void RenewalUltStat(float curUltNum) // �ñر� ī��ƮUI�� �����ϴ� �޼ҵ��Դϴ�.
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
    
    //**************************************************************************************************************************
}
