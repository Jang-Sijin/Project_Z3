using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class EnemyHitFontSpawner : MonoBehaviour
{
    [SerializeField] private GameObject hitText;
    [SerializeField] private GameObject player; // Debugging
    private List<GameObject> hitAniList;
    private List<TextMeshProUGUI> textList;
    private List<RectTransform> hitRect;

    private RectTransform canvasRect;
    private float distance = 2f; // �θ���� �Ÿ�
    private float height = 0f;
    [SerializeField] private Camera mainCamera;

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

    private void LateUpdate()
    {
        // �÷��̾� ���� ���� ���
        Vector3 directionToPlayer = (player.transform.position - transform.parent.position).normalized;

        // �θ� ��ü�κ��� �÷��̾� �������� ���� �Ÿ���ŭ ������ ��ġ ���
        Vector3 targetPosition = transform.parent.position + directionToPlayer * distance;
        targetPosition.y = transform.parent.position.y + height; // ������ ���� ����

        transform.position = targetPosition;
        LookPlayer();

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

    private void LookPlayer()
    {
        transform.LookAt(transform.position + mainCamera.transform.rotation * Vector3.forward, mainCamera.transform.rotation * Vector3.up);
    }
}
