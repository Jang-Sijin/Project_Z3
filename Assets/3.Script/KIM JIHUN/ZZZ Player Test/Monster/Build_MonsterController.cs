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
        //  Debug.Log("TakeDamage: 몬스터 대미지 피해 입음");
        //if (monsterModel.currentState == EMonsterState.Die)
        //{
        //    SwitchState(EMonsterState.Die);
        //    return;
        //}

        if (monsterModel.monsterStatus.CurrentHealth.Value > 0)
        {
            // 몬스터 공격 받음(Hit) 상태로 변경
            SwitchState(EMonsterState.Hit);

            monsterModel.monsterStatus.SetCurrentHealth(monsterModel.monsterStatus.CurrentHealth.Value - playerDamage);
            // EnemyUIController.RefreshHealth(_currentHealth, MaxHealth);

            ShowDamageFont(playerDamage, playerWeaponPostion);
            enemyUIController.RefreshHealth(monsterModel.monsterStatus.CurrentHealth.Value, monsterModel.monsterStatus.MaxHealth); // 몬스터 HP Bar 갱신
            return;
        }
        else
        {
            Debug.Log($"{gameObject.name}: 몬스터 사망");            
            SwitchState(EMonsterState.Die);
            return;
        }
    }

    // 대미지 폰트를 출력합니다.
    private void ShowDamageFont(float damage, Vector3 hitPosition)
    {
        // 데미지 폰트 프리팹 인스턴스화
        GameObject damageFont = Instantiate(_damageFontCanvasPrefab, hitPosition, Quaternion.identity);
        // Canvas의 Text 요소에 접근하여 데미지 값을 설정
        TextMeshProUGUI textMesh = damageFont.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = damage.ToString();
        }

        // 텍스트 박스의 RectTransform 설정
        RectTransform textRectTransform = textMesh.GetComponent<RectTransform>();

        // 월드 좌표를 스크린 좌표로 변환하고 약간의 오프셋을 추가
        Vector3 screenPoint = Camera.main.WorldToScreenPoint(hitPosition);
        screenPoint.x += Random.Range(-100f, 100f);        
        textRectTransform.position = screenPoint;

        // DOTween을 사용하여 애니메이션 적용
        AnimateDamageFont(textRectTransform, damageFont.transform);
    }

    private void AnimateDamageFont(RectTransform damageFontRectTransform, Transform startPosition)    
    {
        // DOTween을 사용하여 상단으로 이동 후 빠르게 아래로 떨어지는 애니메이션 적용
        var sequence = DOTween.Sequence();

        // 상단으로 이동
        sequence.Append(damageFontRectTransform.DOMoveY(damageFontRectTransform.position.y + 600f, 1f)
            .SetEase(Ease.OutCubic));

        // 빠르게 아래로 떨어짐
        sequence.Append(damageFontRectTransform.DOMoveY(damageFontRectTransform.position.y - 100f, 0.3f)
            .SetEase(Ease.InCubic));

        // 투명도 감소
        sequence.Join(damageFontRectTransform.GetComponent<TextMeshProUGUI>().DOFade(0, 0.3f)
            .SetEase(Ease.InOutQuad));

        sequence.Play().OnComplete(() => Destroy(startPosition.transform.gameObject, 1.0f)); // 일정 시간 후에 데미지 폰트 오브젝트를 삭제
    }
}
