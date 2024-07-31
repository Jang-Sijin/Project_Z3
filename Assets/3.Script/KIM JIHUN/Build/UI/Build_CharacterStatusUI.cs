using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Build_CharacterStatusUI : MonoBehaviour
{
    private PlayerInfo selectedCharacter;
    [SerializeField] private CharacterInfo[] _characterData;
    private CharacterInfo _selectedData;

    [Header("팀 로고")]
    [SerializeField] private GameObject _CorinLogo;
    [SerializeField] private GameObject _AnbiLogo;
    [SerializeField] private GameObject _LonginusLogo;

    [Header("장비 UI")]
    [SerializeField] private Image _weaponIMG;
    [SerializeField] private TextMeshProUGUI _weaponNameTXT;
    [SerializeField] private TextMeshProUGUI _weaponAttackTXT;
    [SerializeField] private TextMeshProUGUI _weaponDefenceTXT;
    [SerializeField] private TextMeshProUGUI _weaponHealthTXT;

    [Header("Status UI")]
    [SerializeField] private TextMeshProUGUI _playerLevel;
    [SerializeField] private TextMeshProUGUI _playerExp;
    [SerializeField] private TextMeshProUGUI _playerHealth;
    [SerializeField] private TextMeshProUGUI _playerDefence;
    [SerializeField] private TextMeshProUGUI _playerAttack;

    public void OpenCharacterStatusUI(ECharacter selectedCharacter)
    {
        this.gameObject.SetActive(true);

        _CorinLogo.SetActive(false);
        _AnbiLogo.SetActive(false);
        _LonginusLogo.SetActive(false);

        switch (selectedCharacter)
        {
            case ECharacter.Corin:
                this.selectedCharacter = Build_PlayerManager.INSTANCE.Corin;
                _selectedData = _characterData[0];
                _CorinLogo.SetActive(true);
                break;
            case ECharacter.Anbi:
                this.selectedCharacter = Build_PlayerManager.INSTANCE.Anbi;
                _selectedData = _characterData[1];
                _AnbiLogo.SetActive(true);
                break;
            case ECharacter.Longinus:
                this.selectedCharacter = Build_PlayerManager.INSTANCE.Longinus;
                _selectedData = _characterData[2];
                _LonginusLogo.SetActive(true);
                break;
        }

        //장비 UI 업데이트
        if (this.selectedCharacter.Equipment == null)
        {
            _weaponIMG.gameObject.SetActive(false);
            _weaponNameTXT.text = "404 NOT FOUND";
            _weaponAttackTXT.text = "0";
            _weaponDefenceTXT.text = "0";
            _weaponHealthTXT.text = "0";
        }
        else
        {
            _weaponIMG.gameObject.SetActive(true);
            _weaponIMG.sprite = this.selectedCharacter.Equipment.itemIcon;
            _weaponNameTXT.text = this.selectedCharacter.Equipment.itemName;
            _weaponAttackTXT.text = this.selectedCharacter.Equipment.attackStat.ToString();
            _weaponDefenceTXT.text = this.selectedCharacter.Equipment.defenceStat.ToString();
            _weaponHealthTXT.text = this.selectedCharacter.Equipment.healthStat.ToString();
        }
        // Status UI 업데이트
        _playerLevel.text = $"LV.{Build_PlayerManager.INSTANCE.playerLevel}";
        _playerExp.text = $"{Build_PlayerManager.INSTANCE.currentExp} / {Build_PlayerManager.INSTANCE.playerLevel * 1000}";
        if (this.selectedCharacter.Equipment != null)
        {
            //플레이어 체력, 공격력 수치는 기본값 + (500 * 레벨) + 현재 장비의 값으로 계산
            _playerHealth.text = (_selectedData.maxHealth +
            (500 * Build_PlayerManager.INSTANCE.playerLevel) +
            this.selectedCharacter.Equipment.healthStat).ToString();

            _playerAttack.text = (_selectedData.maxHealth +
            (500 * Build_PlayerManager.INSTANCE.playerLevel) +
            this.selectedCharacter.Equipment.attackStat).ToString();

            //플레이어 방어력 수치는 (100 * 레벨) + 현재 장비의 값으로 계산
            _playerDefence.text = ((100 * Build_PlayerManager.INSTANCE.playerLevel) +
            this.selectedCharacter.Equipment.defenceStat).ToString();

        }
        else
        {
            //플레이어 체력, 공격력 수치는 기본값 + (500 * 레벨) 값으로 계산
            _playerHealth.text = (_selectedData.maxHealth +
            (500 * Build_PlayerManager.INSTANCE.playerLevel)).ToString();

            _playerAttack.text = (_selectedData.maxHealth +
            (500 * Build_PlayerManager.INSTANCE.playerLevel)).ToString();

            //플레이어 방어력 수치는 (100 * 레벨) 값으로 계산
            _playerDefence.text = ((100 * Build_PlayerManager.INSTANCE.playerLevel)).ToString();
        }
    }

    public void CloseCharacterStatusUI()
    {
        gameObject.SetActive(false);
    }
}
