using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedChar : MonoBehaviour
{
    [SerializeField] private Slider realHp;
    [SerializeField] private Slider fakeHp;
    [SerializeField] private Slider sp;
    [SerializeField] private Image profile;
    [SerializeField] private TextMeshProUGUI playerhptext;

    public Image Profile
    {
        get
        {
            return profile;
        }

        set
        {
            profile = value;
        }
    }

    private Coroutine timerCoroutine;

    private void Start()
    {
        Slider[] sliders = new Slider[3];
        sliders = GetComponentsInChildren<Slider>();


        for (int i = 0; i < sliders.Length; i++)
        {
            switch (sliders[i].name)
            {
                case "Fake":
                    fakeHp = sliders[i];
                    break;

                case "Real":
                    realHp = sliders[i];
                    break;

                case "Sp":
                    sp = sliders[i];
                    break;
            }
        }
    }

    public void RefreshHealth(CharInfo charInfo, bool isChangeChar) // hp ���� : �� ������ fake�� ���� ȿ�� �߻� ���θ� �Ǵ��մϴ�.
    {
        realHp.value = charInfo.curHP / charInfo.maxHP;

        if(!isChangeChar)
        Start_CountFillFakeHp();

        else
        {
            fakeHp.value = charInfo.curHP / charInfo.maxHP;
        }

        playerhptext.text = $"{(int)charInfo.curHP} / {(int)charInfo.maxHP}";
    }

    public void RefreshSp(CharInfo charInfo) // sp ����
    {
        sp.value = charInfo.curSP / charInfo.maxSP;
    }

    private void Start_CountFillFakeHp() // ���� �ڷ�ƾ�� �������̸� ���� �ڷ�ƾ ��� �� ����
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(WaitAndExecute_co());
    }
    private IEnumerator WaitAndExecute_co() // ���� ��
    {
        yield return new WaitForSeconds(1f);
        Refresh_fakeHp();
    }

    private void Refresh_fakeHp() // ������ �Ǳ��� �������ϰ� �پ��
    {
        fakeHp.DOValue(realHp.value, 1.5f, false).SetEase(Ease.OutExpo);
    }
}
