using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStunLoop : BossStateBase
{
    private float Currenttime;
    private float StunTime=10.0f;
    public override void Enter()
    {
        base.Enter();
        bossController.PlayAnimation("StunLoop");
        Currenttime = 0;
    }

    public override void Update()
    {
        base.Update();
        Currenttime += Time.deltaTime;

        if (Currenttime>=StunTime)
        {
            bossController.SwitchState(BossState.StunEnd);
        }

        //���� �߿� �ǰ� ��ȿ �߰��Ұ�
    }



}
