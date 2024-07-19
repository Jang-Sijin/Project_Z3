using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BelleRunStartState : BelleStateBase
{

    private Camera mainCamera;
    public override void Enter()
    {
        mainCamera = Camera.main;
        base.Enter();

        belleController.PlayAnimation("Run_Start");
    }

    public override void Update()
    {
        base.Update();
        //이동 멈춤
        if(belleController.inputMoveVec2 == Vector2.zero)
        {
            belleController.SwitchState(EBelleState.RunEnd);
            return;
        }

        if (belleController.toggleWalk)
        {
            belleController.SwitchState(EBelleState.Walk);
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
        //이동 && 애니메이션 종료
        if (belleController.inputMoveVec2 != Vector2.zero && GetNormalizedTime() >= 0.8f)
        {
            belleController.SwitchState(EBelleState.Run);
            return;
        }
    }
}
