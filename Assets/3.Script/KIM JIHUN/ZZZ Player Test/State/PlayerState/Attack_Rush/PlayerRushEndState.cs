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
        //��Ÿ
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            //Debug.Log($"Combo : {playerModel.currentNormalAttakIndex}");
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }
        //�̵�
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
        //��ų
        if (playerController.playerInputSystem.Player.Skill.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackSkill);
            return;
        }
        //��
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        //�ִϸ��̼� ����
        if(IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.Idle);
        }
    }
}
