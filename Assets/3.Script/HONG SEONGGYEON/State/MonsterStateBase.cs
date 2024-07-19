using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterStateBase : stateBase
{


    protected MonsterController monsterController;
    private AnimatorStateInfo NowPlaying;
    protected MonsterModel monsterModel;

    public override void Init(IstateMachineOwner owner)
    {
        monsterController = (MonsterController)owner;
        MonsterModel monsterModel;
        monsterModel = monsterController.monsterModel;
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
        monsterController.StartCoroutine(coroutine);
    }

       public bool isStopAni()
       {
           NowPlaying = monsterModel.animator.GetCurrentAnimatorStateInfo(0);
           return NowPlaying.normalizedTime >= 1.0f && !monsterModel.animator.IsInTransition(0);
       }


}
