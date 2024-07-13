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

        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.AttackUltEnd);
            return;
        }

        //Vector3 inputMoveVec3 = new Vector3(playerController.inputMoveVec2.x, 0, playerController.inputMoveVec2.y);
        //
        //float cameraAxisY = mainCamera.transform.rotation.eulerAngles.y;
        //
        //Vector3 targetDir = Quaternion.Euler(0, cameraAxisY, 0) * inputMoveVec3;
        //
        //Quaternion targetQua = Quaternion.LookRotation(targetDir);

        //playerModel.transform.rotation = Quaternion.Slerp(
        //                                            playerModel.transform.rotation,
        //                                            targetQua,
        //                                            Time.deltaTime * playerController.rotationSpeed
        //                                            );
    }
}
