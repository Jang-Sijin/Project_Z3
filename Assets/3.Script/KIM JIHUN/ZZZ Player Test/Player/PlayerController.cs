using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : SingleMonoBase<PlayerController>, IStateMachineOwner
{
    [HideInInspector] public PlayerInputSystem playerInputSystem;
    public PlayerModel playerModel;

    private StateMachine stateMachine;

    public Vector2 inputMoveVec2;
    public float rotationSpeed = 8f;
    public float evadeTimer { get; private set; }
    public float evadeCoolTime { get; private set; }

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new StateMachine(this);

        playerInputSystem = new PlayerInputSystem();
        evadeCoolTime = 0.5f;
        evadeTimer = evadeCoolTime;
    }

    private void Start()
    {
        LockMouse();
        SwitchState(EPlayerState.Idle);
    }

    public void SwitchState(EPlayerState playerState)
    {
        playerModel.currentState = playerState;
        switch (playerState)
        {
            case EPlayerState.Idle:
            case EPlayerState.IdleAFK:
                stateMachine.EnterState<PlayerIdleState>(true);
                break;
            case EPlayerState.Run:
                stateMachine.EnterState<PlayerRunState>();
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
            case EPlayerState.EvadeEnd:
                stateMachine.EnterState<PlayerEvadeEndState>();
                break;
            case EPlayerState.NormalAttack:
                stateMachine.EnterState<PlayerNormalAttackState>();
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
        }
        playerModel.state = playerState;
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
}
