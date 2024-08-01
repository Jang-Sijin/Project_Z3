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
        // StageGetItemList�� null�� �ƴϸ� �ִϸ��̼��� �����մϴ�.
        if (GameManager.Instance.StageGetItemList != null)
        {
            StartCoroutine(ShowItemsWithAnimation());
        }

        // �ʱ�ȭ
        playerLevel = Build_PlayerManager.INSTANCE.playerLevel;
        expMax = playerLevel * 1000;
        currentExp.Value = Build_PlayerManager.INSTANCE.currentExp;
        targetExp = currentExp.Value + GameManager.Instance.StageTotalExp;        

        // ���� �÷��̾� ���¸� UI�� �ݿ�
        CanvasLevelText.text = playerLevel.ToString();
        CanvasExpText.text = currentExp.ToString();
        CanvasExpMaxText.text = expMax.ToString();

        // ����ġ ��ȭ ����
        currentExp
            .Where(exp => exp >= expMax)
            .Subscribe(_ => LevelUp())
            .AddTo(this); // AddTo�� �ش� ������Ʈ�� �ı��� �� �ڵ����� ������ �����մϴ�.

        // ȹ���� ����ġ �ִϸ��̼�
        AnimateExp();

        // �������� Ŭ���� Ÿ�� �ִϸ��̼�
        AnimateClearTime(GameManager.Instance.StageClearTime);
    }

    private void AnimateExp()
    {
        DOTween.To(() => currentExp.Value, x => currentExp.Value = x, targetExp, 2f)
            .SetEase(Ease.Linear);
    }

    private void LevelUp()
    {
        // ���� ����ġ �� ��ǥ ����ġ ����
        currentExp.Value -= expMax;
        targetExp -= expMax;

        // �÷��̾� ������
        playerLevel++;
        Build_PlayerManager.INSTANCE.playerLevel = playerLevel;

        // �ִ� ����ġ�� ����
        expMax = playerLevel * 1000;

        // UI ����
        CanvasLevelText.text = playerLevel.ToString();
        CanvasExpMaxText.text = expMax.ToString();

        // �ִϸ��̼� �����
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

        float duration = 2f; // �ִϸ��̼� ���� �ð�
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
            // �ش� �ε����� ������ ��������
            var item = GameManager.Instance.StageGetItemList[i];
            var itemImage = CanvasItemList[i];

            // ������ �̹��� ����
            itemImage.sprite = item.itemIcon;  // ���⼭ item.itemIcon�� �������� ��������Ʈ �̹����Դϴ�.
            itemImage.transform.localScale = Vector3.zero;  // �ʱ� ũ�⸦ 0���� ����

            // �ִϸ��̼� ���� (0.5�� ���� Ȯ��)
            itemImage.gameObject.SetActive(true);
            itemImage.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

            // 0.5�� ���
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
//        // StageGetItemList�� null�� �ƴϸ� �ִϸ��̼��� �����մϴ�.
//        if (GameManager.Instance.StageGetItemList != null)
//        {
//            StartCoroutine(ShowItemsWithAnimation());
//        }

//        // ���� �÷��̾� ���¸� UI�� �ݿ�
//        CanvasLevelText.text = Build_PlayerManager.INSTANCE.playerLevel.ToString();
//        CanvasExpMaxText.text = (Build_PlayerManager.INSTANCE.playerLevel * 1000).ToString();
//        CanvasExpText.text = Build_PlayerManager.INSTANCE.currentExp.ToString();

//        // ȹ���� �� ����ġ �ִϸ��̼�
//        AnimateExpText(CanvasGetExpText, 0, GameManager.Instance.StageTotalExp);

//        // ���� ����ġ �ִϸ��̼�
//        AnimateExpText(CanvasGetExpText, 0, GameManager.Instance.StageTotalExp);

//        // ���� ����ġ ����
//        Build_PlayerManager.INSTANCE.EarnExp(GameManager.Instance.StageTotalExp);
//        UpdateExpUI();

//        // �������� Ŭ���� �ð� ǥ��
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
//            // �ش� �ε����� ������ ��������
//            var item = GameManager.Instance.StageGetItemList[i];
//            var itemImage = CanvasItemList[i];

//            // ������ �̹��� ����
//            itemImage.sprite = item.itemIcon;  // ���⼭ item.itemIcon�� �������� ��������Ʈ �̹����Դϴ�.
//            itemImage.transform.localScale = Vector3.zero;  // �ʱ� ũ�⸦ 0���� ����

//            // �ִϸ��̼� ���� (0.5�� ���� Ȯ��)
//            itemImage.gameObject.SetActive(true);
//            itemImage.transform.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutBack);

//            // 0.5�� ���
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
//            // ������ ó��
//            float overflowExp = currentExp - maxExp;
//            Build_PlayerManager.INSTANCE.playerLevel++;
//            Build_PlayerManager.INSTANCE.currentExp = (int)overflowExp;

//            // ������ �ִϸ��̼�
//            CanvasLevelText.text = Build_PlayerManager.INSTANCE.playerLevel.ToString();
//            CanvasExpMaxText.text = (Build_PlayerManager.INSTANCE.playerLevel * 1000).ToString();
//            AnimateExpText(0, overflowExp, () => UpdateExpUI());
//        }
//        else
//        {
//            // ����ġ ������Ʈ �ִϸ��̼�
//            CanvasExpText.text = Build_PlayerManager.INSTANCE.currentExp.ToString();
//            CanvasExpMaxText.text = (Build_PlayerManager.INSTANCE.playerLevel * 1000).ToString();
//            AnimateExpText(0, currentExp);
//        }
//    }
//}