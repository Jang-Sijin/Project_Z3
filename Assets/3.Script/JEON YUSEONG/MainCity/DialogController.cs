using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DialogController : MonoBehaviour
{
    public Text dialogText; // 대화 텍스트를 표시할 UI Text
    private bool isTyping = false; // 대화가 진행 중인지 여부
    private Queue<string> sentences; // 대화 문장들을 저장할 큐
    private Coroutine typingCoroutine; // 현재 진행 중인 타이핑 코루틴

    void Start()
    {
        dialogText.text = ""; // 대화 텍스트를 빈 문자열로 초기화
        sentences = new Queue<string>(); // 대화 문장들을 저장할 큐 초기화
    }

    // 대화 텍스트를 타이핑 효과로 출력하는 코루틴
    public IEnumerator Typing(string text)
    {
        isTyping = true; // 대화가 진행 중임을 표시
        dialogText.text = ""; // 대화 텍스트를 빈 문자열로 초기화
        foreach (char letter in text.ToCharArray())
        {
            dialogText.text += letter; // 한 글자씩 추가
            yield return new WaitForSeconds(0.1f); // 타이핑 속도 조절
        }
        isTyping = false; // 대화가 끝났음을 표시
    }

    // 대화가 진행 중인지 여부를 반환
    public bool IsTyping()
    {
        return isTyping;
    }

    // 현재 타이핑 중인 코루틴을 중지하고 대화 텍스트를 초기화
    public void StopTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // 현재 타이핑 중인 코루틴 중지
        }
        isTyping = false; // 대화가 끝났음을 표시
        dialogText.text = ""; // 대화 텍스트를 빈 문자열로 초기화
    }

    // 현재 문장을 완전히 표시
    public void CompleteTyping()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine); // 현재 타이핑 중인 코루틴 중지
        }
        if (sentences.Count > 0) // 문장이 남아있는지 확인
        {
            dialogText.text = sentences.Peek(); // 현재 문장을 완전히 표시
            isTyping = false; // 타이핑이 끝났음을 표시
        }
    }

    // 대화를 시작하는 메서드
    public void StartDialogue(string[] dialogueLines)
    {
        // 필수 요소가 없거나 대화 내용이 없으면 아무 작업도 하지 않음
        if (dialogText == null || dialogueLines == null || dialogueLines.Length == 0)
        {
            return;
        }

        sentences.Clear(); // 기존의 대화 문장들을 제거
        foreach (string line in dialogueLines)
        {
            sentences.Enqueue(line); // 새로운 대화 문장들을 큐에 추가
        }
        DisplayNextSentence(); // 첫 번째 문장 표시
    }

    // 다음 문장을 표시하는 메서드
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0) // 더 이상 문장이 없으면 대화 종료
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue(); // 큐에서 다음 문장을 가져옴
        StopTyping(); // 현재 타이핑 중인 텍스트가 있으면 정지
        typingCoroutine = StartCoroutine(Typing(sentence)); // 새로운 문장 타이핑 시작
    }

    // 대화 종료 처리 메서드
    void EndDialogue()
    {
        var npcInteraction = FindObjectOfType<NPCInteraction>();
        if (npcInteraction != null)
        {
            npcInteraction.EndDialogue(); // NPCInteraction에게 대화가 끝났음을 알림
        }
    }
}