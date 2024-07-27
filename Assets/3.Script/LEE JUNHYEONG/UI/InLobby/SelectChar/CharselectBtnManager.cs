using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class CharselectBtnManager : MonoBehaviour
{
    private SelectbtnEff prevCharBtn; // ĳ���� ���� ĳ��
    public SelectbtnEff PrevCharBtn {  get { return prevCharBtn; } }

    [SerializeField] private MainCityMenuUIManager mainCityMenuUIManager; // ��� ���� �޴��� �޴���
    [SerializeField] private GameObject EquipmentUI; // ��� UI
    [SerializeField] private GameObject StatUI; // ���� UI
    [SerializeField] private TextMeshPro selectCharNameText; // ���� UI���� ���õ� ĳ������ �̸� ��� ������Ʈ
    [SerializeField] private TextMeshPro selectCharLevelText; // ���� UI ĳ���� ����
    [SerializeField] private TextMeshPro selectCharNextLevelText; // ���� UI ĳ���� ����
    

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