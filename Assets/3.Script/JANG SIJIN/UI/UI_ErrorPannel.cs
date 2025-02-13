using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_ErrorPannel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _titleMessageBoxTextMesh;
    [SerializeField] private TextMeshProUGUI _messageBoxTextMesh;

    [SerializeField] private Button _okButton;

    private void Start()
    {
        _okButton.onClick.AddListener(OnClickOkButton);
    }

    public void SetMessageBoxText(string titleMessage, string message)
    {
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