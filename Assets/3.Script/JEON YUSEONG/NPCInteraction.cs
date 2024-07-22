using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteraction : MonoBehaviour
{
    public float interactionDistance = 7f; // ��ȣ�ۿ� ������ �Ÿ�
    public Transform player; // �÷��̾��� Transform
    public Camera dialogueCamera; // ��ȭ �� ����ϴ� ī�޶�
    private Camera mainCamera; // ���� ī�޶�
    public DialogController dialogController; // DialogController �ν��Ͻ�
    private bool isPlayerInRange = false; // �÷��̾ ���� ���� �ִ��� ����
    private bool isDialogueActive = false; // ��ȭ�� Ȱ��ȭ�Ǿ����� ����
    public GameObject talkUI; // ��ȭ UI�� �����ϴ� ���� ������Ʈ
    public int npcID; // NPC�� ID
    private Dictionary<int, string[]> npcDialogues; // NPC�� ��ȭ ������ ������ ��ųʸ�
    private Dictionary<int, string> npcScenes;
    void Start()
    {
        // NPC ��ȭ ������ ����
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
            npcID = 1; // �⺻ ��ȭ ID
        }
        if (!npcScenes.ContainsKey(npcID))
        {
            npcScenes[npcID] = "DefaultScene"; // �⺻ �� �̸� ����
        }


        // �ʼ� ��Ұ� �����Ǿ� ���� ������ ��ȭ ī�޶�� UI�� ��Ȱ��ȭ
        if (dialogueCamera != null && dialogController != null)
        {
            mainCamera = Camera.main; // ���� ī�޶� ã��
            dialogueCamera.gameObject.SetActive(false); // ��ȭ ī�޶�� �ʱ⿡�� ��Ȱ��ȭ
            talkUI.SetActive(false); // ��ȭ UI�� �ʱ⿡�� ��Ȱ��ȭ
        }
    }

    void Update()
    {
        if (player == null)
        {
            return; // �÷��̾ �������� �ʾ����� �۾��� �������� ����
        }

        float distance = Vector3.Distance(transform.position, player.position); // NPC�� �÷��̾� ���� �Ÿ� ���

        if (distance <= interactionDistance)
        {
            isPlayerInRange = true; // �÷��̾ ��ȣ�ۿ� �Ÿ� ���� ����
            if (Input.GetKeyDown(KeyCode.F) && !isDialogueActive) // F Ű�� ������ �� ��ȭ ����
            {
                StartDialogue();
            }
        }
        else
        {
            isPlayerInRange = false; // �÷��̾ ��ȣ�ۿ� �Ÿ� ���� ����
        }

        if (isDialogueActive)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !dialogController.IsTyping())
            {
                dialogController.DisplayNextSentence(); // ���� �������� �̵�
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                EndDialogue(); // ESC Ű�� ���� ��ȭ ����
            }
        }
    }

    // ��ȭ�� �����ϴ� �޼���
    void StartDialogue()
    {
        if (dialogController == null)
        {
            return; // DialogController�� �������� �ʾ����� �۾��� �������� ����
        }

        mainCamera.gameObject.SetActive(false); // ���� ī�޶� ��Ȱ��ȭ
        dialogueCamera.gameObject.SetActive(true); // ��ȭ ī�޶� Ȱ��ȭ
        talkUI.SetActive(true); // ��ȭ UI Ȱ��ȭ

        isDialogueActive = true; // ��ȭ�� Ȱ��ȭ�Ǿ����� ǥ��
        dialogController.StartDialogue(npcDialogues[npcID]); // ��ȭ ����
    }

    // ��ȭ ���� ó�� �޼���
    public void EndDialogue()
    {
        isDialogueActive = false; // ��ȭ�� ����Ǿ����� ǥ��
        dialogController.StopTyping(); // ��ȭ �ؽ�Ʈ �ʱ�ȭ
        talkUI.SetActive(false); // ��ȭ UI ��Ȱ��ȭ
        dialogueCamera.gameObject.SetActive(false); // ��ȭ ī�޶� ��Ȱ��ȭ
        mainCamera.gameObject.SetActive(true); // ���� ī�޶� Ȱ��ȭ


        // NPC ID�� ���� �ٸ� ������ ��ȯ
        if (npcScenes.ContainsKey(npcID))
        {
            SceneManager.LoadScene(npcScenes[npcID]); // �ش� NPC�� ������ ��ȯ
        }
        else
        {
            SceneManager.LoadScene("DefaultScene"); // �⺻ ������ ��ȯ
        }
    }
}