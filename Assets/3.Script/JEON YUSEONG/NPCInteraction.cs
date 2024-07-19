using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public float interactionDistance = 7f; // 상호작용 가능한 거리
    public Transform player; // 플레이어의 Transform
    public Camera dialogueCamera; // 대화 시 사용하는 카메라
    private Camera mainCamera; // 메인 카메라
    public DialogController dialogController; // DialogController 인스턴스
    private bool isPlayerInRange = false; // 플레이어가 범위 내에 있는지 여부
    private bool isDialogueActive = false; // 대화가 활성화되었는지 여부

    public string[] dialogueLines; // NPC의 대화 내용

    void Start()
    {
        mainCamera = Camera.main; // 메인 카메라를 찾음
        dialogueCamera.gameObject.SetActive(false); // 대화 카메라는 초기에는 비활성화
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, player.position); // NPC와 플레이어 간의 거리 계산

        if (distance <= interactionDistance)
        {
            isPlayerInRange = true; // 플레이어가 상호작용 거리 내에 있음
            if (Input.GetKeyDown(KeyCode.F) && !isDialogueActive) // F 키를 눌렀을 때 대화 시작
            {
                StartDialogue();
            }
        }
        else
        {
            isPlayerInRange = false; // 플레이어가 상호작용 거리 내에 없음
        }

        if (isDialogueActive && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) // 대화가 활성화된 상태에서 스페이스나 마우스 좌클릭 입력을 받으면 대화 진행
        {
            dialogController.DisplayNextSentence();
        }
    }

    void StartDialogue()
    {
        mainCamera.gameObject.SetActive(false); // 메인 카메라 비활성화
        dialogueCamera.gameObject.SetActive(true); // 대화 카메라 활성화

        player.LookAt(transform); // 플레이어가 NPC를 바라보도록 설정
        transform.LookAt(player); // NPC가 플레이어를 바라보도록 설정

        isDialogueActive = true; // 대화가 활성화되었음을 표시
        dialogController.StartDialogue(dialogueLines); // 대화 시작
    }

    public void EndDialogue()
    {
        isDialogueActive = false; // 대화가 종료되었음을 표시
        dialogController.StopTyping(); // 대화 텍스트 초기화

        dialogueCamera.gameObject.SetActive(false); // 대화 카메라 비활성화
        mainCamera.gameObject.SetActive(true); // 메인 카메라 활성화
    }
}
