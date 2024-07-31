using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShowItemInfo : MonoBehaviour
{
    public TMP_Text itemUIText; // UI에 표시될 텍스트
    public Image itemUIImage; // UI에 표시될 이미지
    public float displayDuration = 1.5f; // 아이템 정보가 표시되는 시간

    void Start()
    {
        // 자식 개체에서 텍스트와 이미지를 가져옵니다. 인스펙터에서 할당할 경우 필요하지 않습니다.
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
        Debug.Log("UpdateUI 호출"); // 디버그 로그 추가
        if (itemUIText != null)
        {
            itemUIText.text = item.name_ + " 획득"; // 아이템 이름을 텍스트에 설정
        }

        if (itemUIImage != null)
        {
            itemUIImage.sprite = item.itemIcon;
            itemUIImage.enabled = true; // 이미지를 활성화
        }

        // UI 활성화
        gameObject.SetActive(true);

        // 일정 시간 후 UI 비활성화
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
        // 텍스트와 이미지를 초기화
        if (itemUIText != null)
        {
            itemUIText.text = string.Empty; // 텍스트를 빈 문자열로 초기화
        }

        if (itemUIImage != null)
        {
            itemUIImage.sprite = null; // 아이콘 이미지를 null로 설정
           // itemUIImage.color = new Color(1, 1, 1, 0); // 이미지의 알파 값을 0으로 설정하여 보이지 않도록 함
        }

        // UI 비활성화
        gameObject.SetActive(false);
    }
}