using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEditor.ShaderGraph.Drawing.Inspector.PropertyDrawers;
using UnityEngine.Audio;
using TMPro;

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

        this.gameObject.SetActive(false);
    }

    [SerializeField]private Animator startInto_btn_Ani; // 선택된 메뉴창의 애니메이션을 바꾸기 위해 만들었습니다.
	private Animator curInto_btn_Ani;

	[SerializeField]private Image UpperBlackBlank; 
	[SerializeField]private Image UnderBlackBlank;

	[SerializeField] private GameObject displayMenu;
	[SerializeField] private GameObject soundMenu;

	//Pause메뉴 활성화시 초기 세팅
    //*********************************************************************************************************
    public IEnumerator CallPauseMenu_co()
	{
        UIManager.instance.isCloseOrOpen = true;
        curInto_btn_Ani = startInto_btn_Ani;
        displayMenu.SetActive(true);
        soundMenu.SetActive(false); // 디스플레이 화면이 항상 먼저 나옵니다.

        curInto_btn_Ani.SetBool("Selected", true);

		if (images == null)
			Debug.Log("null");

        for (int i = 0; i < images.Length; i++)
        {
            if (images[i].CompareTag("InvisibleButton"))
                continue;

            images[i].DOFade(0f, 0f);
            images[i].DOFade(1, 0.2f).SetEase(Ease.OutQuad);
        }

        UpperBlackBlank.rectTransform.DOAnchorPosY(-70f, 0.2f).SetEase(Ease.OutQuad);
        Tween tween = UnderBlackBlank.rectTransform.DOAnchorPosY(70f, 0.2f).SetEase(Ease.OutQuad);

		yield return tween.WaitForCompletion();
		UIManager.instance.isCloseOrOpen = false;
        UIManager.instance.isPause = true;
    }
    //************************************************************************************************************


	//환경설정 메뉴 종료시 효과 메소드
    //*********************************************************************************************************
    private IEnumerator ClosePauseMenu()
	{
		yield return new WaitForSeconds(0.2f);

		FadeImages();

		UpperBlackBlank.rectTransform.DOAnchorPosY(70f, 0.2f);
		UnderBlackBlank.rectTransform.DOAnchorPosY(-70f, 0.2f);

        yield return new WaitForSeconds(0.2f);

		UIManager.instance.isCloseOrOpen = false;
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
    //*********************************************************************************************************



    //여러 버튼 함수
    //*********************************************************************************************************
    public void OnClickClose()
	{
		UIManager.instance.isCloseOrOpen = true;
		UIManager.instance.pauseMenuUI.gameObject.SetActive(true);
		StartCoroutine(ClosePauseMenu());
        UIManager.instance.isPause = false;
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
    //*********************************************************************************************************


    //오디오 설정
    //*********************************************************************************************************

    [SerializeField]private AudioMixer masterMixer;
	public void OnChangeSliders(Slider slider)
	{
		float value = slider.value;

		TextMeshProUGUI numValue = slider.GetComponentInChildren<TextMeshProUGUI>();

		
		switch(slider.name)
		{
			//사운드 슬라이더
			case "MasterSound":
                numValue.text = $"{(int)((value + 40) * 2.5f)}";

                if (value == -40f)
					masterMixer.SetFloat("Master", -80f);

				else
					masterMixer.SetFloat("Master", value);
				break;

			case "BGM":
                numValue.text = $"{(int)((value + 40) * 2.5f)}";

                if (value == -40f)
                    masterMixer.SetFloat("BGM", -80f);

                else
                    masterMixer.SetFloat("BGM", value);
                break;

			case "SFX":
                numValue.text = $"{(int)((value + 40) * 2.5f)}";

                if (value == -40f)
                    masterMixer.SetFloat("SFX", -80f);

                else
                    masterMixer.SetFloat("SFX", value);
                break;

				//*******************************************************************
				//디스플레이 슬라이더

			case "CameraVertical":
                numValue.text = $"{(int)value}";
                Debug.Log("마우스 감도 조절 필요");
				break;

			case "CameraHorizontal":
                numValue.text = $"{(int)value}";
                Debug.Log("마우스 감도 조절 필요");
				break;
		}

		//*********************************************************************************************************
	}
}
