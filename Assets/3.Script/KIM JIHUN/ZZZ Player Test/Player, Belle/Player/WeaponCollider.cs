using System;
using System.Collections;
using System.Collections.Generic;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public class WeaponCollider : MonoBehaviour
{
    [HideInInspector] public BoxCollider weaponBoxCol;
    private bool isShakeTrigger;
    private bool isEnemyDetect;
    private void Awake()
    {
        weaponBoxCol = transform.GetComponent<BoxCollider>();
        isShakeTrigger = false;
        isEnemyDetect = false;
    }

    #region UniRx �÷��̾� ���� ó��    
    // ���� �浹�� �ܺο��� ������ �� �ֵ��� Observable�� ����ϴ�.
    private Subject<Collider> _onWeaponHit = new Subject<Collider>();
    public IObservable<Collider> OnWeaponHit => _onWeaponHit.AsObservable();

    private void Start()
    {
        // UniRx�� ����Ͽ� OnTriggerEnter �̺�Ʈ�� Observable�� ��ȯ
        this.OnTriggerEnterAsObservable()
            .Subscribe(_onWeaponHit) // ���⿡ ������ �־��� ���ɼ��� Ů�ϴ�.
            .AddTo(this);
    }
    #endregion

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            isEnemyDetect = true;
            if (isShakeTrigger)
                PlayerController.INSTANCE.ShakeCamera(1.5f, 0.1f);
        }
    }

    public void SetShakeTrigger(bool value)
    {
        isShakeTrigger = value;
    }

}
