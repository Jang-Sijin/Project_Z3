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
        // 1. �� UI ��Ʈ�� ��ũ��Ʈ �Ҵ� �۾� ����
        UIOption = Prefab_Option.GetOrAddComponent<UI_Option>();


        // 2. ������ �������� ���: ��Ȱ��ȭ
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
        // �ɼ�UI ������Ʈ Ȱ��ȭ
        Prefab_Option.SetActive(true);
    }

    public void CloseOptionUI()
    {
        // �ɼ�UI ������Ʈ ��Ȱ��ȭ
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
        // ESC Ű�� ������ ��� �ɼ�UI ���
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // UIOption.
        }        
    }
    #endregion
}