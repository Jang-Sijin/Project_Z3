using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInteraction : MonoBehaviour
{
    public float interactionDistance = 7f; // ��ȣ�ۿ� ������ �Ÿ�
    public Transform player; // �÷��̾��� Transform
    public Camera dialogueCamera; // ��ȭ �� ����ϴ� ī�޶�
    private Camera mainCamera; // ���� ī�޶�
    public DialogController dialogController; // DialogController �ν��Ͻ�
    private bool isPlayerInRange = false; // �÷��̾ ���� ���� �ִ��� ����
    private bool isDialogueActive = false; // ��ȭ�� Ȱ��ȭ�Ǿ����� ����

    public string[] dialogueLines; // NPC�� ��ȭ ����

    void Start()
    {
        mainCamera = Camera.main; // ���� ī�޶� ã��
        dialogueCamera.gameObject.SetActive(false); // ��ȭ ī�޶�� �ʱ⿡�� ��Ȱ��ȭ
    }

    void Update()
    {
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

        if (isDialogueActive && (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))) // ��ȭ�� Ȱ��ȭ�� ���¿��� �����̽��� ���콺 ��Ŭ�� �Է��� ������ ��ȭ ����
        {
            dialogController.DisplayNextSentence();
        }
    }

    void StartDialogue()
    {
        mainCamera.gameObject.SetActive(false); // ���� ī�޶� ��Ȱ��ȭ
        dialogueCamera.gameObject.SetActive(true); // ��ȭ ī�޶� Ȱ��ȭ

        player.LookAt(transform); // �÷��̾ NPC�� �ٶ󺸵��� ����
        transform.LookAt(player); // NPC�� �÷��̾ �ٶ󺸵��� ����

        isDialogueActive = true; // ��ȭ�� Ȱ��ȭ�Ǿ����� ǥ��
        dialogController.StartDialogue(dialogueLines); // ��ȭ ����
    }

    public void EndDialogue()
    {
        isDialogueActive = false; // ��ȭ�� ����Ǿ����� ǥ��
        dialogController.StopTyping(); // ��ȭ �ؽ�Ʈ �ʱ�ȭ

        dialogueCamera.gameObject.SetActive(false); // ��ȭ ī�޶� ��Ȱ��ȭ
        mainCamera.gameObject.SetActive(true); // ���� ī�޶� Ȱ��ȭ
    }
}
