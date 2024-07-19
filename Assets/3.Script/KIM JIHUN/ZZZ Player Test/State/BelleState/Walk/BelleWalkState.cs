using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelleWalkState : BelleStateBase
{
    private Camera mainCamera;

    public override void Enter()
    {
        base.Enter();
        mainCamera = Camera.main;
        belleController.PlayAnimation("Walk");
    }

    public override void Update()
    {
        base.Update();
        //¿Ãµø
        if (belleController.inputMoveVec2 == Vector2.zero)
        {
            //belleController.SwitchState(EBelleState.RunEnd);
            belleController.SwitchState(EBelleState.WalkEnd);
            return;
        }
        else
        {

            if (!belleController.toggleWalk)
            {
                belleController.SwitchState(EBelleState.Run);
                return;
            }
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
    }
}
