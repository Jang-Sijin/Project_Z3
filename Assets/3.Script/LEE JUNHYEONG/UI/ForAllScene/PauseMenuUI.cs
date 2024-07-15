using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine.Audio;

public class PauseMenuUI : MonoBehaviour
{
	/*
	 * 각 버튼들의 OnClick() 시 불러올 메소드들이 있습니다.
	 */

	private Image[] images; // 종료할 때 fade할 이미지들입니다.

	private void Awake()
	{
        int imagesLength = GetComponentsInChildren<Image>().Length;

        images = new Image[imagesLength];
        images = GetComponentsInChildren<Image>();
    }

	[SerializeField]private Animator startInto_btn_Ani; // 선택된 메뉴창의 애니메이션을 바꾸기 위해 만들었습니다.
	private Animator curInto_btn_Ani;

	[SerializeField]private Image UpperBlackBlank; 
	[SerializeField]private Image UnderBlackBlank;

	[SerializeField] private GameObject displayMenu;
	[SerializeField] private GameObject soundMenu;

    private void OnEnable() // esc누를 시 나올 퍼포먼스가 재생됩니다.
    {
		curInto_btn_Ani = startInto_btn_Ani;
		displayMenu.SetActive(true);
		soundMenu.SetActive(false); // 디스플레이 화면이 항상 먼저 나옵니다.

        curInto_btn_Ani.SetBool("Selected", true);
        for (int i=0;i < images.Length;i++)
		{
			if (images[i].CompareTag("InvisibleButton"))
				continue;

			images[i].DOFade(0f, 0f);
				images[i].DOFade(1, 0.3f).SetEase(Ease.OutQuad);
        }

		UpperBlackBlank.rectTransform.DOAnchorPosY(-70f, 1f).SetEase(Ease.OutQuad); 
		UnderBlackBlank.rectTransform.DOAnchorPosY(70f, 1f).SetEase(Ease.OutQuad);
    }

    public IEnumerator ClosePauseMenu()
	{
		yield return new WaitForSeconds(0.2f);

		FadeImages();

		UpperBlackBlank.rectTransform.DOAnchorPosY(70f, 0.3f);
		UnderBlackBlank.rectTransform.DOAnchorPosY(-70f, 0.3f);

        yield return new WaitForSeconds(0.3f);

        gameObject.SetActive(false);
	}

	private void FadeImages()
	{
        for (int i = 0; i < images.Length; i++)
        {
            if (!images[i].CompareTag("MovingUI"))
                images[i].DOFade(0f, 0.3f).SetEase(Ease.OutQuad);
        }
    }

	public void OnClickClose()
	{
		StartCoroutine(ClosePauseMenu());
	}

	public void OnClickIntoButton(Animator changeCur)
	{
		curInto_btn_Ani.SetBool("Selected", false);
        curInto_btn_Ani.SetTrigger("Normal");

		curInto_btn_Ani = changeCur;
		curInto_btn_Ani.SetBool("Selected", true);
	}

	public void OnclickSoundMenu()
	{
		displayMenu.SetActive(false);
		soundMenu.SetActive(true);
	}

	public void OnClickDisplayMenu()
	{
		soundMenu.SetActive(false);
		displayMenu.SetActive(true);
	}

	[SerializeField]private AudioMixer masterMixer;
	public void OnChangeSliders(Slider slider)
	{
		float value = slider.value;
		masterMixer = SoundManager.instance.masterMixer;

		switch(slider.name)
		{
			//사운드 슬라이더
			case "MasterSound":

				if (value == -40f)
					masterMixer.SetFloat("Master", -80f);

				else
					masterMixer.SetFloat("Master", value);
				break;

			case "BGM":

                if (value == -40f)
                    masterMixer.SetFloat("BGM", -80f);

                else
                    masterMixer.SetFloat("BGM", value);
                break;

			case "SFX":

                if (value == -40f)
                    masterMixer.SetFloat("SFX", -80f);

                else
                    masterMixer.SetFloat("SFX", value);
                break;
				//*******************************************************************
				//디스플레이 슬라이더
			case "CameraVertical":
				Debug.Log("마우스 감도 조절 필요");
				break;

			case "CameraHorizontal":
				Debug.Log("마우스 감도 조절 필요");
				break;
		}
	}
}
