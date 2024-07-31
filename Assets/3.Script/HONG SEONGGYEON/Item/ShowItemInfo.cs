using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowItemInfo : MonoBehaviour
{
    public TMP_Text itemUIText; // UI�� ǥ�õ� �ؽ�Ʈ
    public Image itemUIImage; // UI�� ǥ�õ� �̹���
    public float displayDuration = 1.5f; // ������ ������ ǥ�õǴ� �ð�

    void Start()
    {
        // �ڽ� ��ü���� �ؽ�Ʈ�� �̹����� �����ɴϴ�. �ν����Ϳ��� �Ҵ��� ��� �ʿ����� �ʽ��ϴ�.
        if (itemUIImage == null)
        {
            itemUIImage = GetComponentInChildren<Image>();
        }
        if (itemUIText == null)
        {
            itemUIText = GetComponentInChildren<TMP_Text>();
        }
    }

    public void UpdateUI(Item item)
    {
        Debug.Log("UpdateUI ȣ��"); // ����� �α� �߰�
        if (itemUIText != null)
        {
            itemUIText.text = item.name_ + " ȹ��"; // ������ �̸��� �ؽ�Ʈ�� ����
        }

        if (itemUIImage != null)
        {
            itemUIImage.sprite = item.itemIcon;
            itemUIImage.enabled = true; // �̹����� Ȱ��ȭ
        }

        // UI Ȱ��ȭ
        gameObject.SetActive(true);

        // ���� �ð� �� UI ��Ȱ��ȭ
        StopAllCoroutines();
        StartCoroutine(HideUIAfterDelay());
    }

    private IEnumerator HideUIAfterDelay()
    {
        yield return new WaitForSeconds(displayDuration);
        HideUI();
    }

    public void HideUI()
    {
        // �ؽ�Ʈ�� �̹����� �ʱ�ȭ
        if (itemUIText != null)
        {
            itemUIText.text = string.Empty; // �ؽ�Ʈ�� �� ���ڿ��� �ʱ�ȭ
        }

        if (itemUIImage != null)
        {
            itemUIImage.sprite = null; // ������ �̹����� null�� ����
           // itemUIImage.color = new Color(1, 1, 1, 0); // �̹����� ���� ���� 0���� �����Ͽ� ������ �ʵ��� ��
        }

        // UI ��Ȱ��ȭ
        gameObject.SetActive(false);
    }
}