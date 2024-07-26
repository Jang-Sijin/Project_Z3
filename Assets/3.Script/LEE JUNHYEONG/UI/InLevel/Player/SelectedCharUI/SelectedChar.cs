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
    [SerializeField] private Slider spPointer;
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
        Slider[] sliders = new Slider[4];
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
                case "SpPointer":
                    spPointer = sliders[i];
                    break;
            }
        }
    }
    /// <summary>
    /// ĳ���� �Ҵ�
    /// </summary>
    /// <param name="playerModel"></param>
    public void AssignCharacter(PlayerModel playerModel, Sprite portraitImg) 
    {
        realHp.value = playerModel.playerStatus.CurrentHealth / playerModel.playerStatus.MaxHealth;
        fakeHp.value = playerModel.playerStatus.CurrentHealth / playerModel.playerStatus.MaxHealth;
        sp.value = playerModel.playerStatus.CurrentSkillPoint / playerModel.playerStatus.MaxSkillPoint;
        playerhptext.text = $"{(int)playerModel.playerStatus.CurrentHealth} / {(int)playerModel.playerStatus.MaxHealth}";

        profile.sprite = portraitImg;
        spPointer.value = 0.5f;
    }



    public void RefreshHealth(PlayerModel playerModel, bool isChangeChar) // hp ���� : �� ������ fake�� ���� ȿ�� �߻� ���θ� �Ǵ��մϴ�.
    {
        realHp.value = playerModel.playerStatus.CurrentHealth / playerModel.playerStatus.MaxHealth;

        if (!isChangeChar)
            Start_CountFillFakeHp();

        else
        {
            fakeHp.value = playerModel.playerStatus.CurrentHealth / playerModel.playerStatus.MaxHealth;
        }

        playerhptext.text = $"{(int)playerModel.playerStatus.CurrentHealth} / {(int)playerModel.playerStatus.MaxHealth}";
    }

    public void RefreshSp(PlayerModel playerModel) // sp ����
    {
        sp.value = playerModel.playerStatus.CurrentSkillPoint / playerModel.playerStatus.MaxSkillPoint;
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
