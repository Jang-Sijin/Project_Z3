using UnityEngine;
using System.Linq;
using UniRx;
using UnityEngine.TextCore.Text;
using UnityEngine.Playables;

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
                    var enemy = collider.GetComponent<MonsterController>();
                    if (enemy != null)
                    {
                        float playerDamage = GetPlayerCharacterStepAttackDamage();

                        enemy.TakeDamage(playerDamage, _playerModel.transform); // 몬스터에게 대미지 입힘 처리.

                        Debug.Log($"{_playerModel.eCharacter}가 {enemy.name} 몬스터에게 대미지 {playerDamage}를 줌");
                    }
                }
            })
            .AddTo(this);
    }

    private float GetPlayerCharacterStepAttackDamage()
    {
        float playerDamage = _playerModel.playerStatus.CurrentAttackDamage;

        // 캐릭터가 현재 공격 중인 상태
        switch (_playerModel.eCharacter)
        {
            case ECharacter.Corin:
                if (_playerModel.currentState == EPlayerState.NormalAttack || _playerModel.currentState == EPlayerState.NormalAttackEnd)
                    playerDamage = _playerModel.playerStatus.DefaultAttackDamage * _playerModel.playerStatus.NormalAttackDamageMultiple[_playerModel.currentNormalAttakIndex - 1];
                else if (_playerModel.currentState == EPlayerState.AttackSkillEx || _playerModel.currentState == EPlayerState.AttackSkillEnd)
                    playerDamage = _playerModel.playerStatus.DefaultAttackDamage * _playerModel.playerStatus.ExSkillDamage;
                break;
            case ECharacter.Anbi:
                if (_playerModel.currentState == EPlayerState.NormalAttack || _playerModel.currentState == EPlayerState.NormalAttackEnd)
                    playerDamage = _playerModel.playerStatus.DefaultAttackDamage * _playerModel.playerStatus.NormalAttackDamageMultiple[_playerModel.currentNormalAttakIndex - 1];
                else if (_playerModel.currentState == EPlayerState.AttackSkillEx || _playerModel.currentState == EPlayerState.AttackSkillEnd)
                    playerDamage = _playerModel.playerStatus.DefaultAttackDamage * _playerModel.playerStatus.ExSkillDamage;
                break;
            case ECharacter.Longinus:
                if (_playerModel.currentState == EPlayerState.NormalAttack || _playerModel.currentState == EPlayerState.NormalAttackEnd)
                    playerDamage = _playerModel.playerStatus.DefaultAttackDamage * _playerModel.playerStatus.NormalAttackDamageMultiple[_playerModel.currentNormalAttakIndex - 1];
                else if (_playerModel.currentState == EPlayerState.AttackSkillEx || _playerModel.currentState == EPlayerState.AttackSkillEnd)
                    playerDamage = _playerModel.playerStatus.DefaultAttackDamage * _playerModel.playerStatus.ExSkillDamage;
                break;    
        }

        return playerDamage;
    }
}