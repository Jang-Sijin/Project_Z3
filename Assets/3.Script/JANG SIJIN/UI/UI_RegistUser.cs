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
        // 로딩 패널 표시
        _uiLoadingPannel.gameObject.SetActive(true);
        gameObject.SetActive(false);

        try
        {
            await APIManager.Instance.Register(_idInputField.text, _nickNameInputField.text, _phoneNumberInputField.text, _passwordInputField.text,
                async () =>
                {                    
                    await UniTask.Yield();
        
                    _uiErrorPannel.GetComponent<UI_ConfirmPannel>()
                    .ShowMessageBoxText("회원가입 완료", "정상적으로 회원가입이 완료되었습니다.\n가입한 이메일로 로그인하세요.");
                });

        }
        catch (Exception ex)
        {
            _uiErrorPannel.GetComponent<UI_ConfirmPannel>().ShowMessageBoxText("회원가입 실패", ex.Message);
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
        // 현재 크기를 저장한다
        Vector3 originalLocalScale = gameObject.transform.localScale;
        gameObject.transform.localScale = Vector3.zero;
        gameObject.transform.DOScale(originalLocalScale, 0.25f).SetEase(Ease.InSine);
    }

    private void CloseUI()
    {
        // 현재 크기를 저장한다
        Vector3 originalLocalScale = gameObject.transform.localScale;
        // DoTween을 사용하여 크기를 0에서 원래 크기로 변경하는 애니메이션을 적용한다        
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