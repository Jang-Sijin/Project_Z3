using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerStateBase
{

    private Camera mainCamera;

    public override void Enter()
    {
        base.Enter();

        mainCamera = Camera.main;

        switch (playerModel.foot)
        {
            case EModelFoot.Right:
                playerController.PlayAnimation("Run", 0.25f, 0.5f);
                playerModel.foot = EModelFoot.Right;
                break;
            case EModelFoot.Left:
                playerController.PlayAnimation("Run", 0.25f, 0f);
                playerModel.foot = EModelFoot.Left;
                break;
        }
    }
    public override void Update()
    {
        base.Update();

        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            playerController.SwitchState(EPlayerState.EvadeFront);
            return;
        }

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

            //float angles = Mathf.Abs(targetQua.eulerAngles.y - playerModel.transform.eulerAngles.y);
             //Debug.Log("target : " + targetQua.eulerAngles.y);
             //Debug.Log("Player : " + playerModel.transform.eulerAngles.y);


            //if (angles > 177.5f && angles < 182.5f)
            //{
            //    //playerController.SwitchState(EPlayerState.TurnBack);
            //}
            //else
            //{
            playerModel.transform.rotation = Quaternion.Slerp(
                                                        playerModel.transform.rotation,
                                                        targetQua,
                                                        Time.deltaTime * playerController.rotationSpeed
                                                        );
            //}
        }
    }
}
