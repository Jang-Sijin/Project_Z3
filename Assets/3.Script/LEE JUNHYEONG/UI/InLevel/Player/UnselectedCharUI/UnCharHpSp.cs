using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

/*
 * <���õ��� ���� ĳ���Ϳ�>
 Hp Sp �����̴��� ��Ʈ���մϴ�
    Hp Sp �����̴��� �� ���� ���� �ݿ��Ͽ� ����˴ϴ�.
 */

public class UnCharHpSp : MonoBehaviour
{
    [SerializeField]private Slider hpBar;
    [SerializeField]private Slider spBar;
    [SerializeField]private Slider spPointer;
    [SerializeField] private Image profile;

    /// <summary>
    /// �������� ���۽� Init
    /// </summary>
    /// <param name="playerModel"></param>
    public void AssginCharacter(PlayerModel playerModel, Sprite portraitImg)
    {
        profile.sprite = portraitImg;


        hpBar.value = playerModel.playerStatus.CurrentHealth / playerModel.playerStatus.MaxHealth;
        spBar.value = playerModel.playerStatus.CurrentSkillPoint / playerModel.playerStatus.MaxHealth;
        spPointer.value = 0.5f;
    }


    /// <summary>
    /// ĳ���� ����� ������Ʈ
    /// </summary>
    /// <param name="playerModel"></param>
    public void Refresh_Hpbar(PlayerModel playerModel) // �� ������Ʈ
    {
        hpBar.value = playerModel.playerStatus.CurrentHealth / playerModel.playerStatus.MaxHealth;
    }

    public void Refresh_Spbar(PlayerModel playerModel) // Sp ������Ʈ
    {
        spBar.value = playerModel.playerStatus.CurrentSkillPoint / playerModel.playerStatus.MaxSkillPoint;
    }
}
