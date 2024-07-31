using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using JSJ;
using TMPro;
using UnityEngine;

public enum EMonsterState
{
    Idle,
    Spawn,
    Run,
    Attack,
    Hit,
    Die
}

public class Build_MonsterController : MonoBehaviour, IStateMachineOwner
{
    [SerializeField] private GameObject _damageFontCanvasPrefab;      
    [HideInInspector] public Build_MonsterModel monsterModel;
    private EnemyUIController enemyUIController;

    private StateMachine stateMachine;

    private void Awake()
    {
        stateMachine = new StateMachine(this);
        monsterModel = GetComponent<Build_MonsterModel>();
        enemyUIController = GetComponentInChildren<EnemyUIController>();
    }

    private void OnEnable()
    {
        SwitchState(EMonsterState.Spawn);     
    }

    private void OnDisable()
    {        
        SwitchState(EMonsterState.Idle);
    }

    public void SwitchState(EMonsterState monsterState)
    {
        monsterModel.currentState = monsterState;
        switch (monsterState)
        {
            case EMonsterState.Idle:
                stateMachine.EnterState<MonsterIdleState>();
                break;
            case EMonsterState.Spawn:
                stateMachine.EnterState<MonsterSpawnState>();
                break;
            case EMonsterState.Run:
                stateMachine.EnterState<MonsterRunState>();
                break;
            case EMonsterState.Attack:
                stateMachine.EnterState<MonsterAttackState>(true);
                break;
            case EMonsterState.Hit:
                stateMachine.EnterState<MonsterHitState>();
                break;
            case EMonsterState.Die:
                stateMachine.EnterState<MonsterDieState>();
                break;
        }
    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        monsterModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration, float fixedTimeOffset)
    {
        monsterModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration, 0, fixedTimeOffset);
    }

    public void TakeDamage(float playerDamage, Vector3 playerWeaponPostion)
    {
        //  Debug.Log("TakeDamage: ���� ����� ���� ����");
        //if (monsterModel.currentState == EMonsterState.Die)
        //{
        //    SwitchState(EMonsterState.Die);
        //    return;
        //}

        if (monsterModel.monsterStatus.CurrentHealth.Value > 0)
        {
            // ���� ���� ����(Hit) ���·� ����
            SwitchState(EMonsterState.Hit);

            monsterModel.monsterStatus.SetCurrentHealth(monsterModel.monsterStatus.CurrentHealth.Value - playerDamage);
            // EnemyUIController.RefreshHealth(_currentHealth, MaxHealth);

            ShowDamageFont(playerDamage, playerWeaponPostion);
            enemyUIController.RefreshHealth(monsterModel.monsterStatus.CurrentHealth.Value, monsterModel.monsterStatus.MaxHealth); // ���� HP Bar ����
            return;
        }
        else
        {
            Debug.Log($"{gameObject.name}: ���� ���");            
            SwitchState(EMonsterState.Die);
            return;
        }
    }

    // ����� ��Ʈ�� ����մϴ�.
    private void ShowDamageFont(float damage, Vector3 hitPosition)
    {
        // ������ ��Ʈ ������ �ν��Ͻ�ȭ
        GameObject damageFont = Instantiate(_damageFontCanvasPrefab, hitPosition, Quaternion.identity);
        // Canvas�� Text ��ҿ� �����Ͽ� ������ ���� ����
        TextMeshProUGUI textMesh = damageFont.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = damage.ToString();
        }

        // �ؽ�Ʈ �ڽ��� RectTransform ����
        RectTransform textRectTransform = textMesh.GetComponent<RectTransform>();

        // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ�ϰ� �ణ�� �������� �߰�
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(hitPosition);
        screenPoint.x += Random.Range(-100f, 100f);        
        textRectTransform.position = screenPoint;

        // DOTween�� ����Ͽ� �ִϸ��̼� ����
        AnimateDamageFont(textRectTransform, damageFont.transform);
    }

    private void AnimateDamageFont(RectTransform damageFontRectTransform, Transform startPosition)    
    {
        // DOTween�� ����Ͽ� ������� �̵� �� ������ �Ʒ��� �������� �ִϸ��̼� ����
        var sequence = DOTween.Sequence();

        // ������� �̵�
        sequence.Append(damageFontRectTransform.DOMoveY(damageFontRectTransform.position.y + 600f, 1f)
            .SetEase(Ease.OutCubic));

        // ������ �Ʒ��� ������
        sequence.Append(damageFontRectTransform.DOMoveY(damageFontRectTransform.position.y - 100f, 0.3f)
            .SetEase(Ease.InCubic));

        // ���� ����
        sequence.Join(damageFontRectTransform.GetComponent<TextMeshProUGUI>().DOFade(0, 0.3f)
            .SetEase(Ease.InOutQuad));

        sequence.Play().OnComplete(() => Destroy(startPosition.transform.gameObject, 1.0f)); // ���� �ð� �Ŀ� ������ ��Ʈ ������Ʈ�� ����
    }
}
