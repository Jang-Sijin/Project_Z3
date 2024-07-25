using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeState : PlayerStateBase
{

    public override void Enter()
    {
        base.Enter();
        switch (playerModel.currentState)
        {
            case EPlayerState.Idle:
            case EPlayerState.RunEnd:
            case EPlayerState.EvadeBackEnd:
            case EPlayerState.EvadeBack:
            case EPlayerState.NormalAttackEnd:
                //Debug.Log("Play Animation Evade Back");
                playerController.PlayAnimation("Evade_Back");
                break;
            case EPlayerState.EvadeFront:
                //Debug.Log("Play Animation Evade Front");
                playerController.PlayAnimation("Evade_Front");
                break;
        }
    }

    public override void Update()
    {
        base.Update();
        //평타 or 러쉬
        if (playerController.playerInputSystem.Player.Fire.triggered && GetNormalizedTime() >= 0.4f)
        {
            switch (playerModel.currentState)
            {
                case EPlayerState.EvadeFront:
                    playerController.SwitchState(EPlayerState.NormalAttack);
                    return;
                case EPlayerState.EvadeBack:
                    playerController.SwitchState(EPlayerState.AttackRush);
                    return;
            }
        }
        //궁
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        //스킬
        if (playerController.playerInputSystem.Player.Skill.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackSkill);
            return;
        }
        //애니메이션 종료
        if (IsAnimationEnd())
        {
            switch(playerModel.currentState)
            {
                case EPlayerState.EvadeFront:
                    if(playerController.playerInputSystem.Player.Evade.IsPressed())
                    {
                        playerController.SwitchState(EPlayerState.Run);
                        return;
                    }
                    playerController.SwitchState(EPlayerState.EvadeFrontEnd);
                    break;
                case EPlayerState.EvadeBack:
                    playerController.SwitchState(EPlayerState.EvadeBackEnd);
                    break;
            }
         }
    }
}
