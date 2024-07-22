using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        switch (playerModel.currentState)
        {
            case EPlayerState.EvadeFrontEnd:
                playerController.PlayAnimation("Evade_Front_End");
                break;
            case EPlayerState.EvadeBackEnd:
                playerController.PlayAnimation("Evade_Back_End");
                break;
        }
    }

    public override void Update()
    {
        base.Update();
        //�ñر�
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        //��Ÿ or ����
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            switch (playerModel.currentState)
            {
                case EPlayerState.EvadeFrontEnd:
                    playerController.SwitchState(EPlayerState.NormalAttack);
                    return;
                case EPlayerState.EvadeBackEnd:
                    playerController.SwitchState(EPlayerState.AttackRush);
                    return;
            }
        }
        //��ų
        if (playerController.playerInputSystem.Player.Skill.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackSkill);
            return;
        }
        //�̵�
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(EPlayerState.Walk);
            return;
            //playerController.SwitchState(EPlayerState.RunStart);
        }
        //ȸ��
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            playerController.SwitchState(EPlayerState.EvadeBack);
            return;
        }
        //�ִϸ��̼� ����
        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.Idle);
            return;
        }
    }
}
