using UnityEngine;

namespace JSJ
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;           // �÷��̾� ĳ������ Transform
        [SerializeField] private float orbitSpeed = 50f;     // ���� �ӵ�
        [SerializeField] private float scrollSpeed = 2f;     // ��ũ�� �ӵ�
        [SerializeField] private float minYAngle = -80f;     // �ּ� ���� ȸ�� ����
        [SerializeField] private float maxYAngle = 80f;      // �ִ� ���� ȸ�� ����
        [SerializeField] private float minDistance = 1f;     // �ּ� ī�޶�� ĳ���� ���� �Ÿ�
        [SerializeField] private float maxDistance = 5f;     // �ִ� ī�޶�� ĳ���� ���� �Ÿ�

        private float currentXRotation = 0f;  // ���� x ȸ����
        private float currentYRotation = 0f;  // ���� y ȸ����
        private float currentDistance;        // ���� ī�޶�� ĳ���� ������ �Ÿ�

        void Start()
        {
            // �ʱ�ȭ
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
            // �ʱ� ī�޶� ��ġ ����
            Vector3 initialPosition = new Vector3(0, 1.2f, -2.5f);
            transform.position = target.position + initialPosition;
            currentXRotation = 0;
            currentYRotation = 0;
            transform.LookAt(target);
        }

        void LockCursor()
        {
            Cursor.lockState = CursorLockMode.Locked; // Ŀ���� ȭ�� �߾ӿ� ����
            Cursor.visible = false;                   // Ŀ�� �����
        }

        void HandleRotation()
        {
            // ���콺 �Է°� �޾ƿ���
            float mouseX = Input.GetAxis("Mouse X") * orbitSpeed * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * orbitSpeed * Time.deltaTime;

            // �¿� ȸ�� ���� ����
            currentYRotation += mouseX;

            // ���� ȸ�� ���� ����
            currentXRotation -= mouseY;
            currentXRotation = Mathf.Clamp(currentXRotation, minYAngle, maxYAngle);
        }


        void HandleScroll()
        {
            // ���콺 ��ũ�� �Է°� �޾ƿ���
            float scrollInput = Input.GetAxis("Mouse ScrollWheel");

            // ī�޶�� �÷��̾� ������ �Ÿ� ����
            currentDistance -= scrollInput * scrollSpeed;
            currentDistance = Mathf.Clamp(currentDistance, minDistance, maxDistance);
        }

        void UpdateCameraPosition()
        {
            // ���ο� ��ġ ���
            Quaternion rotation = Quaternion.Euler(currentXRotation, currentYRotation, 0);
            Vector3 direction = rotation * Vector3.forward;
            Vector3 newPosition = target.position - direction * currentDistance;

            // ��ġ ���� (�ε巴�� �̵�)
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * orbitSpeed);

            // ȸ�� ���� (�ε巴�� ȸ��)
            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime * orbitSpeed);
        }

    }
}