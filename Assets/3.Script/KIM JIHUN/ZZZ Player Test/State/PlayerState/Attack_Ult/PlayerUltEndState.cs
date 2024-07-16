using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        //UltState�� ī�޶� �ƽ��� �ִٸ�
        if (playerModel.skillUltShot != null)
        {
            playerModel.skillUltShot.SetActive(false);
            CameraManager.INSTANCE.cmBrain.m_DefaultBlend =
                new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 1f);
            CameraManager.INSTANCE.freeLookCamera.SetActive(true);
            CameraManager.INSTANCE.ResetFreeLookCamera();
        }
        else
        {
            CameraManager.INSTANCE.cmBrain.m_DefaultBlend =
                new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.EaseInOut, 1f);
        }


        playerController.PlayAnimation("Attack_Ult_End", 0f);
    }

    public override void Update()
    {
        base.Update();
        //��Ÿ
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }
        //ȸ��
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            playerController.SwitchState(EPlayerState.EvadeBack);
            return;
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
        //�ִϸ��̼� ����
        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.Idle);
        }
    }
}
