using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextBlinkEff : MonoBehaviour
{
    /*
     * 첫 인트로에 있는 text에 깜빡거림 컴포넌트입니다.
     */
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
      
        text.DOFade(0.1f, 1f).SetLoops(-1, LoopType.Yoyo); 
    }
}
