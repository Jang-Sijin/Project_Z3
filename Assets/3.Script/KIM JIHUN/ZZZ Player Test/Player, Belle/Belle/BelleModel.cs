using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class BelleModel : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    public EBelleState currentState;
    [HideInInspector] public CharacterController characterController;

    public float gravity = -9.8f;
    private AnimatorStateInfo stateInfo;

    [HideInInspector] public EModelFoot foot = EModelFoot.Right;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
    }

    public bool IsAnimationEnd()
    {
        stateInfo = animator.GetCurrentAnimatorStateInfo(0);
        return stateInfo.normalizedTime >= 1.0f && !animator.IsInTransition(0);
    }

    public void SetOutLeftFoot()
    {
        foot = EModelFoot.Left;
    }

    public void SetOutRightFoot()
    {
        foot = EModelFoot.Right;
    }


}
