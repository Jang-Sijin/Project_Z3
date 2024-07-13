//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//
////public abstract class Player
//    
//    
//    
//    State
//{
//    protected Player player;
//
//    public Player
//    
//    
//    
//    
//    
//    State(Player player)
//    {
//        this.player = player;
//    }
//
//    public abstract void Enter();
//    public abstract void Update();
//    public abstract void Exit();
//}
//
//public class 
//    
//    
//    State : Player
//    
//    
//    State
//{
//    public IdleState(Player player) : base(player) { }
//    public override void Enter()
//    {
//        player.anim.SetBool(player.animIsMoving, false);
//        player.anim.SetBool(player.animIsRunning, false);
//    }
//    public override void Update()
//    {
//        // 이동 키가 입력됐다면 WalkState 전환
//        //      Shift키가 동시에 입력됐다면 RunState로 전환
//
//        if (player.IsMoving()) // WASD가 입력됐다면 이동 시작
//        {
//            if (Input.GetButton(player.inputRun)) //Run 키가 눌러진 상태라면 바로 달리기 상태 전환
//            {
//                player.ChangeState(new RunState(player));
//                return;
//            }
//            player.ChangeState(new WalkState(player)); // 아니라면 걷기 상대 전환
//        }
//    }
//
//    public override void Exit()
//    {
//    }
//
//}
//
//public class WalkState : PlayerIdleState
//{
//    public WalkState(Player player) : base(player) { }
//
//    public override void Enter()
//    {
//        player.anim.SetBool(player.animIsMoving, true);
//        player.anim.SetBool(player.animIsRunning, false);
//        //걷는 애니메이션 시작
//    }
//    public override void Update()
//    {
//        if (player.IsMoving())
//        {
//            if (Input.GetButtonDown(player.inputRun)) // 걷기 도중 Run 키 입력시 달리기 전환 ( Toggle)
//                player.ChangeState(new RunState(player));
//            else
//                player.Move();
//        }
//        else // 걷기를 멈췄다면 Idle 상태 전환
//            player.ChangeState(new IdleState(player));
//    }
//
//    public override void Exit()
//    {
//    }
//}
//
//public class RunState : PlayerIdleState
//{
//    public RunState(Player player) : base(player) { }
//
//    public override void Enter()
//    {
//        //뛰는 애니메이션 시작
//        player.anim.SetBool(player.animIsMoving, true);
//        player.anim.SetBool(player.animIsRunning, true);
//    }
//    public override void Update()
//    {
//        if (player.bIsMove) 
//        {
//            if (Input.GetButtonDown(player.inputRun)) //캐릭터가 Run 키를 한번 더 누른다면 
//                player.ChangeState(new WalkState(player));
//            else
//                player.Move();
//        }
//        else
//            player.ChangeState(new IdleState(player));
//
//    }
//
//    public override void Exit()
//    {
//    }
//
//}
//
//public class Player : MonoBehaviour
//{
//    //해당 스크립트는 Player 오브젝트에 있음. 
//    //Player
//    //  Avatar
//    //  Camera Aram
//    //      Main Camera
//
//    [Header("Camera Arm")]
//    [SerializeField] private Transform cameraArm;
//    [SerializeField] private Transform mainCamera;
//
//    [Header("Avatar")]
//    [HideInInspector] public Animator anim; // Player Animator
//    [HideInInspector] public bool bIsMove; // 현재 이동 상태인지 확인
//    private PlayerIdleState currentState; // 현재 Player 상태
//
//    [HideInInspector] public bool bIsRunning; // 달리고 있는지 판단
//
//    // InputManager
//    public string inputHorizontal { get { return "Horizontal"; } private set { } }
//    public string inputVertical { get { return "Vertical"; } private set { } }
//    public string inputRun { get { return "Run"; } private set { } }
//
//    // Animation Parameter
//    public string animIsMoving { get { return "IsMoving"; } private set { } }
//    public string animIsRunning { get { return "IsRunning"; } private set { } }
//
//
//
//
//    private void Start()
//    {
//        anim = GetComponentInChildren<Animator>();
//        ChangeState(new IdleState(this)); // 초기 상태를 Idle로 설정
//    }
//
//    private void Update()
//    {
//        //LookAround();
//        currentState.Update();
//    }
//
//    public void ChangeState(PlayerIdleState newState)
//    {
//        if (currentState != null)
//        {
//            currentState.Exit();
//        }
//
//        currentState = newState;
//        currentState.Enter();
//    }
//
//    public void Move()
//    {
//        Vector2 moveInput = new Vector2(Input.GetAxis(inputHorizontal), Input.GetAxis(inputVertical));
//        bIsMove = Input.GetAxis(inputHorizontal) != 0 || Input.GetAxis(inputVertical) != 0;
//
//        if (bIsMove)
//        {
//            Vector3 lookForward = new Vector3(cameraArm.forward.x, 0f, cameraArm.forward.z).normalized;
//            Vector3 lookRight = new Vector3(cameraArm.right.x, 0f, cameraArm.right.z).normalized;
//            Vector3 moveDir = lookForward * moveInput.y + lookRight * moveInput.x;
//
//            anim.transform.forward = moveDir;
//        }
//    }
//
//    public bool IsMoving()
//    {
//        Vector2 moveInput = new Vector2(Input.GetAxis(inputHorizontal), Input.GetAxis(inputVertical));
//        bIsMove = Input.GetAxis(inputHorizontal) != 0 || Input.GetAxis(inputVertical) != 0;
//        return bIsMove;
//    }
//}
