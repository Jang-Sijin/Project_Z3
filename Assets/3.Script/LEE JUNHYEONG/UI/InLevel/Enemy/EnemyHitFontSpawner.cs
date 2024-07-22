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
     * ������ ����� ��
     * bound�� �Ѿ�� �Է��� ���� ��
     * �ִϸ��̼��� ���� ��
     */

    private void FixedUpdate()
    {
        /*
         * �÷��̾ �ٶ󺸴� ������ ĵ���� ��ġ �ٲٱ�
         * 
         * ���� �ڵ�
         * �÷��̾��� �������� 2���� ���ʹ̿��� ������ ���� ����
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
