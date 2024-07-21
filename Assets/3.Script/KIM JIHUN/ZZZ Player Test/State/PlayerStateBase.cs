using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EPlayerState
{
    Idle,
    IdleAFK,
    Walk,
    Run,
    RunStart,
    RunEnd,
    TurnBack,
    EvadeFront,
    EvadeFrontEnd,
    EvadeBack,
    EvadeBackEnd,

    NormalAttack,
    NormalAttakEnd,

    AttackUltStart,
    AttackUlt,
    AttackUltEnd,

    AttackSkill,
    AttackSkillLoop,
    AttackSkillEx,
    AttackSkillEnd,

    AttackRush,
    AttackRushEnd,

    SwitchInNormal
}

public class PlayerStateBase : StateBase
{
    protected PlayerController playerController;
    protected PlayerModel playerModel;
    private AnimatorStateInfo stateInfo;
    protected float statePlayingTime = 0;

    public override void Init(IStateMachineOwner owner)
    {
        playerController = (PlayerController)owner;
        playerModel = playerController.playerModel;
    }
    public override void Enter()
    {
        statePlayingTime = 0;
    }

    public override void Exit()
    {
    }

    public override void FixedUpdate()
    {
    }


    public override void LateUpdate()
    {
    }

    public override void UnInit()
    {
    }

    public override void Update()
    {
        playerModel.characterController.Move(new Vector3(0, playerModel.gravity * Time.deltaTime, 0));
        statePlayingTime += Time.deltaTime;


        //ĳ���� Switch
        if (playerModel.currentState != EPlayerState.AttackUltStart
            && playerModel.currentState != EPlayerState.AttackUlt
            && playerController.playerInputSystem.Player.Switch.triggered)
        {
            //Debug.Log($"player model : {playerModel.gameObject.name} pos : {playerModel.transform.position}");
            playerController.SwitchNextModel();
        }

    }

    /// <summary>
    /// �ִϸ��̼��� ����ƴ��� Ȯ��
    /// </summary>
    /// <returns></returns>
    public bool IsAnimationEnd()
    {
        stateInfo = playerModel.animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0);
    }

    /// <summary>
    /// �ִϸ��̼� ���� ���൵ ��ȯ
    /// </summary>
    /// <returns></returns>
    public float GetNormalizedTime()
    {
        stateInfo = playerModel.animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime;
    }
}
