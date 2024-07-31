using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestionBox : MonoBehaviour
{
    private Animator questionBoxAni;
    private TextMeshProUGUI question;

    private void Awake()
    {
        questionBoxAni = GetComponent<Animator>();
        question = transform.GetChild(4).GetComponent<TextMeshProUGUI>();
    }

    public void OpenQuestionBox(string questionText)
    {
        questionBoxAni.SetTrigger("Open");
        question.text = questionText;
    }

    public void CloseQuestionBox()
    {
        StartCoroutine(CloseQuestionBox_co());
    }

    private IEnumerator CloseQuestionBox_co()
    {
        questionBoxAni.SetTrigger("Close");

        yield return new WaitForSeconds(questionBoxAni.GetCurrentAnimatorStateInfo(0).length);
    }
}
