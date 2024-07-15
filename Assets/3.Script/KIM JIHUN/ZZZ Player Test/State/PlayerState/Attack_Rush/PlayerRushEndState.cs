using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRushEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("Attack_Rush_End");
    }
    public override void Update()
    {
        base.Update();
        //평타
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            //Debug.Log($"Combo : {playerModel.currentNormalAttakIndex}");
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }
        //이동
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(EPlayerState.Walk);
            return;
            //playerController.SwitchState(EPlayerState.RunStart);
        }
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            //Debug.Log("Idle -> Evade Back");
            playerController.SwitchState(EPlayerState.EvadeBack);
            return;
        }
        //스킬
        if (playerController.playerInputSystem.Player.Skill.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackSkill);
            return;
        }
        //궁
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        //애니메이션 종료
        if(IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.Idle);
        }
    }
}
