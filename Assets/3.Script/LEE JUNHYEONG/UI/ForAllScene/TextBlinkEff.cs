using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class TextBlinkEff : MonoBehaviour
{
    private LoopType loopType = LoopType.Yoyo;
    private TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
      
        text.DOFade(0.1f, 1f).SetLoops(-1, loopType); 
    }
}
