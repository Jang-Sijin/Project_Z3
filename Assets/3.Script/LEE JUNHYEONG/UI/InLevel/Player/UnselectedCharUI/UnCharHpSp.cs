using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.UI;

/*
 * <선택되지 않은 캐릭터용>
 Hp Sp 슬라이더를 컨트롤합니다
    Hp Sp 슬라이더는 한 번에 값을 반영하여 변경됩니다.
 */

public class UnCharHpSp : MonoBehaviour
{
    [SerializeField]private Slider hpBar;
    [SerializeField]private Slider spBar;
    [SerializeField]private Slider spPointer;
    [SerializeField] private Image profile;

    /// <summary>
    /// 스테이지 시작시 Init
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
    /// 캐릭터 변경시 업데이트
    /// </summary>
    /// <param name="playerModel"></param>
    public void Refresh_Hpbar(PlayerModel playerModel) // 피 업데이트
    {
        hpBar.value = playerModel.playerStatus.CurrentHealth / playerModel.playerStatus.MaxHealth;
    }

    public void Refresh_Spbar(PlayerModel playerModel) // Sp 업데이트
    {
        spBar.value = playerModel.playerStatus.CurrentSkillPoint / playerModel.playerStatus.MaxSkillPoint;
    }
}
