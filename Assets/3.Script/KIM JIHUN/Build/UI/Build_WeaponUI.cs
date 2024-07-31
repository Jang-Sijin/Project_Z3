using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Build_WeaponUI : MonoBehaviour
{
    [SerializeField] private ECharacter _selectedCharacter;

    [Header("Equiped Weapon")]
    [SerializeField] private Image _equipedWeaponIMG;

    [Header("Inventory List")]
    [SerializeField] private Build_WeaponSlotUI[] itemSlots;
    [Header("Sprite")]
    [SerializeField] private Sprite[] _rankSprite;

    [Header("WeaponPreview")]
    private Build_Item _selectedItemSlot;
    [SerializeField] private GameObject _previewWindow;
    [SerializeField] private TextMeshProUGUI _preWeaponName;
    [SerializeField] private Image _preWeaponIMG;
    [SerializeField] private Image _preWeaponRankIMG;
    [SerializeField] private TextMeshProUGUI _preWeaponAttack;
    [SerializeField] private TextMeshProUGUI _preWeaponDefence;
    [SerializeField] private TextMeshProUGUI _preWeaponHealth;


    [Header("WeaponInfo")]
    [SerializeField] private TextMeshProUGUI _weaponName;
    [SerializeField] private Image _weaponRankIMG;
    [SerializeField] private TextMeshProUGUI _weaponAttack;
    [SerializeField] private TextMeshProUGUI _weaponDefence;
    [SerializeField] private TextMeshProUGUI _weaponHealth;

    public void OpenWeaponUI(ECharacter selectedCharacter)
    {
        if (selectedCharacter == ECharacter.None) return;
        _selectedCharacter = selectedCharacter;
        gameObject.SetActive(true);
        RefreshInventory();
        CloseItemPreview();
    }

    public void CloseWeaponUI()
    {
        gameObject.SetActive(false);
    }

    public void ShowEquipedWeapon()
    {
        _equipedWeaponIMG.gameObject.SetActive(false);
        Build_Item tmp = null;
        switch (_selectedCharacter)
        {
            case ECharacter.Anbi:
                if (Build_PlayerManager.INSTANCE.Anbi.Equipment != null)
                {
                    tmp = Build_PlayerManager.INSTANCE.Anbi.Equipment;
                    _equipedWeaponIMG.gameObject.SetActive(true);
                }
                break;
            case ECharacter.Corin:
                if (Build_PlayerManager.INSTANCE.Corin.Equipment != null)
                {
                    tmp = Build_PlayerManager.INSTANCE.Corin.Equipment;
                    _equipedWeaponIMG.gameObject.SetActive(true);
                }
                break;
            case ECharacter.Longinus:
                if (Build_PlayerManager.INSTANCE.Longinus.Equipment != null)
                {
                    tmp = Build_PlayerManager.INSTANCE.Longinus.Equipment;
                    _equipedWeaponIMG.gameObject.SetActive(true);
                }
                break;
        }

        if (tmp != null)
        {
            _equipedWeaponIMG.sprite = tmp.itemIcon;
            _equipedWeaponIMG.SetNativeSize();
            _weaponName.text = tmp.itemName;
            _weaponRankIMG.gameObject.SetActive(true);
            _weaponRankIMG.sprite = _rankSprite[(int)tmp.itemRank];
            _weaponAttack.text = tmp.attackStat.ToString();
            _weaponDefence.text = tmp.defenceStat.ToString();
            _weaponHealth.text = tmp.healthStat.ToString();
        }
        else
        {
            _equipedWeaponIMG.sprite = null;
            _weaponName.text = "404 NOT FOUND";
            _weaponRankIMG.gameObject.SetActive(false);
            _weaponAttack.text = "0";
            _weaponDefence.text = "0";
            _weaponHealth.text = "0";
        }
    }

    public void RefreshInventory()
    {
        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].gameObject.SetActive(false);
        }

        int weaponInventoryCount = Build_InventoryManager.INSTANCE.WeaponInventory.Inventory.Count;

        for (int i = 0; i < weaponInventoryCount; i++)
        {
            Debug.Log(i);
            itemSlots[i].gameObject.SetActive(true);
            itemSlots[i].RefreshSlot(Build_InventoryManager.INSTANCE.WeaponInventory.Inventory[i]);
        }
        ShowEquipedWeapon();
    }

    public void ShowItemPreview(Build_Item itemSlot)
    {
        if (_selectedItemSlot == itemSlot) CloseItemPreview();
        else
        {
            _selectedItemSlot = itemSlot;
            _previewWindow.SetActive(true);
            _preWeaponName.text = itemSlot.itemName;
            _preWeaponIMG.sprite = itemSlot.itemIcon;
            _preWeaponIMG.SetNativeSize();
            _preWeaponRankIMG.sprite = _rankSprite[(int)itemSlot.itemRank];
            _preWeaponAttack.text = itemSlot.attackStat.ToString();
            _preWeaponDefence.text = itemSlot.defenceStat.ToString();
            _preWeaponHealth.text = itemSlot.healthStat.ToString();
        }
    }

    public void CloseItemPreview()
    {
        _selectedItemSlot = null;
        _previewWindow.SetActive(false);
    }

    public void EquipWeapon()
    {
        if (_selectedItemSlot == null) return;
        switch (_selectedCharacter)
        {
            case ECharacter.Anbi:
                Build_PlayerManager.INSTANCE.Anbi.EquipItem(_selectedItemSlot);
                Debug.Log(Build_PlayerManager.INSTANCE.Anbi.Equipment == null);
                break;
            case ECharacter.Corin:
                Build_PlayerManager.INSTANCE.Corin.EquipItem(_selectedItemSlot);
                break;
            case ECharacter.Longinus:
                Build_PlayerManager.INSTANCE.Longinus.EquipItem(_selectedItemSlot);
                break;
        }
        CloseItemPreview();
        RefreshInventory();
    }

    public void UnequipWeapon()
    {
        switch (_selectedCharacter)
        {
            case ECharacter.Anbi:
                if (Build_PlayerManager.INSTANCE.Anbi.Equipment == null)
                    return;
                Build_PlayerManager.INSTANCE.Anbi.UnequipItem();
                break;
            case ECharacter.Corin:
                if (Build_PlayerManager.INSTANCE.Corin.Equipment == null)
                    return;
                Build_PlayerManager.INSTANCE.Corin.UnequipItem();
                break;
            case ECharacter.Longinus:
                if (Build_PlayerManager.INSTANCE.Longinus.Equipment == null)
                    return;
                Build_PlayerManager.INSTANCE.Longinus.UnequipItem();
                break;
        }
        RefreshInventory();
    }
}
