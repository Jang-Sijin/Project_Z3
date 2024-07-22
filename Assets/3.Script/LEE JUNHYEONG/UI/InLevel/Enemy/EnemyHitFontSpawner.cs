using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
using System.Runtime.CompilerServices;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine.Experimental.AI;
using System.Globalization;
using Unity.VisualScripting;

public class EnemyHitFontSpawner : MonoBehaviour
{
    [SerializeField] private GameObject hitText;
    private List<GameObject> hitAniList;
    private List<TextMeshProUGUI> textList;
    private List<RectTransform> hitRect;

    private RectTransform canvasRect;
    private int hitAniIndex = 0;
    [SerializeField] private int hitFontMax = 10;

    private void Awake()
    { 
        hitAniList = new List<GameObject>();
        textList = new List<TextMeshProUGUI>();
        hitRect = new List<RectTransform>();
        canvasRect = GetComponent<RectTransform>();

        for (int i = 0; i < hitFontMax; i++)
        {
            hitAniList.Add(Instantiate(hitText, canvasRect));
            textList.Add(hitAniList[i].GetComponent<TextMeshProUGUI>());
            hitRect.Add(hitAniList[i].GetComponent<RectTransform>());
            hitAniList[i].gameObject.SetActive(false);
        }
    }
    /*
     * 꺼지는 경우의 수
     * bound를 넘어서는 입력이 있을 시
     * 애니메이션이 끝날 때
     */

    private void FixedUpdate()
    {
        /*
         * 플레이어가 바라보는 쪽으로 캔버스 위치 바꾸기
         * 
         * 예상 코드
         * 플레이어의 방향으로 2정도 에너미에서 떨어진 곳에 띄우기
         */
    }

    public void poolingFont()
    {
        if (hitAniIndex >= hitAniList.Count)
            hitAniIndex = 0;
        
        int index = hitAniIndex;

        hitAniList[index].gameObject.SetActive(false);
        hitAniList[index].gameObject.SetActive(true);

        textList[index].DOFade(1f, 2f).SetLoops(2, LoopType.Yoyo).OnComplete(() => turnOffFont(index));

        float randomX = Random.Range(0f, canvasRect.rect.width);
        float randomY = Random.Range(0f, canvasRect.rect.height);

        hitRect[index].anchoredPosition = new Vector2(randomX, randomY);

        hitAniIndex += 1;
    }

    private void turnOffFont(int index)
    {
        hitAniList[index].gameObject.SetActive(false);
    }
}
