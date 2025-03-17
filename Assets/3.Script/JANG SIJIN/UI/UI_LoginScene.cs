using Cysharp.Threading.Tasks;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_LoginScene : MonoBehaviour
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
        _loginUserButton.onClick.AddListener(() => OnClickLoginButton().Forget());
    }

    private void OnClickRegistUserButton()
    {                
        if (_registUserPannel != null)
        {
            _registUserPannel.GetComponent<UI_RegistUser>().OpenUI();            
        }
    }

    private async UniTask OnClickLoginButton()
    {
        _uiLoadingPannel.gameObject.SetActive(true);

        SceneManagerEx.Instance.LoadScene(Define.SceneType.Home);

        //try
        //{
        //    await APIManager.Instance.Login(_emailInputField.text, _passwordInputField.text);

        //    SceneManagerEx.Instance.LoadScene(Define.SceneType.Home);
        //}
        //catch (Exception ex)
        //{           
        //    _uiErrorPannel.GetComponent<UI_ConfirmPannel>().ShowMessageBoxText("로그인 실패", ex.Message);            
        //}
        //finally
        //{            
        //    _uiLoadingPannel.gameObject.SetActive(false);
        //}
    }
}
