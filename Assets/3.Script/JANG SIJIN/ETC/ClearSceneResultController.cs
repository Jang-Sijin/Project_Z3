using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UniRx;
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

    private ReactiveProperty<int> currentExp = new ReactiveProperty<int>();    
    private int targetExp;    
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
        currentExp.Value = Build_PlayerManager.INSTANCE.currentExp;
        targetExp = currentExp.Value + GameManager.Instance.StageTotalExp;        

        // 현재 플레이어 상태를 UI에 반영
        CanvasLevelText.text = playerLevel.ToString();
        CanvasExpText.text = currentExp.ToString();
        CanvasExpMaxText.text = expMax.ToString();

        // 경험치 변화 감지
        currentExp
            .Where(exp => exp >= expMax)
            .Subscribe(_ => LevelUp())
            .AddTo(this); // AddTo는 해당 컴포넌트가 파괴될 때 자동으로 구독을 해제합니다.

        // 획득한 경험치 애니메이션
        AnimateExp();

        // 스테이지 클리어 타임 애니메이션
        AnimateClearTime(GameManager.Instance.StageClearTime);
    }

    private void AnimateExp()
    {
        DOTween.To(() => currentExp.Value, x => currentExp.Value = x, targetExp, 2f)
            .SetEase(Ease.Linear);
    }

    private void LevelUp()
    {
        // 현재 경험치 및 목표 경험치 갱신
        currentExp.Value -= expMax;
        targetExp -= expMax;

        // 플레이어 레벨업
        playerLevel++;
        Build_PlayerManager.INSTANCE.playerLevel = playerLevel;

        // 최대 경험치량 갱신
        expMax = playerLevel * 1000;

        // UI 갱신
        CanvasLevelText.text = playerLevel.ToString();
        CanvasExpMaxText.text = expMax.ToString();

        // 애니메이션 재시작
        AnimateExp();
    }

    private void UpdateExpDisplay(int currentExp)
    {
        int displayedExp = currentExp;

        CanvasGetExpText.text = Mathf.Floor(currentExp).ToString();
        CanvasExpText.text = Mathf.Floor(displayedExp).ToString();
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

//public class ClearSceneResultController : MonoBehaviour
//{
//    public List<Image> CanvasItemList;
//    public TextMeshProUGUI CanvasGetExpText;
//    public TextMeshProUGUI CanvasExpText;
//    public TextMeshProUGUI CanvasExpMaxText;
//    public TextMeshProUGUI CanvasLevelText;
//    public TextMeshProUGUI CanvasClearTime;

//    private void Start()
//    {
//        // StageGetItemList가 null이 아니면 애니메이션을 시작합니다.
//        if (GameManager.Instance.StageGetItemList != null)
//        {
//            StartCoroutine(ShowItemsWithAnimation());
//        }

//        // 현재 플레이어 상태를 UI에 반영
//        CanvasLevelText.text = Build_PlayerManager.INSTANCE.playerLevel.ToString();
//        CanvasExpMaxText.text = (Build_PlayerManager.INSTANCE.playerLevel * 1000).ToString();
//        CanvasExpText.text = Build_PlayerManager.INSTANCE.currentExp.ToString();

//        // 획득한 총 경험치 애니메이션
//        AnimateExpText(CanvasGetExpText, 0, GameManager.Instance.StageTotalExp);

//        // 실제 경험치 애니메이션
//        AnimateExpText(CanvasGetExpText, 0, GameManager.Instance.StageTotalExp);

//        // 실제 경험치 증가
//        Build_PlayerManager.INSTANCE.EarnExp(GameManager.Instance.StageTotalExp);
//        UpdateExpUI();

//        // 스테이지 클리어 시간 표시
//        DisplayClearTime();
//    }

//    private void DisplayClearTime()
//    {
//        float clearTime = GameManager.Instance.StageClearTime;
//        TimeSpan timeSpan = TimeSpan.FromSeconds(clearTime);
//        CanvasClearTime.text = string.Format("{0:D2}:{1:D2}:{2:D2}", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds);
//    }

//    private IEnumerator ShowItemsWithAnimation()
//    {
//        for (int i = 0; i < GameManager.Instance.StageGetItemList.Count; i++)
//        {
//            // 해당 인덱스의 아이템 가져오기
//            var item = GameManager.Instance.StageGetItemList[i];
//            var itemImage = CanvasItemList[i];

//            // 아이템 이미지 설정
//            itemImage.sprite = item.itemIcon;  // 여기서 item.itemIcon은 아이템의 스프라이트 이미지입니다.
//            itemImage.transform.localScale = Vector3.zero;  // 초기 크기를 0으로 설정

//            // 애니메이션 시작 (0.5초 동안 확대)
//            itemImage.gameObject.SetActive(true);
//            itemImage.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

//            // 0.5초 대기
//            yield return new WaitForSeconds(0.5f);
//        }
//    }

//    private void AnimateExpText(TextMeshProUGUI textMesh, float startValue, float endValue, TweenCallback onComplete = null)
//    {
//        DOTween.To(() => startValue, x =>
//        {
//            startValue = x;
//            textMesh.text = Mathf.Floor(startValue).ToString();
//        }, endValue, 2f).SetEase(Ease.Linear).OnComplete(onComplete);
//    }

//    private void UpdateExpUI()
//    {
//        float currentExp = Build_PlayerManager.INSTANCE.currentExp;
//        float maxExp = Build_PlayerManager.INSTANCE.playerLevel * 1000;

//        if (currentExp >= maxExp)
//        {
//            // 레벨업 처리
//            float overflowExp = currentExp - maxExp;
//            Build_PlayerManager.INSTANCE.playerLevel++;
//            Build_PlayerManager.INSTANCE.currentExp = (int)overflowExp;

//            // 레벨업 애니메이션
//            CanvasLevelText.text = Build_PlayerManager.INSTANCE.playerLevel.ToString();
//            CanvasExpMaxText.text = (Build_PlayerManager.INSTANCE.playerLevel * 1000).ToString();
//            AnimateExpText(0, overflowExp, () => UpdateExpUI());
//        }
//        else
//        {
//            // 경험치 업데이트 애니메이션
//            CanvasExpText.text = Build_PlayerManager.INSTANCE.currentExp.ToString();
//            CanvasExpMaxText.text = (Build_PlayerManager.INSTANCE.playerLevel * 1000).ToString();
//            AnimateExpText(0, currentExp);
//        }
//    }
//}