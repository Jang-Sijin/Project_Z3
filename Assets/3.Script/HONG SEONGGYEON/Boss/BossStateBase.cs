using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossStateBase : stateBase
{
 
    protected BossController bossController;
    private AnimatorStateInfo NowPlaying;
    protected BossModel bossModel;

    public override void Init(IstateMachineOwner owner)
    {
        bossController = (BossController)owner;
        BossModel monsterModel;
        monsterModel = bossController.bossModel;
    }

    public override void Enter()
    {

    }

    public override void Exit()
    {

    }

    public override void FixedUpdate()
    {

    }


    public override void LateUpdate()
    {

    }

    public override void UnInit()
    {
    }

    public override void Update()
    {

    }

    protected void StartCoroutine(IEnumerator coroutine)
    {
        bossController.StartCoroutine(coroutine);
    }

       public bool isStopAni()
       {
           NowPlaying = bossModel.animator.GetCurrentAnimatorStateInfo(0);
           return NowPlaying.normalizedTime >= 1.0f && !bossModel.animator.IsInTransition(0);
       }

}
