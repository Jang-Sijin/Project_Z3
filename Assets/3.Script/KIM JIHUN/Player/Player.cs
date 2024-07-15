using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected Player player;

    public PlayerState(Player player)
    {
        this.player = player;
    }

    public abstract void Enter();
    public abstract void Update();
    public abstract void Exit();
}

public class IdleState : PlayerState
{
    public IdleState(Player player) : base(player) { }
    public override void Enter()
    {
        player.anim.SetBool("IsMoving", false);
    }
    public override void Update()
    {
        // 이동 키가 입력됐다면 WalkState 전환
        //      Shift키가 동시에 입력됐다면 RunState로 전환
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        player.bIsMove = moveInput.magnitude != 0;

        if(player.bIsMove) // 이동 시작 -> State Walk, Run으로 변경
        {
            //if(Input.GetButton(player.dash))
            //{
            //    player.ChangeState(new RunState(player));
            //    return;
            //}
            player.ChangeState(new WalkState(player));
        }
        
    }

    public override void Exit()
    {
    }

}

public class WalkState : PlayerState
{
    public WalkState(Player player) : base(player) { }

    public override void Enter()
    {
        player.moveSpeed = 2f;
        player.anim.SetBool("IsMoving", true);
        //걷는 애니메이션 시작
    }
    public override void Update()
    {
        if (player.IsMoving())
            player.Move();
        else
            player.ChangeState(new IdleState(player));
    }

    public override void Exit()
    {
        player.moveSpeed = 0f;
    }
}

public class RunState : PlayerState
{
    public RunState(Player player) : base(player) { }

    public override void Enter()
    {
        player.moveSpeed = 10f;
        //뛰는 애니메이션 시작
    }
    public override void Update()
    { 
        if(player.bIsMove)
            player.Move();
        else
            player.ChangeState(new IdleState(player));

    }

    public override void Exit()
    {
        throw new System.NotImplementedException();
    }

}

public class Player : MonoBehaviour
{
    //해당 스크립트는 Player 오브젝트에 있음. 
    //Player
    //  Avatar
    //  Camera Aram
    //      Main Camera

    [Header("Camera Arm")]
    [SerializeField] private Transform cameraArm;
    [SerializeField] private Transform mainCamera;

    [Header("Avatar")]
    [HideInInspector] public Animator anim; // Player Animator
    [HideInInspector] public bool bIsMove; // 현재 이동 상태인지 확인
    private PlayerState currentState; // 현재 Player 상태

    [HideInInspector] public float moveSpeed; // 이동 속도
    [HideInInspector] public bool bIsRunning; // 달리고 있는지 판단


    //[HideInInspector] public string dash = "Dash";



    private void Start()
    {
        anim = GetComponentInChildren<Animator>();
        ChangeState(new IdleState(this)); // 초기 상태를 Idle로 설정
    }

    private void Update()
    {
        //LookAround();
        currentState.Update();
    }

    public void ChangeState(PlayerState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;
        currentState.Enter();
    }

    //public void LookAround()
    //{
    //    // 마우스로 상하좌우 시점 컨트롤
    //    Vector2 mouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    //    Vector3 camAngle = cameraArm.rotation.eulerAngles;
    //
    //    float cameraX = camAngle.x - mouseDelta.y;
    //    if(cameraX < 180f) //카메라 회전값이 180도 미만일 경우 -> 카메라가 위로 회전할 때
    //    {
    //        cameraX = Mathf.Clamp(cameraX, -1f, 50f);
    //    }
    //    else // 카메라가 아래로 회전할 경우
    //    {
    //        cameraX = Mathf.Clamp(cameraX, 335f, 361f);
    //    }
    //
    //    cameraArm.rotation = Quaternion.Euler(cameraX, camAngle.y + mouseDelta.x, camAngle.z);
    //}

    public void Move()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bIsMove = moveInput.magnitude != 0;
        
        if (bIsMove)
        {
            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;    
            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;

            anim.transform.forward = moveDir;
            //transform.position += moveDir * Time.deltaTime * moveSpeed;
        }
    }

    public bool IsMoving()
    {
        Vector2 moveInput = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        bIsMove = moveInput.magnitude != 0;
        return bIsMove;
    }
}
