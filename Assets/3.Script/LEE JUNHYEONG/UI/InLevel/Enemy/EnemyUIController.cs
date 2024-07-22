using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/*<������Ʈ ����>
 RefreshHealth(float nowHealth, float maxHealth)�� �ҷ� hp ����
RefreshStun(float nowStun, float maxStun)�� �ҷ� stun ����

FixedUpdate���� ��� UI�� �÷��̾� ī�޶� ��� �ٶ󺾴ϴ�.
*/

public class EnemyUIController : MonoBehaviour
{
    [SerializeField] private Slider realHp;
    [SerializeField] private Slider fakeHp;
    [SerializeField] private Slider stun; // ���� ��ġ �����̴�

    [Header ("SP origin & Repaint Color")]
    [SerializeField] private Color originStunColor;
    [SerializeField] private Color restunColor; // ������ �ɷ��� ��� �ٲ� ���Դϴ�.
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
    private Coroutine timerCoroutine; // fakehp�� �ٴ� �ð��� ��� ���� ����
    private RectTransform rect; // ĵ������ ũ�� ������
    private float originScaleX = 1.5f;
    private float originScaleY = 1.5f;
    private Vector3 originScale;
    [SerializeField] private Camera mainCamera; // ������ ���� UI�� ������� ���� ī�޶�

    private float Distance; // �Ÿ��� ���� UI�� ũ�� ��ȭ�� ��Ÿ���ϴ�.

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

    private void SizeByDistance() // �Ÿ��� ���� UI�� ũ�� ��ȭ�Դϴ�.
    {
        Distance = Vector3.Distance(transform.position, mainCamera.transform.position);

        if (Distance > 10)
        {
            rect.localScale = new Vector3(Distance * 0.1f * originScaleX, Distance * 0.1f * originScaleY);
        }
    }
    public void RefreshHealth(float nowHealth, float maxHealth) // hp ����
    {
        realHp.value = nowHealth / maxHealth;
        Start_CountFillFakeHp();
    }

    public void RefreshStun(float nowStun, float maxStun) // stun ����
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


    private void ReduceStun() // ���� �ɷ� �پ��� �޼ҵ�
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
        yield return new WaitForSeconds(0.3f);
        Refresh_fakeHp();
    }
    private void Refresh_fakeHp() // ������ �Ǳ��� �������ϰ� �پ��
    {
        fakeHp.DOValue(realHp.value, 1.5f, false).SetEase(Ease.OutExpo);
    }
}
