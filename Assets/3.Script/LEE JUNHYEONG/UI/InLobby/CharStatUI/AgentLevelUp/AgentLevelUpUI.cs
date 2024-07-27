using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class AgentLevelUpUI : MonoBehaviour
{
    #region �����ؽ�Ʈ

    //**************************************************************************************************************
    // index -> 0 �� ���� ������ index -> 1�� ������ �� ��ġ��
    [SerializeField] private TextMeshProUGUI[] healthText; // ü�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI[] damageText; // ������ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI[] defenceText; // ���� �ؽ�Ʈ
    //**************************************************************************************************************


    [SerializeField] private TextMeshProUGUI curLevelText; // ���緹�� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI nextLevelText; // �������� �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI amountOfEXP; // (���� ����ġ / ���� ���� ����ġ) ����ġ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI amountOfItemA; // A��ũ ����ġ ������ �ؽ�Ʈ
    [SerializeField] private TextMeshProUGUI amountOfItemS; // S��ũ ����ġ ������ �ؽ�Ʈ

    private float maxEXP = 600f; // ������ : �÷��̾� �ִ� ����ġ
    private float curEXP = 0f; // ������ : �÷��̾� ���� ����ġ
    private float Armor = 10f; // ������ : �÷��̾� ����
    #endregion

    /*
     * ������ ��� �̸� ����ϴ� �޼ҵ�
     * ������ �̸� ��� �� �̹��� fill�ٲٴ� �޼ҵ�
     * ���� �͵� �����ؾ���
     * 
     * ������ �� ���� �ؽ�Ʈ��
     * ü��, 
     */

    //private void Calculate()
}
