using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField] private EPlayerState _currentState;
    [SerializeField] private WeaponCollider _weaponCollider;

    private void Start()
    {
        //// 무기 콜라이더의 OnWeaponHit 이벤트를 구독
        //_weaponCollider.OnWeaponHit
        //    .Where(_ => _currentState == EPlayerState.NormalAttack ||
        //    _currentState == EPlayerState.NormalAttakEnd ||
        //    _currentState == EPlayerState.AttackSkillEx ||
        //    _currentState == EPlayerState.AttackSkillEnd)  // 플레이어가 공격 상태일 때만 처리
        //    .Subscribe((System.IObserver<Collider>)(collider =>
        //    {
        //        if (collider.CompareTag("Enemy"))
        //        {
        //            // 적군과 충돌 시 처리 로직
        //            var enemy = collider.GetComponent<MonsterModel>();
        //            if (enemy != null)
        //            {
        //                enemy.TakeDamage(10);  // 예시로 데미지를 10으로 설정
        //            }
        //        }
        //    }))
        //    .AddTo(this);
    }
}