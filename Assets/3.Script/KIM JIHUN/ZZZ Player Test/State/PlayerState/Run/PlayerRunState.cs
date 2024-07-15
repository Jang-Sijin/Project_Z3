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
        switch (playerModel.currentState)
        {
            case EPlayerState.Walk:
                switch (playerModel.foot)
                {
                    case EModelFoot.Right:
                        playerController.PlayAnimation("Walk", 0.25f, 0.6f);
                        playerModel.foot = EModelFoot.Right;
                        break;
                    case EModelFoot.Left:
                        playerController.PlayAnimation("Walk", 0.25f, 0f);
                        playerModel.foot = EModelFoot.Left;
                        break;
                }
                break;
            case EPlayerState.Run:
                playerController.PlayAnimation("Run");
                break;
        }

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
        //평타
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            playerController.SwitchState(EPlayerState.NormalAttack);
            return;
        }
        //스킬
        if (playerController.playerInputSystem.Player.Skill.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackSkill);
            return;
        }
        //회피
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            playerController.SwitchState(EPlayerState.EvadeFront);
            return;
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
        if (playerModel.currentState == EPlayerState.Walk && statePlayingTime > 3f)
        {
            playerController.SwitchState(EPlayerState.Run);
            return;
        }
    }
}
