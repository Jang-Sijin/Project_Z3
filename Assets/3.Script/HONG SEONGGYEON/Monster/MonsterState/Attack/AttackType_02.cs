using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackType_02 : MonsterStateBase
{
    public override void Enter()
    {
        base.Enter();
        monsterController.PlayAnimation("AttackType_02");
        StartCoroutine(PerformAttack());
    }

    private IEnumerator PerformAttack()
    {
        yield return new WaitForSeconds(0.25f);  // 약간의 딜레이가 필요함..왜일까..
        float animationLength = GetCurrentAnimationLength();
        if (animationLength > 0)
        {
            yield return new WaitForSeconds(animationLength);

        }
        monsterController.SwitchState(MonsterState.Idle);
    }

    private float GetCurrentAnimationLength()
    {
        AnimatorStateInfo stateInfo = monsterController.monsterModel.animator.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("AttackType_02"))
        {
            return stateInfo.length;
        }
        return 0f;
    }

    public override void Update()
    {
        base.Update();
        // 공격 중에는 이동하지 않도록 설정
        monsterController.monsterModel.nmagent.isStopped = true;
    }

    public override void Exit()
    {
        base.Exit();
        // 공격이 끝난 후 다시 이동 가능하도록 설정
        monsterController.monsterModel.nmagent.isStopped = false;
    }
}
