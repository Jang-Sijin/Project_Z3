using UnityEngine;
using UnityEngine.InputSystem.Utilities;

public class PlayerHitController : MonoBehaviour
{
    [SerializeField] private EPlayerState _currentState;
    [SerializeField] private WeaponCollider _weaponCollider;

    private void Start()
    {
        //// ���� �ݶ��̴��� OnWeaponHit �̺�Ʈ�� ����
        //_weaponCollider.OnWeaponHit
        //    .Where(_ => _currentState == EPlayerState.NormalAttack ||
        //    _currentState == EPlayerState.NormalAttakEnd ||
        //    _currentState == EPlayerState.AttackSkillEx ||
        //    _currentState == EPlayerState.AttackSkillEnd)  // �÷��̾ ���� ������ ���� ó��
        //    .Subscribe((System.IObserver<Collider>)(collider =>
        //    {
        //        if (collider.CompareTag("Enemy"))
        //        {
        //            // ������ �浹 �� ó�� ����
        //            var enemy = collider.GetComponent<MonsterModel>();
        //            if (enemy != null)
        //            {
        //                enemy.TakeDamage(10);  // ���÷� �������� 10���� ����
        //            }
        //        }
        //    }))
        //    .AddTo(this);
    }
}