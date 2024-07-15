using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectedChar : MonoBehaviour
{
    [SerializeField] private Slider realHp;
    [SerializeField] private Slider fakeHp;
    [SerializeField] private Slider sp;

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

    public void RefreshHealth(float nowHealth, float maxHealth) // hp 갱신
    {
        realHp.value = nowHealth / maxHealth;
        Start_CountFillFakeHp();
    }

    public void RefreshSp(float nowSp, float maxSp) // sp 갱신
    {
        sp.value = nowSp / maxSp;
    }

    private void Start_CountFillFakeHp() // 세는 코루틴이 실행중이면 이전 코루틴 취소 후 세기
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
        }
        timerCoroutine = StartCoroutine(WaitAndExecute_co());
    }
    private IEnumerator WaitAndExecute_co() // 세는 거
    {
        yield return new WaitForSeconds(1f);
        Refresh_fakeHp();
    }

    private void Refresh_fakeHp() // 지정된 피까지 스무스하게 줄어듦
    {
        fakeHp.DOValue(realHp.value, 1.5f, false).SetEase(Ease.OutExpo);
    }
}
