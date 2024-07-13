using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTurnBackState : PlayerStateBase
{
    private Camera mainCamera;
    public override void Enter()
    {
        base.Enter();
        mainCamera = Camera.main;

        playerController.PlayAnimation("TurnBack", 0.1f);
    }

    public override void Update()
    {
        base.Update();
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        if (stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
        {
            playerController.SwitchState(EPlayerState.Run);
            return;
            //playerController.SwitchState(EPlayerState.RunStart);
        }

        Vector3 inputMoveVec3 = new Vector3(playerController.inputMoveVec2.x, 0, playerController.inputMoveVec2.y);

        float cameraAxisY = mainCamera.transform.rotation.eulerAngles.y;

        Vector3 targetDir = Quaternion.Euler(0, cameraAxisY, 0) * inputMoveVec3;
        Quaternion targetQua = Quaternion.LookRotation(targetDir);


        playerModel.transform.rotation = Quaternion.Slerp(
                                                    playerModel.transform.rotation,
                                                    targetQua,
                                                    Time.deltaTime * playerController.rotationSpeed
                                                    );
    }
}
