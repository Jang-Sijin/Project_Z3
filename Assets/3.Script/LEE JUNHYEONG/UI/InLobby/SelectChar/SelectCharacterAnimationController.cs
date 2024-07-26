using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectCharacterAnimationController : MonoBehaviour
{
    private Animator _animator;
    private bool _isCharacterIdleAFK;
    private float animationLength;
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        animationLength = GetAnimationClipLength();
    }

    private void OnEnable()
    {
        PlayFirstAnimation();
    }

    void PlayFirstAnimation()
    {
        if (gameObject.name.Equals("Corin"))
            _animator.Play("IdleAFK");
        else
            _animator.Play("Idle_AFK");

        // DoTween을 사용하여 첫 번째 애니메이션이 끝난 후 두 번째 애니메이션을 재생합니다.
        DOVirtual.DelayedCall(animationLength, PlaySecondAnimation);
    }

    void PlaySecondAnimation()
    {
        // 두 번째 애니메이션을 재생합니다.
        _animator.Play("Idle");
    }

    private float GetAnimationClipLength()
    {
        string clipName = "0";

        switch(gameObject.name)
        {
            case "Anbi":
                clipName = "Avatar_Female_Size02_Anbi_Ani_Idle_AFK";
                break;

            case "Corin":
                clipName = "Avatar_Female_Size01_Corin_Ani_Idle_AFK";
                break;

            case "Longinus":
                clipName = "Avatar_Female_Size02_Longinus_Ani_Idle_AFK";
                break;

            default:
                Debug.Log($"There`s no such file name {gameObject.name}");
                return 0f;
        }    

        AnimationClip[] clips = _animator.runtimeAnimatorController.animationClips;
        foreach (AnimationClip clip in clips)
        {
            if (clip.name.Equals(clipName))
            {
                return clip.length;
            }
        }

        Debug.LogWarning($"Animation clip with name {clipName} not found!");
        return 0f;
    }
}
