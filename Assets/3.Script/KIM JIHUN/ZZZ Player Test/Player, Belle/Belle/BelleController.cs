using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelleController : SingleMonoBase<BelleController>, IStateMachineOwner
{
    [HideInInspector] public PlayerInputSystem playerInputSystem;

    public BelleModel belleModel;

    private StateMachine stateMachine;

    public Vector2 inputMoveVec2;
    public float rotationSpeed = 8f;

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
        belleModel.currentState = belleState;

        switch (belleState)
        {
            case EBelleState.Idle:
                stateMachine.EnterState<BelleIdleState>();
                break;
            case EBelleState.Walk:
                stateMachine.EnterState<BelleWalkState>();
                break;
            case EBelleState.Run:
                stateMachine.EnterState<BelleRunState>(true);
                break;
            case EBelleState.RunEnd:
                stateMachine.EnterState<BelleRunEndState>();
                break;
        }
    }

    private void LockMouse()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
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
        inputMoveVec2 = playerInputSystem.Player.Move.ReadValue<Vector2>().normalized;
    }

    private void OnEnable()
    {
        playerInputSystem.Enable();
    }

    private void OnDisable()
    {
        playerInputSystem.Disable();
    }
}