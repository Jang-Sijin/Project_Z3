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

    #region UniRx 플레이어 공격 처리    
    // 공격 충돌을 외부에서 구독할 수 있도록 Observable로 만듭니다.
    private Subject<Collider> _onWeaponHit = new Subject<Collider>();
    public IObservable<Collider> OnWeaponHit => _onWeaponHit.AsObservable();

    private void Start()
    {
        // UniRx를 사용하여 OnTriggerEnter 이벤트를 Observable로 전환
        this.OnTriggerEnterAsObservable()
            .Subscribe(_onWeaponHit) // 여기에 문제가 있었을 가능성이 큽니다.
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
