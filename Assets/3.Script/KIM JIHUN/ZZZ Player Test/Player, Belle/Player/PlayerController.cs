using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Cinemachine;
using Unity.VisualScripting.Dependencies.Sqlite;
using Unity.VisualScripting;
using DG.Tweening;
using TMPro;

public class PlayerController : SingleMonoBase<PlayerController>, IStateMachineOwner
{
    [HideInInspector] public PlayerInputSystem playerInputSystem; // Input �ý���
    public PlayerModel playerModel; //���� ��Ʈ������ �÷��̾� ��

    private StateMachine stateMachine;

    public PlayerConfig playerConfig; //���� �Ҵ�Ǿ� �ִ� Player Config. �ִ� 3���� ĳ���Ͱ� ����

    public List<PlayerModel> controllableModels; //playerModel ������ ����� �� �ִ� PlayerModel �迭
    public int currentModelIndex = 0; //���� ��Ʈ������ �� �ε���

    public Vector2 inputMoveVec2; // WASD Ű����� �Է�
    public float rotationSpeed = 8f;

    #region Player Active Cool Down
    [HideInInspector] public float evadeTimer { get; private set; } // ȸ�� Ÿ�̸�
    [HideInInspector] private float evadeCoolTime; // ȸ�� ��Ÿ��
    [HideInInspector] public float switchTimer { get; private set; } // ĳ���� ��ü Ÿ�̸�
    [HideInInspector] private float switchCoolTime; // ĳ���� ��ü ��Ÿ��
    private float maxUltPoint = 100f;
    private float defaultUltPoint = 4f;
    private float currentUltPoint = 0;

    public float MaxUltPoint => maxUltPoint;
    public float DefaultUltPoint => defaultUltPoint;

    public float CurrentUltPoint
    {
        get { return currentUltPoint; }
        set { currentUltPoint = Mathf.Clamp(value, 0, maxUltPoint); }
    }

    private bool canInput = true; // ��ȭ, �ƽſ� ���� bool��. ĳ���͸� ������ �� �ִ���
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

    [SerializeField] private CinemachineFreeLook cinemachineFreeLook; //ī�޶� ����ũ�� ���� �ó׸ӽ�
    private float shakeTimer; // ����ũ Ÿ�̸�

    public GameObject damageFontCanvasPrefab;

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
        UIManager.Instance.OpenIngameUI();

