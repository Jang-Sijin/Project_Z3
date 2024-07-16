using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUltState : PlayerStateBase
{
    private Camera mainCamera;
    public override void Enter()
    {
        base.Enter();
        playerModel.LookEnemy();

        mainCamera = Camera.main;
        //UltState에 카메라 컷신이 있다면
        if (playerModel.skillUltShot != null)
        {
            playerModel.skillUltStartShot.SetActive(false);
            playerModel.skillUltShot.SetActive(true);
        }
        else
        {
            playerModel.skillUltStartShot.SetActive(false);
            CameraManager.INSTANCE.cmBrain.m_DefaultBlend =
                new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 2f);
            CameraManager.INSTANCE.freeLookCamera.SetActive(true);
            CameraManager.INSTANCE.ResetFreeLookCamera();
        }

        playerController.PlayAnimation("Attack_Ult", 0f);
    }
    public override void Update()
    {
        base.Update();

        //애니메이션 종료
        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.AttackUltEnd);
            return;
        }
    }
}
