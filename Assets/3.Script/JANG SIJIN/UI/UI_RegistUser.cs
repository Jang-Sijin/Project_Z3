using DG.Tweening;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_RegistUser : MonoBehaviour
{
    [SerializeField] private Button _acceptButton;
    [SerializeField] private Button _cancleButton;

    [SerializeField] private TMP_InputField _idInputField;
    [SerializeField] private TMP_InputField _nickNameInputField;
    [SerializeField] private TMP_InputField _phoneNumberInputField;
    [SerializeField] private TMP_InputField _passwordInputField;
    [SerializeField] private TMP_InputField _passwordConfirmInputField;

    private void Start()
    {
        _acceptButton.onClick.AddListener(() => OnClickAcceptButtonAsync().ConfigureAwait(false));
        _cancleButton.onClick.AddListener(OnClickCancleButton);
    }

    private async Task OnClickAcceptButtonAsync()
    {
        gameObject.SetActive(false);
        await APIManager.Instance.Register(_idInputField.text, _nickNameInputField.text, _phoneNumberInputField.text, _passwordInputField.text);
    }

    private void OnClickCancleButton()
    {
        // ���� ũ�⸦ �����Ѵ�
        Vector3 originalScale = this.gameObject.transform.localScale;
        // DoTween�� ����Ͽ� ũ�⸦ 0���� ���� ũ��� �����ϴ� �ִϸ��̼��� �����Ѵ�        
        this.gameObject.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InSine)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                this.gameObject.transform.localScale = originalScale;

                Clear();
            });                
    }

    public void Clear()
    {
        _idInputField.text = "";
        _nickNameInputField.text = "";
        _phoneNumberInputField.text = "";
        _passwordInputField.text = "";
        _passwordConfirmInputField.text = "";
    }
}