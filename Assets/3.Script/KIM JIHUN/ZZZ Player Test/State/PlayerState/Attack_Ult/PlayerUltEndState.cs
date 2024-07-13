using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        //UltState에 카메라 컷신이 있다면
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

        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            playerController.SwitchState(EPlayerState.EvadeBack);
            return;
        }
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(EPlayerState.Run);
            return;
            //playerController.SwitchState(EPlayerState.RunStart);
        }
        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.Idle);
        }
    }
}
