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
	 * �� ��ư���� OnClick() �� �ҷ��� �޼ҵ���� �ֽ��ϴ�.
	 */

	private Image[] images; // ������ �� fade�� �̹������Դϴ�.

	private void Awake()
	{
        int imagesLength = GetComponentsInChildren<Image>().Length;

        images = new Image[imagesLength];
        images = GetComponentsInChildren<Image>();
    }

	[SerializeField]private Animator startInto_btn_Ani; // ���õ� �޴�â�� �ִϸ��̼��� �ٲٱ� ���� ��������ϴ�.
	private Animator curInto_btn_Ani;

	[SerializeField]private Image UpperBlackBlank; 
	[SerializeField]private Image UnderBlackBlank;

	[SerializeField] private GameObject displayMenu;
	[SerializeField] private GameObject soundMenu;

    private void OnEnable() // esc���� �� ���� �����ս��� ����˴ϴ�.
    {
		curInto_btn_Ani = startInto_btn_Ani;
		displayMenu.SetActive(true);
		soundMenu.SetActive(false); // ���÷��� ȭ���� �׻� ���� ���ɴϴ�.

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
			//���� �����̴�
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
				//���÷��� �����̴�
			case "CameraVertical":
				Debug.Log("���콺 ���� ���� �ʿ�");
				break;

			case "CameraHorizontal":
				Debug.Log("���콺 ���� ���� �ʿ�");
				break;
		}
	}
}
