using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        switch (playerModel.currentState)
        {
            case EPlayerState.Idle:
                playerController.PlayAnimation("Idle");
                break;
            case EPlayerState.IdleAFK:
                playerController.PlayAnimation("IdleAFK");
                break;
        }
    }

    public override void Update()
    {
        base.Update();
        //궁극기
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        //평타
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            //Debug.Log($"Combo : {playerModel.currentNormalAttakIndex}");
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }
        //스킬
        if(playerController.playerInputSystem.Player.Skill.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackSkill);
            return;
        }
        //회피
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            //Debug.Log("Idle -> Evade Back");
            playerController.SwitchState(EPlayerState.EvadeBack);
            return;
        }
        //이동
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(EPlayerState.Walk);
            return;
            //playerController.SwitchState(EPlayerState.RunStart);
        }

        //유후 모션
        switch (playerModel.currentState)
        {
            case EPlayerState.Idle:
                if (playerModel.currentState == EPlayerState.Idle && statePlayingTime > 4)
                {
                    playerController.SwitchState(EPlayerState.IdleAFK);
                    Debug.Log("Idle_AFK");
                }
                break;
            case EPlayerState.IdleAFK:
                if (IsAnimationEnd())
                    playerController.SwitchState(EPlayerState.Idle);
                break;
        }
    }
}
