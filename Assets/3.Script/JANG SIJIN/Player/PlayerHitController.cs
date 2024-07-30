using UnityEngine;
using System.Linq;
using UniRx;
using UnityEngine.TextCore.Text;
using UnityEngine.Playables;
using JSJ;

public class PlayerHitController : MonoBehaviour
{    
    [SerializeField] private PlayerModel _playerModel;    
    [SerializeField] private WeaponCollider _weaponCollider;

    private void Start()
    {
        //_playerController = PlayerController.INSTANCE.;
        _playerModel = GetComponent<PlayerModel>();
        _weaponCollider = GetComponentInChildren<WeaponCollider>();

        OnWeaponHitEvent();
    }

    private void OnWeaponHitEvent()
    {
        // ���� �ݶ��̴��� OnWeaponHit �̺�Ʈ�� ����
        _weaponCollider.OnWeaponHit
            .Where(collider => _playerModel.currentState == EPlayerState.NormalAttack ||
                               _playerModel.currentState == EPlayerState.NormalAttackEnd ||
                               _playerModel.currentState == EPlayerState.AttackSkillEx ||
                               _playerModel.currentState == EPlayerState.AttackSkillEnd)  // �÷��̾ ���� ������ ���� ó��
            .Subscribe(collider =>
            {
                Debug.Log("���� �ݶ��̴��� �浹 �̺�Ʈ �߻�!");
                if (collider.CompareTag("Enemy"))
                {
                    // ������ �浹 �� ó�� ����
                    var enemy = collider.GetComponent<Build_MonsterController>();
                    if (enemy != null)
                    {
                        float playerDamage = GetPlayerCharacterStepAttackDamage();

                        // �÷��̾� �ñر� ����Ʈ ����
                        if (PlayerController.INSTANCE.CurrentUltPoint <= PlayerController.INSTANCE.MaxUltPoint)
                        {
                            PlayerController.INSTANCE.CurrentUltPoint += PlayerController.INSTANCE.DefaultUltPoint;
                        }
                        else
                        {
                            PlayerController.INSTANCE.CurrentUltPoint = PlayerController.INSTANCE.MaxUltPoint;
                        }
                        Debug.Log($"�ñر� ������: {PlayerController.INSTANCE.CurrentUltPoint}");

                        // �÷��̾� ��ų ����Ʈ ����
                        if (_playerModel.playerStatus.CurrentSkillPoint <= _playerModel.playerStatus.MaxSkillPoint)
                        {
                            _playerModel.playerStatus.CurrentSkillPoint += _playerModel.playerStatus.SkillPoint;
                        }
                        else
                        {
                            _playerModel.playerStatus.CurrentSkillPoint = _playerModel.playerStatus.MaxSkillPoint;
                        }
                        Debug.Log($"��ų ������: {_playerModel.playerStatus.CurrentSkillPoint}");

                        enemy.TakeDamage(playerDamage, _playerModel.transform); // ���Ϳ��� ����� ���� ó��.

                        Debug.Log($"{_playerModel.eCharacter}�� {enemy.name} ���Ϳ��� ����� {playerDamage}�� ��");
                    }
                }
            })
            .AddTo(this);
    }

    private float GetPlayerCharacterStepAttackDamage()
    {
        float playerDamage = _playerModel.playerStatus.CurrentAttackDamage;

        // ĳ���Ͱ� ���� ���� ���� ����
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