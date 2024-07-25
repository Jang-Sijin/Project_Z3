using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWalk : BossStateBase
{
    private float CurrentTime = 0f;
    private float WalkTime = 3.0f;
    private int Nearattacktype;
    public override void Enter()
    {
        base.Enter();
        bossController.PlayAnimation("Walk");
        CurrentTime = 0;
        Nearattacktype = Random.Range(0, 2);

    }

    public override void Update()
    {
        base.Update();
        float distance = bossController.bossModel.Distance;
        CurrentTime += Time.deltaTime;
        bossController.bossModel.RotateTowards(bossController.bossModel.Target.transform.position);
        if (bossController.bossModel.isGroggy) bossController.SwitchState(BossState.StunStart);
        if (distance < 4.0f)
        {
            Debug.Log("ÀÛ¾ÆÁü");
            switch (Nearattacktype)
            {
                case 0:
                    bossController.SwitchState(BossState.Attack1);
                    break;
                case 1:
                    bossController.SwitchState(BossState.Attack2);
                    break;
            }
        }

        else if (CurrentTime >= WalkTime)
        {
            if (distance < 4.0f)
            {
                switch (Nearattacktype)
                {
                    case 0:
                        bossController.SwitchState(BossState.Attack1);
                        break;
                    case 1:
                        bossController.SwitchState(BossState.Attack2);
                        break;
                }
            }
            else if (15 <= distance) bossController.SwitchState(BossState.Attack3);
            else if (8 <= distance && distance < 12) bossController.SwitchState(BossState.Attack4);
            else if (4 <= distance && distance < 6) bossController.SwitchState(BossState.Attack5);

        }
    }



    public override void Exit()
    {
        base.Exit();
        CurrentTime = 0;

    }
}
