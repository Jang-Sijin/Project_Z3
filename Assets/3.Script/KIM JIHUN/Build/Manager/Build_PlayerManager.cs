using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Define;

public class Build_PlayerManager : SingleMonoBase<Build_PlayerManager>
{
    private PlayerInfo _corin;
    private PlayerInfo _longinus;
    private PlayerInfo _anbi;

    public PlayerInfo Corin => _corin;
    public PlayerInfo Longinus => _longinus;
    public PlayerInfo Anbi => _anbi;


    public int currentExp;
    public int playerLevel = 1;

    protected override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(INSTANCE);
    }

    private void Start()
    {
        _corin = new PlayerInfo(ECharacter.Corin);
        _longinus = new PlayerInfo(ECharacter.Longinus);
        _anbi = new PlayerInfo(ECharacter.Anbi);
    }

    private void EarnExp(int earningExp)
    {
        currentExp += earningExp;

        // 각 레벨 * 1000이 레벨당 최대 경험치 (ex: 1->2렙은 1000, 2->3 2000경험치 필요)
        if (currentExp >= playerLevel * 1000)
        {
            currentExp -= (playerLevel * 1000);
            playerLevel += 1;
        }
    }
}

public class PlayerInfo
{
    public ECharacter _characterName;
    private Build_Item _equipment;

    public Build_Item Equipment => _equipment;

    public PlayerInfo(ECharacter characterName)
    {
        this._characterName = characterName;
        _equipment = null;
    }

    public void EquipItem(Build_Item itemToEquip)
    {
        if (_equipment != null) // 아미 무기를 착용하고 있음 -> 착용중인 무기는 인벤토리로, 새로운 무기는 Equipment로
        {
            Build_InventoryManager.INSTANCE.AddToInventory(_equipment);
        }
        _equipment = itemToEquip;
        Build_InventoryManager.INSTANCE.RemoveFromInventory(itemToEquip);
    }

    public void UnequipItem()
    {
        Build_InventoryManager.INSTANCE.AddToInventory(_equipment);
        _equipment = null;
    }
}
