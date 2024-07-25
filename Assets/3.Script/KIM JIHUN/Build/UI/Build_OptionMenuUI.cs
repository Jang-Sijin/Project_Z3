using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Build_OptionMenuUI : MonoBehaviour
{
	 // * �� ��ư���� OnClick() �� �ҷ��� �޼ҵ���� �ֽ��ϴ�.	 

    private Image[] images; // ������ �� fade�� �̹������Դϴ�.

    private void Awake()
    {
        int imagesLength = GetComponentsInChildren<Image>().Length;

        images = new Image[imagesLength];
        images = GetComponentsInChildren<Image>();

        this.gameObject.SetActive(false);
    }

    [SerializeField] private Animator startInto_btn_Ani; // ���õ� �޴�â�� �ִϸ��̼��� �ٲٱ� ���� ��������ϴ�.
    private Animator curInto_btn_Ani;

    [SerializeField] private Image UpperBlackBlank;
    [SerializeField] private Image UnderBlackBlank;

    [SerializeField] private GameObject displayMenu; // �ػ� �޴�
    [SerializeField] private GameObject soundMenu;
    [SerializeField] private GameObject quitMenu;
    [SerializeField] Animator CamDirectionAni;

    /// <summary>
    /// ���� ���� UI
    /// </summary>
    [SerializeField] private Slider _masterVolumeSlider;
    [SerializeField] private Slider _bgmVolumeSlider;
    [SerializeField] private Slider _effectVolumeSlider;
    [SerializeField] private TextMeshProUGUI _masterVolumeTextMesh;
    [SerializeField] private TextMeshProUGUI _bgmVolumeTextMesh;
    [SerializeField] private TextMeshProUGUI _effectVolumeTextMesh;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        _masterVolumeSlider.value = SoundManager.Instance.MasterVolume;
        _bgmVolumeSlider.value = SoundManager.Instance.BgmVolume;
        _effectVolumeSlider.value = SoundManager.Instance.EffectVolume;

        // �����̴� �ʱ� ���� ���� �ؽ�Ʈ �ʱ�ȭ
        UpdateTextValue();

        // �����̴� ���� ����� ������ ȣ��� �Լ� ���
        _masterVolumeSlider.onValueChanged.AddListener(delegate { UpdateTextValue(); });
        _bgmVolumeSlider.onValueChanged.AddListener(delegate { UpdateTextValue(); });
        _effectVolumeSlider.onValueChanged.AddListener(delegate { UpdateTextValue(); });
    }

    private void UpdateTextValue()
    {
        // ���� UI Text ���� �� ����
        _masterVolumeTextMesh.text = ((int)_masterVolumeSlider.value).ToString();
        _bgmVolumeTextMesh.text = ((int)_bgmVolumeSlider.value).ToString();
        _effectVolumeTextMesh.text = ((int)_effectVolumeSlider.value).ToString();

        // ���� �Ŵ��� ���� �� ������Ʈ
        SoundManager.Instance.SetMasterVolume((int)_masterVolumeSlider.value);
        SoundManager.Instance.SetBgmVolume((int)_bgmVolumeSlider.value);
        SoundManager.Instance.SetEffectVolume((int)_effectVolumeSlider.value);
    }

    //Pause�޴� Ȱ��ȭ�� �ʱ� ����
    //*********************************************************************************************************
    public IEnumerator CallPauseMenu_co()
    {
        UIManager.Instance.isCloseOrOpen = true;
        curInto_btn_Ani = startInto_btn_Ani;
        displayMenu.SetActive(true);
        soundMenu.SetActive(false); // ���÷��� ȭ���� �׻� ���� ���ɴϴ�.

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
        UIManager.Instance.isCloseOrOpen = false;
        UIManager.Instance.isPause = true;
    }
    //************************************************************************************************************


    //ȯ�漳�� �޴� ����� ȿ�� �޼ҵ�
    //*********************************************************************************************************
    private IEnumerator ClosePauseMenu_co()
    {
        yield return new WaitForSeconds(0.2f);

        FadeImages();

        UpperBlackBlank.rectTransform.DOAnchorPosY(70f, 0.2f);
        UnderBlackBlank.rectTransform.DOAnchorPosY(-70f, 0.2f);

        yield return new WaitForSeconds(0.2f);

        UIManager.Instance.isCloseOrOpen = false;
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



    //���� ��ư �Լ�
    //*********************************************************************************************************
    public void OnClickCloseMainUI() // ���� UI �ݱ�
    {
        UIManager.Instance.isCloseOrOpen = true;
        gameObject.SetActive(true);
        StartCoroutine(ClosePauseMenu_co());
        UIManager.Instance.isPause = false;
    }

    public void OnClickIntoButton(Animator changeCur)// ���� �޴� ���� ��ư Ŭ���� �߻��ϴ� �ִϸ��̼��Դϴ�.
    {
        curInto_btn_Ani.SetBool("Selected", false);
        curInto_btn_Ani.SetTrigger("Normal");

        curInto_btn_Ani = changeCur;
        curInto_btn_Ani.SetBool("Selected", true);
    }

    public void OnClickShowSoundMenu() // ���� �޴� ����
    {
        displayMenu.SetActive(false);
        soundMenu.SetActive(true);
    }

    public void OnClickShowDisplayMenu() // ���÷��� �޴� ����
    {
        soundMenu.SetActive(false);
        displayMenu.SetActive(true);
        ChangeCamDirection(true);
    }

    public void OnClickMainMenuOpen() // ���� �޴� �ҷ�����
    {
        quitMenu.SetActive(true);
    }

    public void OnClickMainMenuClose() // ���� �޴� ������
    {
        quitMenu.SetActive(false);
    }

    public void OnClickExitGame() // ���� ���� ��ư
    {
        Application.Quit();
    }

    public void ChangeCamDirection(bool isOn)
    {
        if (!isOn)
        {
            //CamDirectionAni.SetBool("On", true);
        }
        else
        {
            //CamDirectionAni.SetBool("On", false);
        }
    }
    //*********************************************************************************************************


    //�����̴� ����
    //*********************************************************************************************************

    [SerializeField] private AudioMixer masterMixer;
    public void OnChangeSliders(Slider slider)
    {
        float value = slider.value;

        TextMeshProUGUI numValue = slider.GetComponentInChildren<TextMeshProUGUI>();


        switch (slider.name)
        {
            //���� �����̴�
            case "MasterSound":
                numValue.text = $"{(int)((value + 40) * 2.5f)}";
                slider.value = (int)slider.value;
                if (value == -40f)
                    masterMixer.SetFloat("Master", -80f);

                else
                    masterMixer.SetFloat("Master", value);
                break;

            case "BGM":
                numValue.text = $"{(int)((value + 40) * 2.5f)}";
                slider.value = (int)slider.value;

                if (value == -40f)
                    masterMixer.SetFloat("BGM", -80f);

                else
                    masterMixer.SetFloat("BGM", value);
                break;

            case "SFX":
                numValue.text = $"{(int)((value + 40) * 2.5f)}";
                slider.value = (int)slider.value;

                if (value == -40f)
                    masterMixer.SetFloat("SFX", -80f);

                else
                    masterMixer.SetFloat("SFX", value);
                break;

            //*******************************************************************
            //���÷��� �����̴�

            case "CameraVertical":
                slider.value = (int)slider.value;
                numValue.text = $"{(int)value}";
                Debug.Log("���콺 ���� ���� �ʿ�");
                break;

            case "CameraHorizontal":
                slider.value = (int)slider.value;
                numValue.text = $"{(int)value}";
                Debug.Log("���콺 ���� ���� �ʿ�");
                break;
        }
    }

    //*********************************************************************************************************
    //��Ӵٿ� UI �޼ҵ��Դϴ�.
    public void ResolutionDropDown(int index)
    {
        switch (index)
        {
            case 0:
                Screen.SetResolution(1920, 1080, true);
                Debug.Log("�ػ󵵸� 1920x1080(��üȭ��)���� �ٲߴϴ�.");
                break;

            case 1:
                Screen.SetResolution(1280, 720, true);
                Debug.Log("�ػ󵵸� 1280x720(��üȭ��)���� �ٲߴϴ�.");
                break;

            case 2:
                Screen.SetResolution(1920, 1080, false);
                Debug.Log("�ػ󵵸� 1920x1080(â���)���� �ٲߴϴ�.");
                break;

            case 3:
                Screen.SetResolution(1280, 720, false);
                Debug.Log("�ػ󵵸� 1280x720(â���)���� �ٲߴϴ�.");
                break;
        }
    }
    //*********************************************************************************************************



}
