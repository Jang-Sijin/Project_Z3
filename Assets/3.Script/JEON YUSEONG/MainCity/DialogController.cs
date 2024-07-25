using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogController : MonoBehaviour
{
    public Text dialogText; // ��ȭ �ؽ�Ʈ�� ǥ���� UI Text
    private bool isTyping = false; // ��ȭ�� ���� ������ ����
    private Queue<string> sentences; // ��ȭ ������� ������ ť
    private Coroutine typingCoroutine; // ���� ���� ���� Ÿ���� �ڷ�ƾ

    void Start()
    {
        dialogText.text = ""; // ��ȭ �ؽ�Ʈ�� �� ���ڿ��� �ʱ�ȭ
        sentences = new Queue<string>(); // ��ȭ ������� ������ ť �ʱ�ȭ
    }

    // ��ȭ �ؽ�Ʈ�� Ÿ���� ȿ���� ����ϴ� �ڷ�ƾ
    public IEnumerator Typing(string text)
    {
        isTyping = true; // ��ȭ�� ���� ������ ǥ��
        dialogText.text = ""; // ��ȭ �ؽ�Ʈ�� �� ���ڿ��� �ʱ�ȭ
        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter; // �� ���ھ� �߰�
            yield return new WaitForSeconds(0.1f); // Ÿ���� �ӵ� ����
        }
        isTyping = false; // ��ȭ�� �������� ǥ��
    }

    // ��ȭ�� ���� ������ ���θ� ��ȯ
    public bool IsTyping()
    {
        return isTyping;
    }

    // ���� Ÿ���� ���� �ڷ�ƾ�� �����ϰ� ��ȭ �ؽ�Ʈ�� �ʱ�ȭ
    public void StopTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // ���� Ÿ���� ���� �ڷ�ƾ ����
        }
        isTyping = false; // ��ȭ�� �������� ǥ��
        dialogText.text = ""; // ��ȭ �ؽ�Ʈ�� �� ���ڿ��� �ʱ�ȭ
    }

    // ���� ������ ������ ǥ��
    public void CompleteTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // ���� Ÿ���� ���� �ڷ�ƾ ����
        }
        if (sentences.Count > 0) // ������ �����ִ��� Ȯ��
        {
            dialogText.text = sentences.Peek(); // ���� ������ ������ ǥ��
            isTyping = false; // Ÿ������ �������� ǥ��
        }
    }

    // ��ȭ�� �����ϴ� �޼���
    public void StartDialogue(string[] dialogueLines)
    {
        // �ʼ� ��Ұ� ���ų� ��ȭ ������ ������ �ƹ� �۾��� ���� ����
        if (dialogText == null || dialogueLines == null || dialogueLines.Length == 0)
        {
            return;
        }

        sentences.Clear(); // ������ ��ȭ ������� ����
        foreach (string line in dialogueLines)
        {
            sentences.Enqueue(line); // ���ο� ��ȭ ������� ť�� �߰�
        }
        DisplayNextSentence(); // ù ��° ���� ǥ��
    }

    // ���� ������ ǥ���ϴ� �޼���
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) // �� �̻� ������ ������ ��ȭ ����
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue(); // ť���� ���� ������ ������
        StopTyping(); // ���� Ÿ���� ���� �ؽ�Ʈ�� ������ ����
        typingCoroutine = StartCoroutine(Typing(sentence)); // ���ο� ���� Ÿ���� ����
    }

    // ��ȭ ���� ó�� �޼���
    void EndDialogue()
    {
        var npcInteraction = FindObjectOfType<NPCInteraction>();
        if (npcInteraction != null)
        {
            npcInteraction.EndDialogue(); // NPCInteraction���� ��ȭ�� �������� �˸�
        }
    }
}