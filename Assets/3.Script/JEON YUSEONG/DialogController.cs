using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogController : MonoBehaviour
{
    
    public Text dialogText; // ��ȭ �ؽ�Ʈ�� ǥ���� UI Text
    private bool isTyping = false; // ��ȭ�� ���� ������ ����
    private Queue<string> sentences; // ��ȭ ������� ������ ť
    
    // Start�� ó�� ������ �� ȣ��˴ϴ�.
    void Start()
    {
        
        dialogText.text = ""; // ��ȭ �ؽ�Ʈ�� �� ���ڿ��� �ʱ�ȭ
        sentences = new Queue<string>(); // ��ȭ ������� ������ ť �ʱ�ȭ
    }

    // �ؽ�Ʈ�� Ÿ���� ȿ���� ǥ���ϴ� �ڷ�ƾ
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

    // ��ȭ�� ���� ������ Ȯ���ϴ� �Լ�
    public bool IsTyping()
    {
        return isTyping;
    }

    // Ÿ������ �����ϰ� ��ȭ �ؽ�Ʈ�� �ʱ�ȭ�ϴ� �Լ�
    public void StopTyping()
    {
        StopAllCoroutines(); // ��� �ڷ�ƾ ����
        isTyping = false; // ��ȭ�� �������� ǥ��
        dialogText.text = ""; // ��ȭ �ؽ�Ʈ�� �� ���ڿ��� �ʱ�ȭ
    }

    // ��ȭ�� �����ϴ� �Լ�
    public void StartDialogue(string[] dialogueLines)
    {
        sentences.Clear(); // ������ ��ȭ ������� ����
        foreach (string line in dialogueLines)
        {
            sentences.Enqueue(line); // ���ο� ��ȭ ������� ť�� �߰�
        }
        DisplayNextSentence(); // ù ��° ���� ǥ��
    }

    // ���� ������ ǥ���ϴ� �Լ�
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) // �� �̻� ������ ������ ��ȭ ����
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue(); // ť���� ���� ������ ������
        StopTyping(); // ���� Ÿ���� ���� �ؽ�Ʈ�� ������ ����
        StartCoroutine(Typing(sentence)); // ���ο� ���� Ÿ���� ����
    }

    // ��ȭ�� ������ �� ó���� ����
    void EndDialogue()
    {
        FindObjectOfType<NPCInteraction>().EndDialogue(); // NPCInteraction���� ��ȭ�� �������� �˸�
    }
}
