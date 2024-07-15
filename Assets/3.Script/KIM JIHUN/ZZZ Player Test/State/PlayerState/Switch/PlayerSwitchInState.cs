using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSwitchInState : PlayerStateBase
{
    public override void Enter()
    {
        base.Enter();
        playerController.PlayAnimation("SwitchInNormal");
    }

    public override void Update()
    {
        base.Update();
    }
}
