using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EModelFoot
{
    Right,
    Left
}
public class PlayerModel : MonoBehaviour
{
    [HideInInspector] public Animator animator;
    [HideInInspector] public EPlayerState state;
    [HideInInspector] public CharacterController characterController;

    public float gravity = -9.8f;
    public SkillConfig skillConfig;
    public int currentNormalAttakIndex = 1;
    public GameObject skillUltStartShot;
    public GameObject skillUltShot;

    [HideInInspector] public EModelFoot foot = EModelFoot.Right;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        characterController = GetComponent<CharacterController>();
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
