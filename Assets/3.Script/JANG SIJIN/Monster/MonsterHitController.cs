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
        // ���� �ݶ��̴��� OnWeaponHit �̺�Ʈ�� ����
        _monsterweaponCollider.OnWeaponHit
            .Where(collider => _monsterModel.currentState == EMonsterState.Attack)
            .Subscribe(collider =>
            {
                Debug.Log("���� �ݶ��̴��� �浹 �̺�Ʈ �߻�!");
                if (collider.CompareTag("Player"))
                {
                    Debug.Log("���� > �÷��̾� �ݶ��̴� �浹");
                    // ������ �浹 �� ó�� ����
                    var player = collider.GetComponentInParent<PlayerController>();
                    if (player != null)
                    {
                        Debug.Log("�ݶ��̴��� ��Ʈ�� PlayerController ������Ʈ ����");
                        float monsterDamage = GetMonsterAttackDamage();                        
                        
                        player.TakeDamage(monsterDamage, collider.transform.position); // �÷��̾�� ����� ���� ó��.

                        Debug.Log($"{_monsterModel.name}�� {player.name} �÷��̾�� ����� {monsterDamage}�� ��");

                        // Player UI ���� ó��
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