using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    [SerializeField] private Slider stun; // 스턴 수치 슬라이더

    [Header ("SP origin & Repaint Color")]
    [SerializeField] private Color originStunColor;
    [SerializeField] private Color restunColor; // 스턴이 걸렸을 경우 바꿀 색입니다.
    private Image spFillImage; 

    private float stunTime = 5f;
    public float StunTime
    {
        get
        {
            return stunTime;
        }

        set
        {
            stunTime = value;
        }
    }

    [SerializeField] private Collider monster;
    private Coroutine timerCoroutine; // fakehp의 다는 시간을 재기 위한 변수
    private RectTransform rect; // 캔버스의 크기 조절용
    private float originScaleX = 1.5f;
    private float originScaleY = 1.5f;
    private Vector3 originScale;
    [SerializeField] private Camera mainCamera; // 시점에 따라 UI가 따라오기 위한 카메라

    private float Distance; // 거리에 따른 UI의 크기 변화를 나타냅니다.

    private void Start()
    {
        monster = GetComponentInParent<Collider>();
        rect = GetComponentInParent<RectTransform>();
        spFillImage = stun.fillRect.GetComponent<Image>();
        originScale = new Vector3(originScaleX, originScaleY);

        transform.localPosition = new Vector3(monster.bounds.size.x + (monster.bounds.size.x)*(0.5f), 
                                              monster.bounds.size.y * (0.5f) + (monster.bounds.size.y*0.25f),
                                              0);
    }
    private void FixedUpdate()
    {
        PosByCamera();
        SizeByDistance();
    }

    private void LateUpdate()
    {
        LookPlayer();
    }
    private void LookPlayer()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }

    private void PosByCamera()
    {
        transform.localPosition = new Vector3(monster.bounds.size.x + (monster.bounds.size.x) * (0.5f) * Distance * (0.2f),
                                              monster.bounds.size.y * (0.5f) + (monster.bounds.size.y * 0.25f) * Distance * (0.2f),
                                              0);
    }

    private void SizeByDistance() // 거리에 따른 UI의 크기 변화입니다.
    {
        Distance = Vector3.Distance(transform.position, mainCamera.transform.position);

        if (Distance > 10)
        {
            rect.localScale = new Vector3(Distance * 0.1f * originScaleX, Distance * 0.1f * originScaleY);
        }
    }
    public void RefreshHealth(float nowHealth, float maxHealth) // hp 갱신
    {
        realHp.value = nowHealth / maxHealth;
        Start_CountFillFakeHp();
    }

    public void RefreshStun(float nowStun, float maxStun) // stun 갱신
    { 
        if (isStunReducing)
            return;

        stun.value = nowStun / maxStun;

        if (stun.value >= 1f)
        {
            ReduceStun();
        }
    }

    public bool isStunReducing = false;


    private void ReduceStun() // 스턴 걸려 줄어드는 메소드
    {
        isStunReducing = true;
        spFillImage.color = restunColor;
        stun.DOValue(0f, stunTime).SetEase(Ease.Linear).OnComplete(OnCompleteStun);
    }

    private void OnCompleteStun()
    {
        isStunReducing = false;
        spFillImage.color = originStunColor;
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
        yield return new WaitForSeconds(0.3f);
        Refresh_fakeHp();
    }
    private void Refresh_fakeHp() // 지정된 피까지 스무스하게 줄어듦
    {
        fakeHp.DOValue(realHp.value, 1.5f, false).SetEase(Ease.OutExpo);
    }
}
