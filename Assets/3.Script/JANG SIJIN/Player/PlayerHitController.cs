using UnityEngine;
using System.Linq;
using UniRx;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField] private PlayerModel _playerModel;    
    [SerializeField] private WeaponCollider _weaponCollider;

    private void Start()
    {
        _playerModel = GetComponent<PlayerModel>();
        _weaponCollider = GetComponentInChildren<WeaponCollider>();

        OnWeaponHitEvent();
    }

    private void OnWeaponHitEvent()
    {
        // 무기 콜라이더의 OnWeaponHit 이벤트를 구독
        _weaponCollider.OnWeaponHit
            .Where(collider => _playerModel.currentState == EPlayerState.NormalAttack ||
                               _playerModel.currentState == EPlayerState.NormalAttackEnd ||
                               _playerModel.currentState == EPlayerState.AttackSkillEx ||
                               _playerModel.currentState == EPlayerState.AttackSkillEnd)  // 플레이어가 공격 상태일 때만 처리
            .Subscribe(collider =>
            {
                Debug.Log("무기 콜라이더에 충돌 이벤트 발생!");
                if (collider.CompareTag("Enemy"))
                {
                    // 적군과 충돌 시 처리 로직
                    var enemy = collider.GetComponent<MonsterModel>();
                    if (enemy != null)
                    {
                        Debug.Log("대미지 10 줬음");
                        enemy.TakeDamage(1);  // 예시로 데미지를 10으로 설정
                    }
                }
            })
            .AddTo(this);
    }
}