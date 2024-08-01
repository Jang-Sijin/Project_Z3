using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ClearSceneResultController : MonoBehaviour
{
    public List<Image> CanvasItemList;
    public TextMeshProUGUI CanvasGetExpText;
    public TextMeshProUGUI CanvasExpText;
    public TextMeshProUGUI CanvasExpMaxText;
    public TextMeshProUGUI CanvasLevelText;
    public TextMeshProUGUI CanvasClearTime;

    private int initialExp;
    private int targetExp;
    private int currentExp;
    private int playerLevel;
    private int expMax;

    private void Start()
    {
        // StageGetItemList가 null이 아니면 애니메이션을 시작합니다.
        if (GameManager.Instance.StageGetItemList != null)
        {
            StartCoroutine(ShowItemsWithAnimation());
        }

        // 초기화
        playerLevel = Build_PlayerManager.INSTANCE.playerLevel;
        expMax = playerLevel * 1000;
        initialExp = Build_PlayerManager.INSTANCE.currentExp;
        targetExp = initialExp + GameManager.Instance.StageTotalExp;
        currentExp = initialExp;

        // 현재 플레이어 상태를 UI에 반영
        CanvasLevelText.text = Build_PlayerManager.INSTANCE.playerLevel.ToString();
        CanvasExpText.text = Build_PlayerManager.INSTANCE.currentExp.ToString();
        CanvasExpMaxText.text = (Build_PlayerManager.INSTANCE.playerLevel * 1000).ToString();

        // 획득한 경험치 애니메이션
        AnimateExp();

        // 스테이지 클리어 타임 애니메이션
        AnimateClearTime(GameManager.Instance.StageClearTime);
    }

    private void AnimateExp()
    {
        DOTween.To(() => currentExp, x =>
        {
            currentExp = x;
            UpdateExpDisplay(currentExp);

        }, targetExp, 2f).SetEase(Ease.Linear).OnUpdate(() =>
        {
            if (currentExp >= expMax)
            {
                currentExp -= expMax;
                LevelUp();
            }
        });
    }

    private void UpdateExpDisplay(int currentExp)
    {
        int displayedExp = currentExp % expMax;

        CanvasGetExpText.text = Mathf.Floor(currentExp).ToString();
        CanvasExpText.text = Mathf.Floor(displayedExp).ToString();
    }

    private void LevelUp()
    {
        playerLevel++;
        Build_PlayerManager.INSTANCE.playerLevel = playerLevel;
        expMax = playerLevel * 1000;

        CanvasLevelText.text = playerLevel.ToString();
        CanvasExpMaxText.text = expMax.ToString();
        AnimateExp();
    }

    private void AnimateClearTime(float clearTime)
    {
        Debug.Log("ClearTime: " + clearTime);

        float duration = 2f; // 애니메이션 지속 시간
        DOTween.To(() => clearTime, x =>
        {
            clearTime = x;
            CanvasClearTime.text = FormatTime(clearTime);
        }, clearTime, duration).SetEase(Ease.Linear);
    }

    private string FormatTime(float timeInSeconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(timeInSeconds);
        return string.Format("{0:D2}:{1:D2}:{2:D2}", time.Hours, time.Minutes, time.Seconds);
    }


    private IEnumerator ShowItemsWithAnimation()
    {
        for (int i = 0; i < GameManager.Instance.StageGetItemList.Count; i++)
        {
            // 해당 인덱스의 아이템 가져오기
            var item = GameManager.Instance.StageGetItemList[i];
            var itemImage = CanvasItemList[i];

            // 아이템 이미지 설정
            itemImage.sprite = item.itemIcon;  // 여기서 item.itemIcon은 아이템의 스프라이트 이미지입니다.
            itemImage.transform.localScale = Vector3.zero;  // 초기 크기를 0으로 설정

            // 애니메이션 시작 (0.5초 동안 확대)
            itemImage.gameObject.SetActive(true);
            itemImage.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

            // 0.5초 대기
            yield return new WaitForSeconds(0.5f);
        }
    }
}