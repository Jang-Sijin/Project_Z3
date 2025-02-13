using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIAPIManager : MonoBehaviour
{
    [SerializeField] private TMP_InputField _emailInputField;
    [SerializeField] private TMP_InputField _passwordInputField;

    [SerializeField] private TextMeshProUGUI _apiStatusTextMesh;

    [SerializeField] private Button _registUserButton;
    [SerializeField] private Button _loginUserButton;


    [SerializeField] private GameObject _registUserPannel;
    [SerializeField] private GameObject _uiLoadingPannel;
    [SerializeField] private GameObject _uiErrorPannel;

    private void OnValidate()
    {
        _apiStatusTextMesh.text = "";
    }

    // Start is called before the first frame update
    private void Start()
    {
        _registUserButton.onClick.AddListener(OnClickRegistUserButton);
        _loginUserButton.onClick.AddListener(OnClickLoginButton);
    }

    private void OnClickRegistUserButton()
    {                
        if (_registUserPannel != null)
        {
            _registUserPannel.gameObject.SetActive(true);

            // ���� ũ�⸦ �����Ѵ�
            Vector3 originalScale = _registUserPannel.gameObject.transform.localScale;
            // DoTween�� ����Ͽ� ũ�⸦ 0���� ���� ũ��� �����ϴ� �ִϸ��̼��� �����Ѵ�
            _registUserPannel.gameObject.transform.localScale = Vector3.zero;
            _registUserPannel.gameObject.transform.DOScale(originalScale, 0.25f).SetEase(Ease.InSine);
        }
    }

    private async void OnClickLoginButton()
    {
        _uiLoadingPannel.gameObject.SetActive(true);

        try
        {
            _apiStatusTextMesh.text = await APIManager.Instance.Login(_emailInputField.text, _passwordInputField.text);
        }
        catch (Exception ex)
        {
            _uiErrorPannel.gameObject.SetActive(true);

            // ���� ũ�⸦ �����Ѵ�
            Vector3 originalScale = _uiErrorPannel.gameObject.transform.localScale;
            // DoTween�� ����Ͽ� ũ�⸦ 0���� ���� ũ��� �����ϴ� �ִϸ��̼��� �����Ѵ�
            _uiErrorPannel.gameObject.transform.localScale = Vector3.zero;
            _uiErrorPannel.gameObject.transform.DOScale(originalScale, 0.25f).SetEase(Ease.InSine);

            _uiErrorPannel.GetComponent<UI_ErrorPannel>().SetMessageBoxText("�α��� ����", ex.Message);            
        }
        finally
        {            
            _uiLoadingPannel.gameObject.SetActive(false);
        }
    }
}
