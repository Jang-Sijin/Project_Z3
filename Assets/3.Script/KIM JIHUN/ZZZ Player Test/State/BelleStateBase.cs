using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EBelleState
{
    Idle,
    WalkStart,
    Walk,
    WalkEnd,
    RunStart,
    Run,
    RunEnd
}

public class BelleStateBase : StateBase
{
    protected BelleController belleController;
    protected BelleModel belleModel;
    private AnimatorStateInfo stateInfo;
    protected float statePlayingTime = 0;
    public override void Init(IStateMachineOwner owner)
    {
        belleController = (BelleController)owner;
        belleModel = belleController.belleModel;
    }

    public override void Enter()
    {
        statePlayingTime = 0;
    }

    public override void Exit()
    {
    }
    public override void Update()
    {
        belleModel.characterController.Move(new Vector3(0, belleModel.gravity * Time.deltaTime, 0));
        statePlayingTime += Time.deltaTime;

        // ESC Pause ��� �ʿ�
    }

    public override void FixedUpdate()
    {
    }


    public override void LateUpdate()
    {
    }

    public override void UnInit()
    {
    }

    /// <summary>
    /// �ִϸ��̼��� ����ƴ��� Ȯ��
    /// </summary>
    /// <returns></returns>
    public bool IsAnimationEnd()
    {
        stateInfo = belleModel.animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1.0f && !belleModel.animator.IsInTransition(0);
    }


    /// <summary>
    /// �ִϸ��̼� ���� ���൵ ��ȯ
    /// </summary>
    /// <returns></returns>
    public float GetNormalizedTime()
    {
        stateInfo = belleModel.animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime;
    }
}
