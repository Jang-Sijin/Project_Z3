using UnityEngine;

namespace JSJ
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;           // 플레이어 캐릭터의 Transform
        [SerializeField] private float orbitSpeed = 50f;     // 공전 속도
        [SerializeField] private float scrollSpeed = 2f;     // 스크롤 속도
        [SerializeField] private float minYAngle = -80f;     // 최소 상하 회전 각도
        [SerializeField] private float maxYAngle = 80f;      // 최대 상하 회전 각도
        [SerializeField] private float minDistance = 1f;     // 최소 카메라와 캐릭터 사이 거리
        [SerializeField] private float maxDistance = 5f;     // 최대 카메라와 캐릭터 사이 거리

        private float currentXRotation = 0f;  // 현재 x 회전값
        private float currentYRotation = 0f;  // 현재 y 회전값
        private float currentDistance;        // 현재 카메라와 캐릭터 사이의 거리

        void Start()
        {
            // 초기화
            currentDistance = (minDistance + maxDistance) / 2f;
            target = GameObject.FindWithTag("Player").transform;
            InitCameraPosition();
            LockCursor();
        }

        void Update()
        {
            HandleRotation();
            HandleScroll();
            UpdateCameraPosition();
        }

        void InitCameraPosition()
        {
            // 초기 카메라 위치 설정
            Vector3 initialPosition = new Vector3(0, 1.2f, -2.5f);
            transform.position = target.position + initialPosition;
            currentXRotation = 0;
            currentYRotation = 0;
            transform.LookAt(target);
        }

        void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked; // 커서를 화면 중앙에 고정
            Cursor.visible = false;                   // 커서 숨기기
        }

        void HandleRotation()
        {
            // 마우스 입력값 받아오기
            float mouseX = Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;

            // 좌우 회전 각도 조절
            currentYRotation += mouseX;

            // 상하 회전 각도 조절
            currentXRotation -= mouseY;
            currentXRotation = Mathf.Clamp(currentXRotation, minYAngle, maxYAngle);
        }


        void HandleScroll()
        {
            // 마우스 스크롤 입력값 받아오기
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // 카메라와 플레이어 사이의 거리 조절
            currentDistance -= scrollInput * scrollSpeed;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
        }

        void UpdateCameraPosition()
        {
            // 새로운 위치 계산
            Quaternion rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
            Vector3 direction = rotation * Vector3.forward;
            Vector3 newPosition = target.position - direction * currentDistance;

            // 위치 적용 (부드럽게 이동)
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * orbitSpeed);

            // 회전 적용 (부드럽게 회전)
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * orbitSpeed);
        }

    }
}