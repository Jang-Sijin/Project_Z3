using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public enum TempChar
{ 
    ANBY = 0,
    SOUKAKU = 1,
    CORIN =2
};

public class InGameUI : MonoBehaviour
{
    /* key �Է¿� ���� ���� ȿ������ ��������
     �� ���ӿ��� �۵��ϴ� UI Ű ��� ��������
ESC -Pause
���콺 (��) - �Ϲݰ���
ȿ�� ����

   "      (��) - �뽬
	�������� �� �� �� ������ �뽬 ���� �뽬�� ��Ÿ���� �ִ�
	���� �� ���� �ȉ�

 E              - ��ų
���� ���� �Ѿ��ϴ� ����Ʈ�� ���� - ���� ���ϸ��̼� ����

Space 	  -  ����ġ
����ġ�� ������ ���ʿ��� ������ ������ �ٲ��
������ �÷��̾ �� ������ �ٲ� �� �ִ�.
     */

    /*
     * ĳ������ ���� ��ġ�� �ݿ��Ϸ��� �Ʒ� �������� �ҷ��� ����ϸ� �˴ϴ�.
     */
    [SerializeField] private SelectedChar selectedChar;
    [SerializeField] private UnCharHpSp unCharHpSp1;
    [SerializeField] private UnCharHpSp unCharHpSp2;

    private void Update()
    {
        OpenAndClosePause();
    }

    private void OpenAndClosePause()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !UIManager.instance.isCloseOrOpen)
        {
            if (UIManager.instance.isPause)
            {
                UIManager.instance.pauseMenuUI.OnClickClose();
            }

            else
            {
                UIManager.instance.pauseMenuUI.gameObject.SetActive(true);
                StartCoroutine(UIManager.instance.pauseMenuUI.CallPauseMenu_co());
            }
        }
    }

    public void ChangeChar(TempChar selectedChar, TempChar unselectedChar1, TempChar unselectedChar2)
    {
        /*
         * ���� ĳ������ �����͸� ���� �ͼ�
         */


    }
}
