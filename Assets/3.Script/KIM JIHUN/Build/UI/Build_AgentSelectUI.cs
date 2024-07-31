using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class Build_AgentSelectUI : MonoBehaviour
{
    [SerializeField] private GameObject _agentSelectObj;
    private ECharacter selectedCharacter;
    public ECharacter SelectedCharacter => selectedCharacter;

    [SerializeField] private RawImage _playerPreviewRawImage;
    [SerializeField] private GameObject[] _playerBTNImage;


    [Header("캐릭터 모델 오브젝트")]
    [SerializeField] private GameObject _anbiModel;
    [SerializeField] private GameObject _corinModel;
    [SerializeField] private GameObject _longinus;

    public void OpenAgentSelectUI()
    {
        selectedCharacter = ECharacter.None;
        foreach (var item in _playerBTNImage)
        {
            item.SetActive(false);
        }
        _playerPreviewRawImage.gameObject.SetActive(false);
        _agentSelectObj.SetActive(true);
    }

    public void CloseAgentSelectUI()
    {
        _agentSelectObj.SetActive(false);
    }

    public void ChangeCharacter(int index)
    {
        _playerPreviewRawImage.gameObject.SetActive(true);
        switch (index)
        {
            case 0:
                selectedCharacter = ECharacter.Anbi;
                ShowCharacter();
                break;
            case 1:
                selectedCharacter = ECharacter.Corin;
                ShowCharacter();
                break;
            case 2:
                selectedCharacter = ECharacter.Longinus;
                ShowCharacter();
                break;
        }
    }

    public void ShowCharacter()
    {
        _anbiModel.SetActive(false);
        _corinModel.SetActive(false);
        _longinus.SetActive(false);

        foreach (var item in _playerBTNImage)
        {
            item.SetActive(false);
        }

        switch (selectedCharacter)
        {
            case ECharacter.Anbi:
                _playerBTNImage[0].SetActive(true);
                _anbiModel.SetActive(true);
                break;
            case ECharacter.Corin:
                _playerBTNImage[1].SetActive(true);
                _corinModel.SetActive(true);
                break;
            case ECharacter.Longinus:
                _playerBTNImage[2].SetActive(true);
                _longinus.SetActive(true);
                break;
        }
    }

    public void OpenCharacterStatusUI()
    {
        if (selectedCharacter == ECharacter.None) return;
        UIManager.Instance.CharacterStatusUI.OpenCharacterStatusUI(selectedCharacter);
        gameObject.SetActive(false);
    }
}
