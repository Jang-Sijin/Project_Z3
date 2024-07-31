using System;
using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UniRx;
using UnityEngine;

public class MonsterWeaponCollider : MonoBehaviour
{
    [HideInInspector] public BoxCollider weaponBoxCol;

    private void Awake()
    {
        weaponBoxCol = transform.GetComponent<BoxCollider>();
    }

    #region UniRx ���� ���� ó��    
    // ���� �浹�� �ܺο��� ������ �� �ֵ��� Observable�� ����ϴ�.
    private Subject<Collider> _onWeaponHit = new Subject<Collider>();
    public IObservable<Collider> OnWeaponHit => _onWeaponHit.AsObservable();

    private void Start()
    {
        // UniRx�� ����Ͽ� OnTriggerEnter �̺�Ʈ�� Observable�� ��ȯ
        this.OnTriggerEnterAsObservable()
            .Subscribe(_onWeaponHit)
            .AddTo(this);
    }
    #endregion
}