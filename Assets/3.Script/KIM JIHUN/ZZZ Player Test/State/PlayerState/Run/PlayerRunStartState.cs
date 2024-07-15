using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunStartState : PlayerStateBase
{
    private Camera mainCamera;
    public override void Enter()
    {
        base.Enter();
        mainCamera = Camera.main;

        playerController.PlayAnimation("Run_Start");
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
        //회피
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            if (playerController.evadeTimer >= playerController.evadeCoolTime)
            {
                playerController.SwitchState(EPlayerState.EvadeBack);
                return;
            }
        }
        //이동
        if (playerController.inputMoveVec2 == Vector2.zero)
        {
            playerController.SwitchState(EPlayerState.RunEnd);
            return;
        }
        else
        {
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
        //애니메이션 종료
        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.Run);
        }

    }
}
