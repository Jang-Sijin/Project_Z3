using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelleRunState : BelleStateBase
{
    private Camera mainCamera;

    public override void Enter()
    {
        base.Enter();

        mainCamera = Camera.main;
        switch (belleModel.currentState)
        {
            case EBelleState.Walk:
                switch (belleModel.foot)
                {
                    case EModelFoot.Right:
                        belleController.PlayAnimation("Walk", 0.25f, 0.6f);
                        belleModel.foot = EModelFoot.Right;
                        break;
                    case EModelFoot.Left:
                        belleController.PlayAnimation("Walk", 0.25f, 0f);
                        belleModel.foot = EModelFoot.Left;
                        break;
                }
                break;
            case EBelleState.Run:
                belleController.PlayAnimation("Run");
                break;
        }
    }

    public override void Update()
    {
        base.Update();
        //이동
        if (belleController.inputMoveVec2 == Vector2.zero)
        {
            //belleController.SwitchState(EBelleState.RunEnd);
            belleController.SwitchState(EBelleState.Idle);
            return;
        }
        else
        {

            Vector3 inputMoveVec3 = new Vector3(belleController.inputMoveVec2.x, 0, belleController.inputMoveVec2.y);

            float cameraAxisY = mainCamera.transform.rotation.eulerAngles.y;

            Vector3 targetDir = Quaternion.Euler(0, cameraAxisY, 0) * inputMoveVec3;

            Quaternion targetQua = Quaternion.LookRotation(targetDir);
            belleModel.transform.rotation = Quaternion.Slerp(
                                                        belleModel.transform.rotation,
                                                        targetQua,
                                                        Time.deltaTime * belleController.rotationSpeed
                                                        );
        }

        //애니메이션 종료
        if (belleModel.currentState == EBelleState.Walk && statePlayingTime > 3f)
        {
            belleController.SwitchState(EBelleState.Run);
            return;
        }
    }
}

