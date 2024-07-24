using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class BelleController : SingleMonoBase<BelleController>, IStateMachineOwner
{
    [HideInInspector] public PlayerInputSystem playerInputSystem;

    public BelleModel belleModel;

    private StateMachine stateMachine;

    public Vector2 inputMoveVec2;
    public float rotationSpeed = 8f;

    public bool toggleWalk = false;
    [SerializeField]private CinemachineFreeLook cinemachineFreeLook;

    private bool canInput = true; // 대화, 컷신에 사용될 bool값. 캐릭터를 조종할 수 있는지
    public bool CanInput
    {
        get { return canInput; }
        set
        {
            canInput = value;
            //SwitchState(EBelleState.Idle);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);

        playerInputSystem = new PlayerInputSystem();
    }

    private void Start()
    {
        LockMouse();
        SwitchState(EBelleState.Idle);
    }

    public void SwitchState(EBelleState belleState)
    {
        if (!canInput)
        {
            if (belleModel.currentState != EBelleState.Idle)
            {
                belleModel.currentState = EBelleState.Idle;
                stateMachine.EnterState<BelleIdleState>();
            }
            return;
        }
        belleModel.currentState = belleState;
        belleModel.currentState = belleState;

        switch (belleState)
        {
            case EBelleState.Idle:
                stateMachine.EnterState<BelleIdleState>();
                break;
            case EBelleState.Run:
                stateMachine.EnterState<BelleRunState>(true);
                break;
            case EBelleState.RunEnd:
                stateMachine.EnterState<BelleRunEndState>();
                break;
            case EBelleState.RunStart:
                stateMachine.EnterState<BelleRunStartState>();
                break;
            case EBelleState.WalkStart:
                stateMachine.EnterState<BelleWalkStartState>();
                break;
            case EBelleState.Walk:
                stateMachine.EnterState<BelleWalkState>();
                break;
            case EBelleState.WalkEnd:
                stateMachine.EnterState<BelleWalkEndState>();
                break;
        }
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

    public void PlayAnimation(string animationName, float fixedTransitionDuration = 0.25f)
    {
        belleModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration);
    }

    public void PlayAnimation(string animationName, float fixedTransitionDuration, float fixedTimeOffset)
    {
        belleModel.animator.CrossFadeInFixedTime(animationName, fixedTransitionDuration, 0, fixedTimeOffset);
    }

    private void Update()
    {
        if (playerInputSystem.Player.Escape.triggered)
        {
            if (Build_UIManager.Instance != null)
            {
                Build_UIManager.Instance.OptionUIOpenClose();
            }
        }
        inputMoveVec2 = playerInputSystem.Player.Move.ReadValue<Vector2>().normalized;
        if (playerInputSystem.Player.Evade.triggered)
            toggleWalk = !toggleWalk;

    }

    private void OnEnable()
    {
        playerInputSystem.Enable();
    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
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
        belleModel.transform.position = spawnPoint;
    }
}