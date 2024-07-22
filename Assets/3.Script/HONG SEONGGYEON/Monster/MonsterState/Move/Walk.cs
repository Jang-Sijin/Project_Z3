using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Walk : MonsterStateBase
{
    private float RunningCoolTime=7.0f;
    private float CurrentCoolTime=0f;
    public override void Enter()
    {
        base.Enter();
        CurrentCoolTime = 0f;
        monsterController.PlayAnimation("Walk");
    }

    public override void Update()
    {
        base.Update();
        CurrentCoolTime += Time.deltaTime;
        if (monsterController.monsterModel.isGroggy)
        {
            monsterController.SwitchState(MonsterState.Stun_Start);
        }
        else
        {
            if (monsterController.monsterModel.Distance >= 7.0f && CurrentCoolTime >= RunningCoolTime)
            {
                monsterController.SwitchState(MonsterState.Run);
            }


            else if (monsterController.monsterModel.Distance > 2 && monsterController.monsterModel.Distance < 3)
            {
            //    Debug.Log("¿öÅ©");
                monsterController.SwitchState(MonsterState.AttackType_02);
            }

            else if (monsterController.monsterModel.Distance > 4 && monsterController.monsterModel.Distance < 7)
            {
                monsterController.SwitchState(MonsterState.AttackType_03_Start);
            }
        }

    }

    public override void Exit()
    {
        base.Exit();
        CurrentCoolTime = 0f;
    }


}




