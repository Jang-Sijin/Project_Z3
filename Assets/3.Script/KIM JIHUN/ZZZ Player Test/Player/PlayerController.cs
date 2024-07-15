using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerController : SingleMonoBase<PlayerController>, IStateMachineOwner
{
    [HideInInspector] public PlayerInputSystem playerInputSystem; // Input 시스템
    public PlayerModel playerModel; //현재 컨트롤중인 플레이어 모델

    private StateMachine stateMachine;

    public PlayerConfig playerConfig; //현재 할당되어 있는 Player Config. 최대 3명의 캐릭터가 있음

    private List<PlayerModel> controllableModels; //playerModel 변수에 사용할 수 있는 PlayerModel 배열
    private int currentModelIndex = 0; //현재 컨트롤중인 모델 인덱스

    public Vector2 inputMoveVec2; // WASD 키보드로 입력
    public float rotationSpeed = 8f;
    [HideInInspector] public float evadeTimer { get; private set; } // 회피 타이머
    [HideInInspector] public float evadeCoolTime { get; private set; } // 회피 쿨타임

    public List<GameObject> enemyList { get; private set; }
    //public GameObject closestEnemy { get; private set; }
    public GameObject closestEnemy;
    public Vector3 directionToEnemy { get; private set; }


    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);

        playerInputSystem = new PlayerInputSystem();
        evadeCoolTime = 0.5f;
        evadeTimer = evadeCoolTime;

        controllableModels = new List<PlayerModel>();
        for (int i = 0; i < playerConfig.models.Length; i++)
        {
            GameObject model = Instantiate(playerConfig.models[i], transform);
            controllableModels.Add(model.GetComponent<PlayerModel>());
            controllableModels[i].gameObject.SetActive(false);
        }
        SetPlayerModel(0);

        enemyList = new List<GameObject>();
    }

    private void Start()
    {
        LockMouse();
        SwitchState(EPlayerState.Idle);
    }

    /// <summary>
    /// 현재 상태를 playerState로 변경
    /// </summary>
    /// <param name="playerState">변경할 State</param>
    public void SwitchState(EPlayerState playerState)
    {
        playerModel.currentState = playerState;
        switch (playerState)
        {
            case EPlayerState.Idle:
            case EPlayerState.IdleAFK:
                stateMachine.EnterState<PlayerIdleState>(true);
                break;
            case EPlayerState.Walk:
            case EPlayerState.Run:
                stateMachine.EnterState<PlayerRunState>(true);
                break;
            case EPlayerState.RunStart:
                stateMachine.EnterState<PlayerRunStartState>();
                break;
            case EPlayerState.RunEnd:
                stateMachine.EnterState<PlayerRunEndState>();
                break;
            case EPlayerState.TurnBack:
                stateMachine.EnterState<PlayerTurnBackState>();
                break;
            case EPlayerState.EvadeFront:
            case EPlayerState.EvadeBack:
                if (evadeTimer != evadeCoolTime) return;

                stateMachine.EnterState<PlayerEvadeState>();
                evadeTimer -= evadeCoolTime;
                break;
            case EPlayerState.EvadeFrontEnd:
            case EPlayerState.EvadeBackEnd:
                stateMachine.EnterState<PlayerEvadeEndState>();
                break;
            case EPlayerState.NormalAttack:
                stateMachine.EnterState<PlayerNormalAttackState>(true);
                break;
            case EPlayerState.NormalAttakEnd:
                stateMachine.EnterState<PlayerNormalAttackEndState>();
                break;
            case EPlayerState.AttackUltStart:
                stateMachine.EnterState<PlayerUltStartState>();
                break;
            case EPlayerState.AttackUlt:
                stateMachine.EnterState<PlayerUltState>();
                break;
            case EPlayerState.AttackUltEnd:
                stateMachine.EnterState<PlayerUltEndState>();
                break;
            case EPlayerState.SwitchIn:
                stateMachine.EnterState<PlayerSwitchInState>();
                break;
            case EPlayerState.AttackSkill:
                stateMachine.EnterState<PlayerSkillState>();
                break;
            case EPlayerState.AttackSkillEx:
                stateMachine.EnterState<PlayerSkillExtraState>();
                break;
            case EPlayerState.AttackSkillEnd:
                stateMachine.EnterState<PlayerSkillEndState>();
                break;
            case EPlayerState.AttackRush:
                stateMachine.EnterState<PlayerRushState>();
                break;
            case EPlayerState.AttackRushEnd:
                stateMachine.EnterState<PlayerRushEndState>();
                break;
        }
    }

    /// <summary>
    /// 다음 캐릭터로 교체
    /// </summary>
    public void SwitchNextModel()
    {
        //사용하던 StateMachine 초기화
        stateMachine.Clear();

        playerModel.Exit();

        currentModelIndex++;
        if (currentModelIndex >= controllableModels.Count)
            currentModelIndex = 0;
        PlayerModel nextModel = controllableModels[currentModelIndex];
        nextModel.gameObject.SetActive(true);
        //nextModel.gameObject.transform.position = currentPosition;
        playerModel = nextModel;
        //nextModel.gameObject.transform.position = playerModel.transform.position;
        //Debug.Log($"Next Model : {nextModel.transform.name}, {nextModel.gameObject.transform.position}");
        //Debug.Log($"Player Model: {playerModel.transform.name}, {playerModel.gameObject.transform.position}");
        playerModel.Enter();
        SwitchState(EPlayerState.Idle);

    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        playerModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration, float fixedTimeOffset)
    {
        playerModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration, 0, fixedTimeOffset);
    }

    private void Update()
    {
        if (enemyList.Count != 0)
        {
            //가장 가까운 몬스터 순으로 -> 레이캐스트가 가능한 개체 찾기
            closestEnemy = RaycastEnemy();
            if (closestEnemy != null)
            {
                Vector3 tmpEnemy = new Vector3(closestEnemy.transform.position.x, 0, closestEnemy.transform.position.z);
                Vector3 tmpPlayer = new Vector3(playerModel.transform.position.x, 0, playerModel.transform.position.z);
                directionToEnemy = (tmpEnemy - tmpPlayer).normalized;
            }
        }
        else
        {
            if (closestEnemy != null)
                closestEnemy = null;
        }


        inputMoveVec2 = playerInputSystem.Player.Move.ReadValue<Vector2>().normalized;

        if (evadeTimer < evadeCoolTime)
        {
            evadeTimer += Time.deltaTime;
            //Debug.Log(evadeTimer);
            if (evadeTimer > evadeCoolTime)
                evadeTimer = evadeCoolTime;
        }
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        playerInputSystem.Enable();
    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
    }

    public void SetPlayerModel(int index)
    {
        currentModelIndex = index;
        controllableModels[currentModelIndex].gameObject.SetActive(true);
        playerModel = controllableModels[currentModelIndex];
    }

    public void AddEnemy(GameObject enemyToAdd)
    {
        enemyList.Add(enemyToAdd);
    }

    public void RemoveEnemy(GameObject enemyToRemove)
    {
        enemyList.Remove(enemyToRemove);
    }

    public GameObject RaycastEnemy()
    {

        Vector3 playerPosition = playerModel.transform.position;
        foreach (var enemy in SortEnemyByDist())
        {
            Vector3 directionToEnemy = (enemy.transform.position - playerPosition).normalized;
            RaycastHit hit;

            // 플레이어 위치에서 적 방향으로 레이캐스트
            if (Physics.Raycast(playerPosition, directionToEnemy, out hit))
            {
                // 히트된 객체가 적이라면 해당 적을 반환
                if (hit.collider.gameObject == enemy)
                {
                    return enemy;
                }
            }
        }
        return null;
    }

    public GameObject[] SortEnemyByDist()
    {
        Vector3 referencePosition = playerModel.transform.position;

        GameObject[] sortedGameObjects = enemyList
            .OrderBy(go => Vector3.Distance(go.transform.position, referencePosition)) // 각 GameObject의 위치와 기준 위치 사이의 거리 계산
            .ToArray(); // 정렬된 결과를 배열로 변환

        return sortedGameObjects; // 정렬된 GameObject 배열 반환
    }
}
