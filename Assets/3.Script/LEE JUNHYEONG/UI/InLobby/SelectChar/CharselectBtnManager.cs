using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharselectBtnManager : MonoBehaviour
{
    private SelectbtnEff prevCharBtn; // 캐릭터 선택 캐싱
    public SelectbtnEff PrevCharBtn { get { return prevCharBtn; } }

    private MainCityMenuUIManager mainCityMenuUIManager; // 모든 메인 메뉴의 메니저
    [SerializeField] private GameObject EquipmentUI; // 장비 UI
    [SerializeField] private GameObject StatUI; // 스탯 UI
    [SerializeField] private GameObject RawImage; // 캐릭터 이미지
    private void OnEnable()
    {
        RawImage.SetActive(false);
    }

    private void Start()
    {
        mainCityMenuUIManager = GetComponentInParent<MainCityMenuUIManager>();
    }


    public void ClickCharBtn(SelectbtnEff clickedBtn) // 캐릭터 선택 버튼 메소드
    {
        RawImage.SetActive(true);
        if (clickedBtn == prevCharBtn)
            return;

        if (prevCharBtn != null)
            prevCharBtn.TurnOff();

        clickedBtn.OnClickButton();
        prevCharBtn = clickedBtn;
    }

    public void OnClickStatUIBTN() // 스탯 UI 메소드 아무 캐릭 선택 안 했을 시 안들어가짐
    {
        if (PrevCharBtn == null)
        {
            return;
        }

        mainCityMenuUIManager.ChangeToOtherMenuEFF();
        StatUI.SetActive(true);
        mainCityMenuUIManager.emenuState = MainCityMenuUIManager.EMenuState.CharStatMenu;
    }

    private void OnDisable()
    {
        if (prevCharBtn == null)
            return;

        prevCharBtn.TurnOff();
        prevCharBtn.AniIMG.enabled = false;
        prevCharBtn = null;
    }
}