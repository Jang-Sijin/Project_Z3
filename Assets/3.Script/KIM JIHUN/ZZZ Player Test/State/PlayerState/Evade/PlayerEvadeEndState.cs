using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvadeEndState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();

        switch(playerModel.currentState)
        {
            case EPlayerState.EvadeFrontEnd:
                playerController.PlayAnimation("Evade_Front_End");
                break;
            case EPlayerState.EvadeBackEnd:
                playerController.PlayAnimation("Evade_Back_End");
                break;
        }
    }

    public override void Update()
    {
        base.Update();
        //궁극기
        if(playerController.playerInputSystem.Player.Ult.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackUltStart);
            return;
        }
        //평타 or 러쉬
        if (playerController.playerInputSystem.Player.Fire.triggered)
        {
            switch (playerModel.currentState)
            {
                case EPlayerState.EvadeFrontEnd:
                    playerController.SwitchState(EPlayerState.NormalAttack);
                    return;
                case EPlayerState.EvadeBackEnd:
                    playerController.SwitchState(EPlayerState.AttackRush);
                    return;
            }
        }
        //스킬
        if (playerController.playerInputSystem.Player.Skill.triggered)
        {
            playerController.SwitchState(EPlayerState.AttackSkill);
            return;
        }
        //이동
        if (playerController.inputMoveVec2 != Vector2.zero)
        {
            playerController.SwitchState(EPlayerState.Walk);
            return;
            //playerController.SwitchState(EPlayerState.RunStart);
        }
        //회피
        if (playerController.playerInputSystem.Player.Evade.triggered)
        {
            if (playerController.evadeTimer >= playerController.evadeCoolTime)
            {
                playerController.SwitchState(EPlayerState.EvadeBack);
                return;
            }
        }
        //애니메이션 종료
        if (stateInfo.normalizedTime >= 1.0f && !playerModel.animator.IsInTransition(0))
        {
            playerController.SwitchState(EPlayerState.Idle);
            return;
        }
    }
}
