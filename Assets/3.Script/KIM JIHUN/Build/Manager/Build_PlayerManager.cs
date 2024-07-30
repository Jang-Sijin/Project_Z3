using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Build_PlayerManager : SingleMonoBase<Build_PlayerManager>
{
    private PlayerInfo _corin;
    private PlayerInfo _longinus;
    private PlayerInfo _anbi;

    public PlayerInfo Corin => _corin;
    public PlayerInfo Longinus => _longinus;
    public PlayerInfo Anbi => _anbi;
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
}

public class PlayerInfo
{
    public ECharacter _characterName;
    private int _playerLevel;
    private Build_ItemSlot _equipment;
    private int _itemLevel;

    public Build_ItemSlot Equipment => _equipment;

    public PlayerInfo(ECharacter characterName)
    {
        this._characterName = characterName;
        _equipment = null;
        _playerLevel = 1;
    }

    public void EquipItem(Build_ItemSlot itemToEquip)
    {
        _equipment = itemToEquip;
    }

    public void UnequipItem()
    {
        _equipment = null;
        _itemLevel = 0;
    }
}