        //for(int i = 0; i < 3; i++)
        //{
        //    SwitchNextModel();
        //}
    }

    /// <summary>
    /// ���� ���¸� playerState�� ����
    /// </summary>
    /// <param name="playerState">������ State</param>
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
                UIManager.Instance.InGameUI.Pressbtn(KeyCode.Mouse1);

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
                    UIManager.Instance.InGameUI.Pressbtn(KeyCode.Q);
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
                    UIManager.Instance.InGameUI.Pressbtn(KeyCode.E);
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
                UIManager.Instance.InGameUI.Pressbtn(KeyCode.Space);
                stateMachine.EnterState<PlayerSwitchInNormalState>();
                break;
                #endregion
        }
    }

    /// <summary>
    /// ���� ĳ���ͷ� ��ü
    /// </summary>
    public void SwitchNextModel(bool isDead = false)
    {
        // ����ġ ��Ÿ�� Ȯ��
        if (switchTimer != switchCoolTime) return;
        switchTimer -= switchCoolTime;
        //����ϴ� StateMachine �ʱ�ȭ
        stateMachine.Clear();

        if (!isDead)
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
        UIManager.Instance.InGameUI.ChangeChar();

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
        //Debug.Log(controllableModels[currentModelIndex].eCharacter);
        if (playerInputSystem.Player.Escape.triggered)
        {
            if (UIManager.Instance != null)
            {
                UIManager.Instance.IngamePauseUI.OpenIngamePauseUI();
            }
        }

        #region ���� Ÿ����
        if (enemyList.Count != 0)
        {
            //���� ����� ���� ������ -> ����ĳ��Ʈ�� ������ ��ü ã��
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

        #region �̵� ���� �Է�
        inputMoveVec2 = playerInputSystem.Player.Move.ReadValue<Vector2>().normalized;
        #endregion

        #region ȸ�� ��Ÿ��
        if (evadeTimer < evadeCoolTime)
        {
            evadeTimer += Time.deltaTime;
            //Debug.Log(evadeTimer);
            if (evadeTimer > evadeCoolTime)
                evadeTimer = evadeCoolTime;
        }
        #endregion

        #region ĳ���� ��ü ��Ÿ��
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
        var enemyList = SortEnemyByDist();

        if (enemyList == null || enemyList.Length == 0)
            return null;

        foreach (var enemy in SortEnemyByDist())
        {
            Vector3 directionToEnemy = (enemy.transform.position - playerPosition).normalized;
            RaycastHit hit;

            // �÷��̾� ��ġ���� �� �������� ����ĳ��Ʈ
            if (Physics.Raycast(playerPosition, directionToEnemy, out hit))
            {
                // ��Ʈ�� ��ü�� ���̶�� �ش� ���� ��ȯ
                if (hit.collider.gameObject == enemy)
                {
                    return enemy;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// �νĵ� ���͵��� ����� �Ÿ������� �����ϴ� �Լ�
    /// </summary>
    /// <returns></returns>
    public GameObject[] SortEnemyByDist()
    {
        // null�̰ų� �ı��� GameObject�� �����մϴ�.
        enemyList = enemyList.Where(go => go != null && go.transform != null).ToList();

        if (enemyList == null || enemyList.Count == 0)
        {
            return null;
        }

        Vector3 referencePosition = playerModel.transform.position;

        GameObject[] sortedGameObjects = enemyList
            .OrderBy(go => Vector3.Distance(go.transform.position, referencePosition)) // �� GameObject�� ��ġ�� ���� ��ġ ������ �Ÿ� ���
            .ToArray(); // ���ĵ� ����� �迭�� ��ȯ

        return sortedGameObjects; // ���ĵ� GameObject �迭 ��ȯ
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

    public void TakeDamage(float monsterDamage, Vector3 monsterWeaponPostion)
    {
        if (playerModel.playerStatus.CurrentHealth > 0)
        {
            // �÷��̾� ���� ����(Hit) ���·� ����
            //SwitchState(EMonsterState.Hit);

            playerModel.playerStatus.CurrentHealth -= monsterDamage;
            // EnemyUIController.RefreshHealth(_currentHealth, MaxHealth);

            ShowDamageFont(monsterDamage, monsterWeaponPostion);
            // enemyUIController.RefreshHealth(monsterModel.monsterStatus.CurrentHealth, monsterModel.monsterStatus.MaxHealth); // �÷��̾� HP Bar ����
            return;
        }
        else
        {
            Debug.Log($"{gameObject.name}: �÷��̾� ���");
            // #�÷��̾� ��� ó�� �߰� �ʿ�
            return;
        }
    }

    // ����� ��Ʈ�� ����մϴ�.
    private void ShowDamageFont(float damage, Vector3 hitPosition)
    {
        // ������ ��Ʈ ������ �ν��Ͻ�ȭ
        GameObject damageFont = Instantiate(damageFontCanvasPrefab, hitPosition, Quaternion.identity);
        // Canvas�� Text ��ҿ� �����Ͽ� ������ ���� ����
        TextMeshProUGUI textMesh = damageFont.GetComponentInChildren<TextMeshProUGUI>();
        if (textMesh != null)
        {
            textMesh.text = damage.ToString();
        }

        // ��Ʈ ����: ����
        textMesh.color = Color.red;

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

        // ������ ����
        sequence.Join(damageFontRectTransform.GetComponent<TextMeshProUGUI>().DOFade(0, 0.3f)
            .SetEase(Ease.InOutQuad));

        sequence.Play().OnComplete(() => Destroy(startPosition.transform.gameObject, 1.0f)); // ���� �ð� �Ŀ� ������ ��Ʈ ������Ʈ�� ����
    }
}