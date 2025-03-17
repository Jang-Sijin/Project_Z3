using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
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

    [SerializeField] private GameObject _uiErrorPannel;
    [SerializeField] private GameObject _uiLoadingPannel;

    private void Start()
    {
        _acceptButton.onClick.AddListener(() => OnClickAcceptButtonAsync().Forget());
        _cancleButton.onClick.AddListener(OnClickCancleButton);
    }

    private async UniTask OnClickAcceptButtonAsync()
    {
        // �ε� �г� ǥ��
        _uiLoadingPannel.gameObject.SetActive(true);
        gameObject.SetActive(false);

        try
        {
            await APIManager.Instance.Register(_idInputField.text, _nickNameInputField.text, _phoneNumberInputField.text, _passwordInputField.text,
                async () =>
                {                    
                    await UniTask.Yield();
        
                    _uiErrorPannel.GetComponent<UI_ConfirmPannel>()
                    .ShowMessageBoxText("ȸ������ �Ϸ�", "���������� ȸ�������� �Ϸ�Ǿ����ϴ�.\n������ �̸��Ϸ� �α����ϼ���.");
                });

        }
        catch (Exception ex)
        {
            _uiErrorPannel.GetComponent<UI_ConfirmPannel>().ShowMessageBoxText("ȸ������ ����", ex.Message);
        }
        finally
        {
            _uiLoadingPannel.gameObject.SetActive(false);
            CloseUI();
        }
    }

    private void OnClickCancleButton()
    {
        CloseUI();
    }

    public void OpenUI()
    {
        gameObject.SetActive(true);
        // ���� ũ�⸦ �����Ѵ�
        Vector3 originalLocalScale = gameObject.transform.localScale;
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(originalLocalScale, 0.25f).SetEase(Ease.InSine);
    }

    private void CloseUI()
    {
        // ���� ũ�⸦ �����Ѵ�
        Vector3 originalLocalScale = gameObject.transform.localScale;
        // DoTween�� ����Ͽ� ũ�⸦ 0���� ���� ũ��� �����ϴ� �ִϸ��̼��� �����Ѵ�        
        this.gameObject.transform.DOScale(Vector3.zero, 0.25f).SetEase(Ease.InSine)
            .OnComplete(() =>
            {
                gameObject.SetActive(false);
                this.gameObject.transform.localScale = originalLocalScale;

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