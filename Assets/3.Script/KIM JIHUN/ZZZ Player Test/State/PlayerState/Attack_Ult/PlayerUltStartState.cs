using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerUltStartState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        playerModel.LookEnemy();
        CameraManager.INSTANCE.cmBrain.m_DefaultBlend =
            new CinemachineBlendDefinition(CinemachineBlendDefinition.Style.Cut, 0f);
        CameraManager.INSTANCE.freeLookCamera.SetActive(false);
        playerModel.skillUltStartShot.SetActive(true);


        playerController.PlayAnimation("Attack_Ult_Start", 0f);
        if (playerController.controllableModels[playerController.currentModelIndex].eCharacter == ECharacter.Anbi)
        {
            SoundManager.Instance.PlayEffect($"AnbiAttack_Skill_Q");
        }
        else if (playerController.controllableModels[playerController.currentModelIndex].eCharacter == ECharacter.Longinus)
        {
            SoundManager.Instance.PlayEffect($"LonginusAttack_Skill_Q");
        }
        else if (playerController.controllableModels[playerController.currentModelIndex].eCharacter == ECharacter.Corin)
        {
            SoundManager.Instance.PlayEffect($"CorinAttack_Skill_Q");
        }
    }

    public override void Update()
    {
        base.Update();
        //애니메이션 종료
        if (IsAnimationEnd())
            playerController.SwitchState(EPlayerState.AttackUlt);
    }
}
