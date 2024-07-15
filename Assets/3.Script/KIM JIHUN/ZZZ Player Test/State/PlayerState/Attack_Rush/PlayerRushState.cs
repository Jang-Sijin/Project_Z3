using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRushState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("Attack_Rush");
    }
    public override void Update()
    {
        base.Update();
        //회피
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            //Debug.Log("Idle -> Evade Back");
            playerController.SwitchState(EPlayerState.EvadeBack);
            return;
        }
        //궁
        if (playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        //애니메이션 종료
        if (IsAnimationEnd())
        {
            playerController.SwitchState(EPlayerState.AttackRushEnd);
        }
    }
}
