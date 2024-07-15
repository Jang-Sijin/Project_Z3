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
            case EPlayerState.NormalAttakEnd:
                Debug.Log("Play Animation Evade Back");
                playerController.PlayAnimation("Evade_Back");
                break;
            case EPlayerState.EvadeFront:
                Debug.Log("Play Animation Evade Front");
                playerController.PlayAnimation("Evade_Front");
                break;
            //case EPlayerState.Evade:
            //    Debug.Log("Enter evade to evade");
            //    if (playerController.inputMoveVec2 == Vector2.zero)
            //        playerController.PlayAnimation("Evade_Back", 0);
            //    else
            //        playerController.PlayAnimation("Evade_Front", 0);
            //    break;
        }
    }

    public override void Update()
    {
        base.Update();
        //if (playerController.playerInputSystem.Player.Evade.IsPressed())
        //{
        //    if (playerController.evadeTimer >= playerController.evadeCoolTIme && playerModel.state == EPlayerState.Evade_Back)
        //    {
        //        playerController.SwitchState(EPlayerState.Evade_Back);
        //    }
        //}
        if (stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
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
