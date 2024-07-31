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
    [SerializeField] private Slider realHp; // �ٷ� ��ġ�� �����ϴ� hp
    [SerializeField] private Slider fakeHp; // �����ð� ���� ��ġ�� �����ϴ� hp
    [SerializeField] private Slider stun; // ���� ��ġ �����̴�

    [Header ("SP origin & Repaint Color")]
    [SerializeField] private Color originStunColor; // ���� ���� �÷�
    [SerializeField] private Color restunColor; // ���� �߻� �� ������ �÷�
    private Image spFillImage; // �÷��� �ٲ� fill �̹���

    private float stunTime = 5f; // ���� �ð�
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

    private Coroutine timerCoroutine; // fakehp�� �ٴ� �ð��� ��� ���� ����
    private RectTransform rect; // ĵ������ ũ�� ������
    private float originScaleX = 1f;
    private float originScaleY = 1f;
    private Vector3 originScale;
    [SerializeField] private Transform monsterTransform; // ���� ������Ʈ
    [SerializeField] private Camera mainCamera; // ������ ���� UI�� ������� ���� ī�޶�

    private float Distance; // �Ÿ��� ���� UI�� ũ�� ��ȭ�� ��Ÿ���ϴ�.

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

    private void LookPlayer() // �÷��̾� �ٶ󺸱�
    {
        // ĵ������ ī�޶� ���� ���������� ȸ����ŵ�ϴ�.
        Vector3 direction = (transform.position - mainCamera.transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        transform.rotation = lookRotation;
    }

    //private void SizeByDistance() // �Ÿ��� ���� UI�� ũ�� ��ȭ�Դϴ�.
    //{
    //    Distance = Vector3.Distance(transform.position, mainCamera.transform.position);

    //    if (Distance > 10)
    //    {
    //        rect.localScale = new Vector3(Distance * 0.1f * originScaleX, Distance * 0.1f * originScaleY);
    //    }
    //}

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

    public bool isStunReducing = false; // ���� ���� Ȯ��


    private void ReduceStun() // ���� �ɷ� �پ��� �޼ҵ�
    {
        isStunReducing = true;
        spFillImage.color = restunColor;
        stun.DOValue(0f, stunTime).SetEase(Ease.Linear).OnComplete(OnCompleteStun);
    }

    private void OnCompleteStun() // ������ ���� �� �� �� ���� �� �̹��� �� ������ ����
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
