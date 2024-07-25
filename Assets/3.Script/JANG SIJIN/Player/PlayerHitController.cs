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
                    var enemy = collider.GetComponent<MonsterModel>();
                    if (enemy != null)
                    {
                        Debug.Log("����� 10 ����");
                        enemy.TakeDamage(1);  // ���÷� �������� 10���� ����
                    }
                }
            })
            .AddTo(this);
    }
}