using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CommonLoadingUI : MonoBehaviour // �ε� ���� �����ϴ� ��ũ��Ʈ�� �ε� UI�� ������Ʈ�� �����մϴ�.
{
    [SerializeField] private Sprite[] backGroundSprites;
    [SerializeField] private Image backGroundIMG;
    [SerializeField] private TextMeshProUGUI help;
    [SerializeField] private Sprite[] Profiles;
    [SerializeField] private Image profile;
    [SerializeField] private GameObject endLoadingText;

    private string[] helps =
    {
        "[������ ������]�� ���� ������ �� ������ �÷��̾�� ���ΰ��Դϴ�. 6�������� ���� ���� [Random Play]�� ��ϰ� ������ �������� �ź����� ������ ���õ� �ӹ��� �����ϱ⵵ �մϴ�. �Ѷ� �ӹ��� �����ϴ� �� [�Ŀ���] ������ �Ҿ���� ���� ���� ������ ������ �ٽ� ��ϰ� �ֽ��ϴ�.",
        "[���� ������]: ��Ȱ�� �䳢���� ������ 1�� �����Դϴ�. ����� ����������, ����������ŭ�� ��Ȱ�� �䳢���� �ְ� ���� �� �ϳ���� �� �� �ֽ��ϴ�.",
        "[�ڸ� ��ũ��]: ���丮�� �Ͽ콺Ű���� ���̵� �� �� ���Դϴ�. �ſ� ���� ���̵��ε� �ڽŰ��� ���� �����ϰ� �ٸ� ����鿡�� �̿��� ������ �� �η��� �մϴ�.  �׻� ��������ϴ� ����̰� ���� �� ���� ����⵵ �մϴ�.",
        "[11ȣ]: �������� �������� �Ͽ��Դϴ�. ���� ��伮 �δ��� �����ν� �Ҵ뿡 �ҼӵǾ� ������, ���� ���ݼ��� ����ϰ� �ֽ��ϴ�. [11ȣ]�� �δ뿡���� �ڵ�������� ������ Ȯ�ε��� �ʰ� �ֽ��ϴ�."
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
