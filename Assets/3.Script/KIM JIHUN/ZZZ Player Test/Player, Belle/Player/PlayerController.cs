using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;
using Unity.VisualScripting.Dependencies.Sqlite;
using Unity.VisualScripting;

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

    #region Player Active Cool Down
    [HideInInspector] public float evadeTimer { get; private set; } // 회피 타이머
    [HideInInspector] private float evadeCoolTime; // 회피 쿨타임
    [HideInInspector] public float switchTimer { get; private set; } // 캐릭터 교체 타이머
    [HideInInspector] private float switchCoolTime; // 캐릭터 교체 쿨타임
    private float maxUltPoint = 100f;
    private float currentUltPoint = 0;

    public float CurrentUltPoint
    {
        get { return currentUltPoint; }
        set { currentUltPoint = Mathf.Clamp(value, 0, maxUltPoint); }
    }

    private bool canInput = true; // 대화, 컷신에 사용될 bool값. 캐릭터를 조종할 수 있는지
    public bool CanInput
    {
        get { return canInput; }
        set
        {
            canInput = value;
        }
    }
    #endregion

    public List<GameObject> enemyList { get; private set; }
    //public GameObject closestEnemy { get; private set; }
    [HideInInspector] public GameObject closestEnemy;
    [HideInInspector] public Vector3 directionToEnemy { get; private set; }

    [SerializeField] private CinemachineFreeLook cinemachineFreeLook; //카메라 쉐이크를 위한 시네머신
    private float shakeTimer; //쉐이크 타이머

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);

        playerInputSystem = new PlayerInputSystem();
        evadeCoolTime = 0.5f;
        switchCoolTime = 1f;
        evadeTimer = evadeCoolTime;

        controllableModels = new List<PlayerModel>();
        for (int i = 0; i < playerConfig.models.Length; i++)
        {
            GameObject model = Instantiate(playerConfig.models[i], transform);
            //controllableModels[i].gameObject.SetActive(true);
            //model.SetActive(true);
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

        //for(int i = 0; i < 3; i++)
        //{
        //    SwitchNextModel();
        //}
    }

    /// <summary>
    /// 현재 상태를 playerState로 변경
    /// </summary>
    /// <param name="playerState">변경할 State</param>
    public void SwitchState(EPlayerState playerState)
    {
        if (!canInput)
        {
            if (playerModel.currentState != EPlayerState.Idle)
            {
                playerModel.currentState = EPlayerState.Idle;
                stateMachine.EnterState<PlayerIdleState>();
            }
            return;
        }
        playerModel.currentState = playerState;
        switch (playerState)
        {
            #region Idle, Run
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
            #endregion
            #region Evade
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
            #endregion
            #region Normal Attack
            case EPlayerState.NormalAttack:
                stateMachine.EnterState<PlayerNormalAttackState>(true);
                break;
            case EPlayerState.NormalAttackEnd:
                stateMachine.EnterState<PlayerNormalAttackEndState>();
                break;
            #endregion
            #region Ult
            case EPlayerState.AttackUltStart:
                if (currentUltPoint >= maxUltPoint)
                {
                    currentUltPoint -= maxUltPoint;
                    stateMachine.EnterState<PlayerUltStartState>();
                }
                break;
            case EPlayerState.AttackUlt:
                stateMachine.EnterState<PlayerUltState>();
                break;
            case EPlayerState.AttackUltEnd:
                stateMachine.EnterState<PlayerUltEndState>();
                break;
            #endregion
            #region E Skill
            case EPlayerState.AttackSkill:
                if (playerModel.playerStatus.CurrentSkillPoint >= 50f)
                {
                    playerModel.playerStatus.CurrentSkillPoint -= 50f;
                    stateMachine.EnterState<PlayerSkillState>();
                }
                break;
            case EPlayerState.AttackSkillEx:
                stateMachine.EnterState<PlayerSkillExtraState>();
                break;
            case EPlayerState.AttackSkillLoop:
                stateMachine.EnterState<PlayerSkilllLoopState>();
                break;
            case EPlayerState.AttackSkillEnd:
                stateMachine.EnterState<PlayerSkillEndState>();
                break;
            #endregion
            #region Rush
            case EPlayerState.AttackRush:
                stateMachine.EnterState<PlayerRushState>();
                break;
            case EPlayerState.AttackRushEnd:
                stateMachine.EnterState<PlayerRushEndState>();
                break;
            #endregion
            #region Switch
            case EPlayerState.SwitchInNormal:
                stateMachine.EnterState<PlayerSwitchInNormalState>();
                break;
                #endregion
        }
    }

    /// <summary>
    /// 다음 캐릭터로 교체
    /// </summary>
    public void SwitchNextModel()
    {
        // 스위치 쿨타임 확인
        if (switchTimer != switchCoolTime) return;
        switchTimer -= switchCoolTime;
        //사용하던 StateMachine 초기화
        stateMachine.Clear();

        playerModel.Exit();

        currentModelIndex++;
        if (currentModelIndex >= controllableModels.Count)
            currentModelIndex = 0;
        PlayerModel nextModel = controllableModels[currentModelIndex];
        nextModel.gameObject.SetActive(true);

        Vector3 prevPos = playerModel.transform.position;
        Quaternion prevRot = playerModel.transform.rotation;
        //Debug.Log($"Prev Pos : {playerModel.transform.position} Prev Rot : {playerModel.transform.rotation}");

        playerModel = nextModel;
        playerModel.Enter(prevPos, prevRot);
        SwitchState(EPlayerState.SwitchInNormal);

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
        #region 몬스터 타겟팅
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
        #endregion

        #region 이동 방향 입력
        inputMoveVec2 = playerInputSystem.Player.Move.ReadValue<Vector2>().normalized;
        #endregion

        #region 회피 쿨타임
        if (evadeTimer < evadeCoolTime)
        {
            evadeTimer += Time.deltaTime;
            //Debug.Log(evadeTimer);
            if (evadeTimer > evadeCoolTime)
                evadeTimer = evadeCoolTime;
        }
        #endregion

        #region 캐릭터 교체 쿨타임
        if (switchTimer < switchCoolTime)
        {
            switchTimer += Time.deltaTime;
            if (switchTimer > switchCoolTime)
                switchTimer = switchCoolTime;
        }
        #endregion

        #region CameraShake
        if (shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            if (shakeTimer <= 0f)
            {
                cinemachineFreeLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                cinemachineFreeLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
                cinemachineFreeLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
            }
        }
        #endregion

    }

    public void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void UnlockMouse()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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

    /// <summary>
    /// 인식된 몬스터들을 가까운 거리순으로 정렬하는 함수
    /// </summary>
    /// <returns></returns>
    public GameObject[] SortEnemyByDist()
    {
        Vector3 referencePosition = playerModel.transform.position;

        GameObject[] sortedGameObjects = enemyList
            .OrderBy(go => Vector3.Distance(go.transform.position, referencePosition)) // 각 GameObject의 위치와 기준 위치 사이의 거리 계산
            .ToArray(); // 정렬된 결과를 배열로 변환

        return sortedGameObjects; // 정렬된 GameObject 배열 반환
    }

    public void ShakeCamera(float intensity, float time)
    {
        cinemachineFreeLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        cinemachineFreeLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        cinemachineFreeLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;


        shakeTimer = time;
    }

    public void StopShakeCamera()
    {
        cinemachineFreeLook.GetRig(0).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cinemachineFreeLook.GetRig(1).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        cinemachineFreeLook.GetRig(2).GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        shakeTimer = 0f;
    }

    public void LockCamera()
    {
        cinemachineFreeLook.m_XAxis.m_InputAxisName = "";
        cinemachineFreeLook.m_YAxis.m_InputAxisName = "";
    }

    public void UnlockCamera()
    {
        cinemachineFreeLook.m_XAxis.m_InputAxisName = "Mouse X";
        cinemachineFreeLook.m_YAxis.m_InputAxisName = "Mouse Y";
    }

    public void SetSpawnPoint(Vector3 spawnPoint)
    {
        this.transform.position = spawnPoint;
        playerModel.transform.position = spawnPoint;
    }
}