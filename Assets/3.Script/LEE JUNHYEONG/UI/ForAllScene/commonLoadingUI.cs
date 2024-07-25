using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CommonLoadingUI : MonoBehaviour // 로딩 씬을 제어하는 스크립트로 로딩 UI에 컴포넌트로 부착합니다.
{
    [SerializeField] private Sprite[] backGroundSprites;
    [SerializeField] private Image backGroundIMG;
    [SerializeField] private TextMeshProUGUI help;
    [SerializeField] private Sprite[] Profiles;
    [SerializeField] private Image profile;
    [SerializeField] private GameObject endLoadingText;

    private string[] helps =
    {
        "[레전드 로프꾼]인 벨은 젠레스 존 제로의 플레이어블 주인공입니다. 6단지에서 비디오 가게 [Random Play]를 운영하고 있으며 로프꾼의 신분으로 공동과 관련된 임무를 수행하기도 합니다. 한때 임무를 진행하던 중 [파에톤] 계정을 잃어버려 새로 만든 로프넷 계정을 다시 운영하고 있습니다.",
        "[엔비 데마라]: 교활한 토끼굴의 유일한 1기 직원입니다. 상식은 부족하지만, 전투에서만큼은 교활한 토끼굴의 최강 전력 중 하나라고 할 수 있습니다.",
        "[코린 위크스]: 빅토리아 하우스키핑의 메이드 중 한 명입니다. 매우 착한 메이드인데 자신감이 많이 부족하고 다른 사람들에게 미움을 받을까 봐 두려워 합니다.  항상 허둥지둥하는 모습이고 급할 때 말을 더듬기도 합니다.",
        "[11호]: 뉴에리두 방위군의 일원입니다. 현재 흑요석 부대의 오볼로스 소대에 소속되어 있으며, 메인 공격수를 담당하고 있습니다. [11호]는 부대에서의 코드네임으로 본명은 확인되지 않고 있습니다."
    };

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManagerEx.Instance.LoadScene(Define.SceneType.Town);
        }
    }

    private void Start()
    {
        int index = Random.Range(0, helps.Length);

        profile.sprite = Profiles[index];
        help.text = helps[index];

        backGroundIMG.sprite = backGroundSprites[Random.Range(0, backGroundSprites.Length)];
    }

    public void ActivateEndText()
    {
        endLoadingText.SetActive(true);
    }
}
