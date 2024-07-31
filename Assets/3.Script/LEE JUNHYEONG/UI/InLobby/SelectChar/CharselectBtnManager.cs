using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class CharselectBtnManager : MonoBehaviour
{
    private SelectbtnEff prevCharBtn; // ĳ���� ���� ĳ��
    public SelectbtnEff PrevCharBtn { get { return prevCharBtn; } }

    private MainCityMenuUIManager mainCityMenuUIManager; // ��� ���� �޴��� �޴���
    [SerializeField] private GameObject EquipmentUI; // ��� UI
    [SerializeField] private GameObject StatUI; // ���� UI
    [SerializeField] private GameObject RawImage; // ĳ���� �̹���
    private void OnEnable()
    {
        RawImage.SetActive(false);
    }

    private void Start()
    {
        mainCityMenuUIManager = GetComponentInParent<MainCityMenuUIManager>();
    }


    public void ClickCharBtn(SelectbtnEff clickedBtn) // ĳ���� ���� ��ư �޼ҵ�
    {
        RawImage.SetActive(true);
        if (clickedBtn == prevCharBtn)
            return;

        if (prevCharBtn != null)
            prevCharBtn.TurnOff();

        clickedBtn.OnClickButton();
        prevCharBtn = clickedBtn;
    }

    public void OnClickStatUIBTN() // ���� UI �޼ҵ� �ƹ� ĳ�� ���� �� ���� �� �ȵ���
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