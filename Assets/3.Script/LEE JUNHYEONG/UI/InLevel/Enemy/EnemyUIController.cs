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
    [SerializeField] private Slider realHp; // 바로 수치가 감소하는 hp
    [SerializeField] private Slider fakeHp; // 일정시간 이후 수치가 감소하는 hp
    [SerializeField] private Slider stun; // 스턴 수치 슬라이더

    [Header ("SP origin & Repaint Color")]
    [SerializeField] private Color originStunColor; // 원본 스턴 컬러
    [SerializeField] private Color restunColor; // 스턴 발생 시 적용할 컬러
    private Image spFillImage; // 컬러를 바꿀 fill 이미지

    private float stunTime = 5f; // 스턴 시간
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

    private Coroutine timerCoroutine; // fakehp의 다는 시간을 재기 위한 변수
    private RectTransform rect; // 캔버스의 크기 조절용
    private float originScaleX = 1f;
    private float originScaleY = 1f;
    private Vector3 originScale;
    [SerializeField] private Transform monsterTransform; // 몬스터 오브젝트
    [SerializeField] private Camera mainCamera; // 시점에 따라 UI가 따라오기 위한 카메라

    private float Distance; // 거리에 따른 UI의 크기 변화를 나타냅니다.

    private void Start()
    {
        mainCamera = GameObject.FindWithTag("MainCamera").GetComponent<Camera>();
        monsterTransform = transform.root.GetComponent<Build_MonsterController>().transform;
        rect = GetComponentInParent<RectTransform>();
        spFillImage = stun.fillRect.GetComponent<Image>();
        originScale = new Vector3(originScaleX, originScaleY);
    }

    private void Update()
    {
        LookPlayer();
        //SizeByDistance();
    }

    private void LookPlayer() // 플레이어 바라보기
    {
        // 캔버스를 카메라가 보는 정방향으로 회전시킵니다.
        Vector3 direction = (transform.position - mainCamera.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
    }

    //private void SizeByDistance() // 거리에 따른 UI의 크기 변화입니다.
    //{
    //    Distance = Vector3.Distance(transform.position, mainCamera.transform.position);

    //    if (Distance > 10)
    //    {
    //        rect.localScale = new Vector3(Distance * 0.1f * originScaleX, Distance * 0.1f * originScaleY);
    //    }
    //}

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

    public bool isStunReducing = false; // 스턴 여부 확인


    private void ReduceStun() // 스턴 걸려 줄어드는 메소드
    {
        isStunReducing = true;
        spFillImage.color = restunColor;
        stun.DOValue(0f, stunTime).SetEase(Ease.Linear).OnComplete(OnCompleteStun);
    }

    private void OnCompleteStun() // 스턴이 끝날 시 불 값 변경 및 이미지 본 색으로 변경
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
