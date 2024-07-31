using UnityEngine;
using System.Linq;
using UniRx;
using static Define;

public class MonsterHitController : MonoBehaviour
{
    [SerializeField] private Build_MonsterModel _monsterModel;
    [SerializeField] private MonsterWeaponCollider _monsterweaponCollider;

    private void Start()
    {
        _monsterModel = GetComponent<Build_MonsterModel>();
        _monsterweaponCollider = GetComponentInChildren<MonsterWeaponCollider>();

        OnWeaponHitEvent();
    }

    private void OnWeaponHitEvent()
    {
        // 무기 콜라이더의 OnWeaponHit 이벤트를 구독
        _monsterweaponCollider.OnWeaponHit
            .Where(collider => _monsterModel.currentState == EMonsterState.Attack)
            .Subscribe(collider =>
            {
                Debug.Log("몬스터 콜라이더에 충돌 이벤트 발생!");
                if (collider.CompareTag("Player"))
                {
                    Debug.Log("몬스터 > 플레이어 콜라이더 충돌");
                    // 적군과 충돌 시 처리 로직
                    var player = collider.GetComponentInParent<PlayerController>();
                    if (player != null)
                    {
                        Debug.Log("콜라이더에 히트된 PlayerController 컴포넌트 존재");
                        float monsterDamage = GetMonsterAttackDamage();                        
                        
                        player.TakeDamage(monsterDamage, collider.transform.position); // 플레이어에게 대미지 입힘 처리.

                        Debug.Log($"{_monsterModel.name}가 {player.name} 플레이어에게 대미지 {monsterDamage}를 줌");

                        // Player UI 갱신 처리
                        UIManager.Instance.InGameUI.RefreshIngameUI();
                    }
                }
            })
            .AddTo(this);
    }

    private float GetMonsterAttackDamage()
    {
        float monsterDamage = _monsterModel.monsterInfo.defaultAttackDamage;

        return monsterDamage;
    }
}