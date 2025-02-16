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
    [SerializeField] private Image _captureImage;


    [SerializeField] private Sprite[] _helpCharacterImages;
    private string[] helpTexts =
    {
        "[레전드 로프꾼]인 벨은 젠레스 존 제로의 플레이어블 주인공입니다. 6단지에서 비디오 가게 [Random Play]를 운영하고 있으며 로프꾼의 신분으로 공동과 관련된 임무를 수행하기도 합니다. 한때 임무를 진행하던 중 [파에톤] 계정을 잃어버려 새로 만든 로프넷 계정을 다시 운영하고 있습니다.",
        "[엔비 데마라]: 교활한 토끼굴의 유일한 1기 직원입니다. 상식은 부족하지만, 전투에서만큼은 교활한 토끼굴의 최강 전력 중 하나라고 할 수 있습니다.",
        "[코린 위크스]: 빅토리아 하우스키핑의 메이드 중 한 명입니다. 매우 착한 메이드인데 자신감이 많이 부족하고 다른 사람들에게 미움을 받을까 봐 두려워 합니다.  항상 허둥지둥하는 모습이고 급할 때 말을 더듬기도 합니다.",
        "[11호]: 뉴에리두 방위군의 일원입니다. 현재 흑요석 부대의 오볼로스 소대에 소속되어 있으며, 메인 공격수를 담당하고 있습니다. [11호]는 부대에서의 코드네임으로 본명은 확인되지 않고 있습니다."
    };

    public void ShowUI()
    {
        StartCoroutine(CaptureCoroutine());
        _captureImage.gameObject.SetActive(true);


        int randomIndex = Random.Range(0, 4);        

        _helpChracterProfileImage.sprite = _helpCharacterImages[randomIndex];
        _helpTextMesh.text = helpTexts[randomIndex];
        _helpRectMask.padding = new Vector4(0, 0, 1920, 0);
        gameObject.SetActive(true);


        if (BelleController.INSTANCE != null)
            BelleController.INSTANCE.CanInput = false;
        if (PlayerController.INSTANCE != null)
            PlayerController.INSTANCE.CanInput = false;

        // 현재 패딩을 가져오기        
        DOTween.To(() => _helpRectMask.padding.z, z =>
        {
            _helpRectMask.padding = new Vector4(_helpRectMask.padding.x, _helpRectMask.padding.y, z, _helpRectMask.padding.w);
        }, 0, 1f);
    }

    public void HideUI()
    {        
        _captureImage.gameObject.SetActive(false);

        // 현재 패딩을 가져오기        
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

    private IEnumerator CaptureCoroutine()
    {
        Camera captureCamera = Camera.main;

        yield return new WaitForEndOfFrame();

        // 현재 화면 크기 가져오기
        int width = Screen.width;
        int height = Screen.height;

        // RenderTexture 생성
        RenderTexture renderTexture = new RenderTexture(width, height, 24);
        captureCamera.targetTexture = renderTexture;
        captureCamera.Render();

        // Texture2D로 변환
        Texture2D screenTexture = new Texture2D(width, height, TextureFormat.RGB24, false);
        RenderTexture.active = renderTexture;
        screenTexture.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        screenTexture.Apply();

        // 리소스 정리
        captureCamera.targetTexture = null;
        RenderTexture.active = null;
        Destroy(renderTexture);

        // Texture2D를 Sprite로 변환
        Sprite capturedSprite = Sprite.Create(screenTexture, new Rect(0, 0, width, height), new Vector2(0.5f, 0.5f));

        // UI Image에 적용
        _captureImage.sprite = capturedSprite;
    }
}
