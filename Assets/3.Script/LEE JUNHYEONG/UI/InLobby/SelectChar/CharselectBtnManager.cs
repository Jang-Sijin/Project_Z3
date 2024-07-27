using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharselectBtnManager : MonoBehaviour
{
    private SelectbtnEff prevCharBtn; // 캐릭터 선택 캐싱
    public SelectbtnEff PrevCharBtn {  get { return prevCharBtn; } }

    [SerializeField] private MainCityMenuUIManager mainCityMenuUIManager; // 모든 메인 메뉴의 메니저
    [SerializeField] private GameObject EquipmentUI; // 장비 UI
    [SerializeField] private GameObject StatUI; // 스탯 UI
    [SerializeField] private TextMeshPro selectCharNameText; // 스탯 UI에서 선택된 캐릭터의 이름 출력 오브젝트
    [SerializeField] private TextMeshPro selectCharLevelText; // 스탯 UI 캐릭터 레벨
    [SerializeField] private TextMeshPro selectCharNextLevelText; // 스탯 UI 캐릭터 레벨
    

    public void ClickCharBtn(SelectbtnEff clickedBtn)
    {
        if (clickedBtn == prevCharBtn)
            return;

        if (prevCharBtn != null)
            prevCharBtn.turnOff();

        clickedBtn.OnClickButton();
        prevCharBtn = clickedBtn;
    }

    public void OnClickCharStat()
    {

    }

    public void ClickEquipmentBTN()
    {
        if (prevCharBtn == null) return;
        //mainCityMenuUIManager.TurnOnMenuByOBJ(EquipmentUI);
    }
}