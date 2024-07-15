using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*<컴포넌트 설명>
 RefreshHealth(float nowHealth, float maxHealth)를 불러 hp 갱신
RefreshStun(float nowStun, float maxStun)를 불러 stun 갱신

FixedUpdate에서 계속 UI가 플레이어 카메라를 계속 바라봅니다.
*/

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private Slider realHp;
    [SerializeField] private Slider fakeHp;
    [SerializeField] private Slider stun;
    [SerializeField] private Collider monster;
    [SerializeField] private Text stunText;
    private Coroutine timerCoroutine;
    private Camera mainCamera; // 시점에 따라 UI가 따라오기 위해 카메라를 받아옵니다.

    //***************************************************************************
    //private float curHp;
    //private float maxhp;
    //private float curStun;
    //private float maxStun;
    //***************************************************************************
    // DB 구성완료하면 받을 변수

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

                case "Stun":
                    stun = sliders[i];
                    break;
            }
        }
        monster = GetComponentInParent<Collider>();
        mainCamera = Camera.main;

        transform.localPosition = new Vector3(monster.bounds.size.x * (0.5f) + 2f, monster.bounds.size.y * (0.5f), 0);
    }

    private void FixedUpdate()
    {
        transform.forward = mainCamera.transform.forward;
    }

    public void RefreshHealth(float nowHealth, float maxHealth) // hp 갱신
    {
        realHp.value = nowHealth / maxHealth;
        Start_CountFillFakeHp();
    }

    public void RefreshStun(float nowStun, float maxStun) // stun 갱신
    {
        stun.value = nowStun / maxStun;
        stunText.text = $"{stun.value * 100}";
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
