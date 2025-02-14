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
        // ���� ũ�⸦ �����Ѵ�
        Vector3 originalScale = gameObject.transform.localScale;
        // DoTween�� ����Ͽ� ũ�⸦ 0���� ���� ũ��� �����ϴ� �ִϸ��̼��� �����Ѵ�
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(originalScale, 0.25f).SetEase(Ease.InSine);
    }

    private void CloseUI()
    {
        // ���� ũ�⸦ �����Ѵ�
        Vector3 originalScale = gameObject.transform.localScale;
        gameObject.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InSine)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                gameObject.transform.localScale = originalScale;
            });
    }
}