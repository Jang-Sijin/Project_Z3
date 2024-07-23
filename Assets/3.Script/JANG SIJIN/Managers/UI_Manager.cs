using UnityEngine;
using System.Collections;
using Unity.VisualScripting;

public class UI_Manager : SingletonBase<UI_Manager>
{
    public GameObject Prefab_Option;

    public UI_Option UIOption;


    #region Unity Event
    private void Start()
    {
        // 1. 각 UI 컨트롤 스크립트 할당 작업 진행
        UIOption = Prefab_Option.GetOrAddComponent<UI_Option>();


        // 2. 프리팹 켜져있을 경우: 비활성화
        if (Prefab_Option.activeSelf)
        {
            Prefab_Option.SetActive(false);
        }
    }

    private void Update()
    {

    }
    #endregion



    #region Public Methods    
    public void OpenOptionUI()
    {
        // 옵션UI 오브젝트 활성화
        Prefab_Option.SetActive(true);
    }

    public void CloseOptionUI()
    {
        // 옵션UI 오브젝트 비활성화
        Prefab_Option.SetActive(false);
    }

    public void OnClickButtonGameExit()
    {
        Application.Quit();
    }
    #endregion

    #region Private Methods
    private void OnPressDownButtonOption()
    {
        // ESC 키를 눌렀을 경우 옵션UI 출력
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // UIOption.
        }        
    }
    #endregion
}