using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ConfirmPannel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleMessageBoxTextMesh;
    [SerializeField] private TextMeshProUGUI _messageBoxTextMesh;

    [SerializeField] private Button _okButton;

    private void Start()
    {
        _okButton.onClick.AddListener(OnClickOkButton);
    }

    public void ShowMessageBoxText(string titleMessage, string message)
    {
        OpenUI();

        _titleMessageBoxTextMesh.text = titleMessage;
        _messageBoxTextMesh.text = message;
    }

    public void Clear()
    {
        _titleMessageBoxTextMesh.text = "";
        _messageBoxTextMesh.text = "";
    }

    private void OnClickOkButton()
    {
        CloseUI();
    }

    private void OpenUI()
    {
        gameObject.SetActive(true);
        // 현재 크기를 저장한다
        Vector3 originalScale = gameObject.transform.localScale;
        // DoTween을 사용하여 크기를 0에서 원래 크기로 변경하는 애니메이션을 적용한다
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(originalScale, 0.25f).SetEase(Ease.InSine);
    }

    private void CloseUI()
    {
        // 현재 크기를 저장한다
        Vector3 originalScale = gameObject.transform.localScale;
        gameObject.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InSine)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                gameObject.transform.localScale = originalScale;
            });
    }
}