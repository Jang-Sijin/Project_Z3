using UnityEngine;

// [전체 구조: FSM]
// 플레이어 상태(State) 관리
namespace JSJ
{
    public abstract class State
    {
        protected PlayerController player;

        public State(PlayerController player)
        {
            this.player = player;
        }

        public abstract void Enter();
        public abstract void Update();
        public abstract void Exit();
    }

    public class PlayerController : MonoBehaviour
    {
        private State _currentState;
        private Animator _animator;
        public Transform cameraTransform; // 카메라 Transform 참조

        void Start()
        {
            // Get Component
            _animator = GetComponent<Animator>();
            cameraTransform = GameObject.FindWithTag("MainCamera").transform;

            // 초기 상태 설정 (Idle 상태)
            ChangeState(new IdleState(this));
        }

        void Update()
        {
            if (_currentState != null)
            {
                _currentState.Update();
            }
        }

        public void ChangeState(State newState)
        {
            if (_currentState != null)
            {
                _currentState.Exit();
            }

            _currentState = newState;

            if (_currentState != null)
            {
                _currentState.Enter();
            }
        }

        public void SetAnimatorBool(string param, bool value)
        {
            _animator.SetBool(param, value);
        }

        public void SetAnimatorTrigger(string param)
        {
            _animator.SetTrigger(param);
        }
    }

    public class IdleState : State
    {
        public IdleState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            // Idle 상태에 들어왔을 때 실행할 코드
            Debug.Log("Entered Idle State");

            // 초기 세팅
            player.SetAnimatorBool("isWalking", false);
            player.SetAnimatorBool("isRunning", false);
        }

        public override void Update()
        {
            // Idle 상태일 때 실행할 코드
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                player.ChangeState(new WalkState(player));
            }
        }

        public override void Exit()
        {
            // Idle 상태를 떠날 때 실행할 코드
            Debug.Log("Exited Idle State");
        }
    }

    public class WalkState : State
    {
        private float speed = 5f;
        private float runThreshold = 2f; // Run 상태로 전환되는 시간
        private float walkTime = 0f;

        public WalkState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            Debug.Log("Entered Walk State");
            player.SetAnimatorBool("isWalking", true);
        }

        public override void Update()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 forward = player.cameraTransform.forward;
            Vector3 right = player.cameraTransform.right;

            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 direction = forward * moveVertical + right * moveHorizontal;

            if (direction.magnitude > 0.1f)
            {
                walkTime += Time.deltaTime;

                // 일정 시간 이상 이동 시 Run 상태로 전환
                if (walkTime >= runThreshold)
                {
                    player.ChangeState(new RunState(player));
                }

                Quaternion targetRotation = Quaternion.LookRotation(direction);
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * speed);

                player.transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
            else
            {
                walkTime = 0f;
                player.SetAnimatorBool("isWalking", false);
                player.ChangeState(new IdleState(player));
            }
        }

        public override void Exit()
        {
            Debug.Log("Exited Walk State");
            walkTime = 0f; // Reset walk time        
        }
    }

    public class RunState : State
    {
        private float speed = 8f; // Run 속도

        public RunState(PlayerController player) : base(player) { }

        public override void Enter()
        {
            Debug.Log("Entered Run State");
            player.SetAnimatorBool("isRunning", true);
        }

        public override void Update()
        {
            float moveHorizontal = Input.GetAxis("Horizontal");
            float moveVertical = Input.GetAxis("Vertical");

            Vector3 forward = player.cameraTransform.forward;
            Vector3 right = player.cameraTransform.right;

            forward.y = 0;
            right.y = 0;
            forward.Normalize();
            right.Normalize();

            Vector3 direction = forward * moveVertical + right * moveHorizontal;

            if (direction.magnitude > 0.1f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);
                player.transform.rotation = Quaternion.Slerp(player.transform.rotation, targetRotation, Time.deltaTime * speed);

                player.transform.Translate(direction * speed * Time.deltaTime, Space.World);
            }
            else
            {
                player.ChangeState(new IdleState(player));
            }
        }

        public override void Exit()
        {
            Debug.Log("Exited Run State");
            player.SetAnimatorBool("isRunning", false);
        }
    }
}