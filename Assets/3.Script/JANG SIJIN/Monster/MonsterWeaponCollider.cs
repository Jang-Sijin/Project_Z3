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

    #region UniRx 몬스터 공격 처리    
    // 공격 충돌을 외부에서 구독할 수 있도록 Observable로 만듭니다.
    private Subject<Collider> _onWeaponHit = new Subject<Collider>();
    public IObservable<Collider> OnWeaponHit => _onWeaponHit.AsObservable();

    private void Start()
    {
        // UniRx를 사용하여 OnTriggerEnter 이벤트를 Observable로 전환
        this.OnTriggerEnterAsObservable()
            .Subscribe(_onWeaponHit)
            .AddTo(this);
    }
    #endregion
}