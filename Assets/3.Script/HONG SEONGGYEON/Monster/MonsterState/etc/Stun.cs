using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stun : MonsterStateBase
{
    private float CurrentTime = 0f;
    private float CoolTime = 10f;
    public override void Enter()
    {
        base.Enter();
        CurrentTime = 0;
        monsterController.PlayAnimation("Stun");

    }

    public override void Update()
    {
        base.Update();
        CurrentTime += Time.deltaTime;
        //if(CurrentTime>=CoolTime)            
        {
            monsterController.SwitchState(MonsterState.Stun_End);
        }

    }

    public override void Exit()
    {
        base.Exit();
        CurrentTime = 0;
    }

}
