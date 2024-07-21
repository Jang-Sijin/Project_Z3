using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteraction : MonoBehaviour
{
    public float interactionDistance = 7f; // 상호작용 가능한 거리
    public Transform player; // 플레이어의 Transform
    public Camera dialogueCamera; // 대화 시 사용하는 카메라
    private Camera mainCamera; // 메인 카메라
    public DialogController dialogController; // DialogController 인스턴스
    private bool isPlayerInRange = false; // 플레이어가 범위 내에 있는지 여부
    private bool isDialogueActive = false; // 대화가 활성화되었는지 여부
    public GameObject talkUI; // 대화 UI를 포함하는 게임 오브젝트
    public int npcID; // NPC의 ID
    private Dictionary<int, string[]> npcDialogues; // NPC의 대화 내용을 저장할 딕셔너리
    private Dictionary<int, string> npcScenes;
    void Start()
    {
        // NPC 대화 내용을 설정
        npcDialogues = new Dictionary<int, string[]>
        {
            { 1, new string[] { "Hello! I'm NPC 1.", "What can I do for you?", "Have a great day!" } },
            { 2, new string[] { "Greetings from NPC 2!", "Looking for something?", "Goodbye!" } },
            { 3, new string[] { "Hi, I'm NPC 3!", "Need any help?", "See you later!" } },
            { 4, new string[] { "Hello there, NPC 4!", "Feel free to ask me anything.", "Take care!" } }
        };

        npcScenes = new Dictionary<int, string>
        {
            { 1, "Scene1" },
            { 2, "Scene2" },
            { 3, "Scene3" },
            { 4, "Scene4" }
        };

        if (!npcDialogues.ContainsKey(npcID))
        {
            npcID = 1; // 기본 대화 ID
        }
        if (!npcScenes.ContainsKey(npcID))
        {
            npcScenes[npcID] = "DefaultScene"; // 기본 씬 이름 설정
        }


        // 필수 요소가 설정되어 있지 않으면 대화 카메라와 UI를 비활성화
        if (dialogueCamera != null && dialogController != null)
        {
            mainCamera = Camera.main; // 메인 카메라를 찾음
            dialogueCamera.gameObject.SetActive(false); // 대화 카메라는 초기에는 비활성화
            talkUI.SetActive(false); // 대화 UI도 초기에는 비활성화
        }
    }

    void Update()
    {
        if (player == null)
        {
            return; // 플레이어가 설정되지 않았으면 작업을 수행하지 않음
        }

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

        if (isDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !dialogController.IsTyping())
            {
                dialogController.DisplayNextSentence(); // 다음 문장으로 이동
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndDialogue(); // ESC 키를 눌러 대화 종료
            }
        }
    }

    // 대화를 시작하는 메서드
    void StartDialogue()
    {
        if (dialogController == null)
        {
            return; // DialogController가 설정되지 않았으면 작업을 수행하지 않음
        }

        mainCamera.gameObject.SetActive(false); // 메인 카메라 비활성화
        dialogueCamera.gameObject.SetActive(true); // 대화 카메라 활성화
        talkUI.SetActive(true); // 대화 UI 활성화

        isDialogueActive = true; // 대화가 활성화되었음을 표시
        dialogController.StartDialogue(npcDialogues[npcID]); // 대화 시작
    }

    // 대화 종료 처리 메서드
    public void EndDialogue()
    {
        isDialogueActive = false; // 대화가 종료되었음을 표시
        dialogController.StopTyping(); // 대화 텍스트 초기화
        talkUI.SetActive(false); // 대화 UI 비활성화
        dialogueCamera.gameObject.SetActive(false); // 대화 카메라 비활성화
        mainCamera.gameObject.SetActive(true); // 메인 카메라 활성화


        // NPC ID에 따라 다른 씬으로 전환
        if (npcScenes.ContainsKey(npcID))
        {
            SceneManager.LoadScene(npcScenes[npcID]); // 해당 NPC의 씬으로 전환
        }
        else
        {
            SceneManager.LoadScene("DefaultScene"); // 기본 씬으로 전환
        }
    }
}