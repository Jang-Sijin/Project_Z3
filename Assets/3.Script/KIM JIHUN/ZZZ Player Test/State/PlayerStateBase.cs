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

    SwitchIn
}

public class PlayerStateBase : StateBase
{
    protected PlayerController playerController;
    protected PlayerModel playerModel;
    protected AnimatorStateInfo stateInfo;
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

    public override void FIxedUpdate()
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
        stateInfo = playerModel.animator.GetCurrentAnimatorStateInfo(0);
        statePlayingTime += Time.deltaTime;

        //Ä³¸¯ÅÍ Switch
        if(playerController.playerInputSystem.Player.Switch.triggered)
        {
            Debug.Log($"player model : {playerModel.gameObject.name} pos : {playerModel.transform.position}");
            playerController.SwitchNextModel();
        }
    }

    public bool IsAnimationEnd()
    {
        return stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0);
    }
}
