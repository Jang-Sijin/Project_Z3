using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] private Slider stun;
    [SerializeField] private Collider monster;
    [SerializeField] private Text stunText;
    private Coroutine timerCoroutine;
    private Camera mainCamera; // ������ ���� UI�� ������� ���� ī�޶� �޾ƿɴϴ�.

    //***************************************************************************
    //private float curHp;
    //private float maxhp;
    //private float curStun;
    //private float maxStun;
    //***************************************************************************
    // DB �����Ϸ��ϸ� ���� ����

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

    public void RefreshHealth(float nowHealth, float maxHealth) // hp ����
    {
        realHp.value = nowHealth / maxHealth;
        Start_CountFillFakeHp();
    }

    public void RefreshStun(float nowStun, float maxStun) // stun ����
    {
        stun.value = nowStun / maxStun;
        stunText.text = $"{stun.value * 100}";
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
