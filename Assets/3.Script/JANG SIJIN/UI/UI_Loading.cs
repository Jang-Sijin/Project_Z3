using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_Loading : MonoBehaviour
{
    [SerializeField] private Image _helpChracterProfileImage;
    [SerializeField] private TextMeshProUGUI _helpTextMesh;
    [SerializeField] private RectMask2D _helpRectMask;    


    [SerializeField] private Sprite[] _helpCharacterImages;
    private string[] helpTexts =
    {
        "[������ ������]�� ���� ������ �� ������ �÷��̾�� ���ΰ��Դϴ�. 6�������� ���� ���� [Random Play]�� ��ϰ� ������ �������� �ź����� ������ ���õ� �ӹ��� �����ϱ⵵ �մϴ�. �Ѷ� �ӹ��� �����ϴ� �� [�Ŀ���] ������ �Ҿ���� ���� ���� ������ ������ �ٽ� ��ϰ� �ֽ��ϴ�.",
        "[���� ������]: ��Ȱ�� �䳢���� ������ 1�� �����Դϴ�. ����� ����������, ����������ŭ�� ��Ȱ�� �䳢���� �ְ� ���� �� �ϳ���� �� �� �ֽ��ϴ�.",
        "[�ڸ� ��ũ��]: ���丮�� �Ͽ콺Ű���� ���̵� �� �� ���Դϴ�. �ſ� ���� ���̵��ε� �ڽŰ��� ���� �����ϰ� �ٸ� ����鿡�� �̿��� ������ �� �η��� �մϴ�.  �׻� ��������ϴ� ����̰� ���� �� ���� ����⵵ �մϴ�.",
        "[11ȣ]: �������� �������� �Ͽ��Դϴ�. ���� ��伮 �δ��� �����ν� �Ҵ뿡 �ҼӵǾ� ������, ���� ���ݼ��� ����ϰ� �ֽ��ϴ�. [11ȣ]�� �δ뿡���� �ڵ�������� ������ Ȯ�ε��� �ʰ� �ֽ��ϴ�."
    };

    public void ShowUI()
    {                
        int randomIndex = Random.Range(0, 4);        

        _helpChracterProfileImage.sprite = _helpCharacterImages[randomIndex];
        _helpTextMesh.text = helpTexts[randomIndex];
        _helpRectMask.padding = new Vector4(0, 0, 0, 0);
        gameObject.SetActive(true);


        if (BelleController.INSTANCE != null)
            BelleController.INSTANCE.CanInput = false;
        if (PlayerController.INSTANCE != null)
            PlayerController.INSTANCE.CanInput = false;

        // ���� �е��� ��������        
        DOTween.To(() => _helpRectMask.padding.z, z =>
        {
            _helpRectMask.padding = new Vector4(_helpRectMask.padding.x, _helpRectMask.padding.y, z, _helpRectMask.padding.w);
        }, 0, 1f);
    }

    public void HideUI()
    {                
        // ���� �е��� ��������        
        DOTween.To(() => _helpRectMask.padding.x, x =>
        {
            _helpRectMask.padding = new Vector4(x, _helpRectMask.padding.y, _helpRectMask.padding.z, _helpRectMask.padding.w);
        }, 1920f, 1f)
            .OnComplete(() =>
        {

            if (BelleController.INSTANCE != null)
                BelleController.INSTANCE.CanInput = true;
            if (PlayerController.INSTANCE != null)
                PlayerController.INSTANCE.CanInput = true;

            gameObject.SetActive(false);
        });
    }
}
