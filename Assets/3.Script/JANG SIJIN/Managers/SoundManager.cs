using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;

public class SoundManager : SingletonBase<SoundManager>
{    
    private AudioSource bgmSource;
    private AudioSource effectSource;

    private Dictionary<string, AudioClip> bgmDictionary;
    private Dictionary<string, AudioClip> effectDictionary;

    private float masterVolume = 1.0f;   // ������ ����
    private float bgmVolume = 0.5f;      // BGM ����
    private float effectVolume = 0.5f;   // ����Ʈ ����

    public float MasterVolume => masterVolume * 100;        
    public float BgmVolume => bgmVolume * 100;
    public float EffectVolume => effectVolume * 100;

    private float audioFadeDuration = 5f;

    protected override void Awake()
    {
        base.Awake();

        // ���Ž�
        bgmSource = gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();

        bgmSource.loop = true;

        // ���� �ʱ� ���� ũ�� ����
        bgmSource.volume = bgmVolume * masterVolume;
        effectSource.volume = effectVolume * masterVolume;

        // ����� Ŭ�� �ε�
        LoadAudio();
    }

    void LoadAudio()
    {
        // BGM �� SFX�� Dictionary�� �߰�
        bgmDictionary = new Dictionary<string, AudioClip>();
        effectDictionary = new Dictionary<string, AudioClip>();

        // Resources ���� �� bgm, sfx ��� ����       
        AudioClip[] bgmClips = Resources.LoadAll<AudioClip>("Sounds/BGM");
        AudioClip[] sfxClips = Resources.LoadAll<AudioClip>("Sounds/Effect");

        // Load Sound Files
        foreach (var bgm in bgmClips)
        {
            bgmDictionary.Add(bgm.name, bgm);
        }

        foreach (var sfx in sfxClips)
        {
            effectDictionary.Add(sfx.name, sfx);
        }
    }

    /// <summary>
    /// ���� �̸����� ��� ���带 �����մϴ�.
    /// </summary>
    /// <param name="bgmName"></param>
    public void PlayBgm(string bgmName)
    {
        if (bgmDictionary.ContainsKey(bgmName))
        {
            bgmSource.clip = bgmDictionary[bgmName];
            bgmSource.Play();
        }
        else
        {
            Debug.LogWarning("BGM Ŭ���� ã�� �� �����ϴ�: " + bgmName);
        }
    }

    /// <summary>
    /// ��� ���带 �����մϴ�.
    /// </summary>
    public void StopBgm()
    {
        bgmSource.Stop();
    }

    /// <summary>
    /// ���� �̸����� ȿ���� ���带 �����մϴ�.
    /// </summary>
    /// <param name="sfxName"></param>
    public void PlayEffect(string sfxName)
    {
        if (effectDictionary.ContainsKey(sfxName))
        {
            effectSource.PlayOneShot(effectDictionary[sfxName]);
        }
        else
        {
            Debug.LogWarning("SFX Ŭ���� ã�� �� �����ϴ�: " + sfxName);
        }
    }

    /// <summary>
    /// ȿ���� ���带 �����մϴ�.
    /// </summary>
    public void StopEffect()
    {
        effectSource.Stop();
    }

    /// <summary>
    /// ������ ������ �����մϴ�.
    /// </summary>
    /// <param name="volume"></param>
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume / 100f;  // �����̴� ��(0~1)�� ���� ���� ��(0~1)���� ��ȯ
        bgmSource.volume = bgmVolume * masterVolume;
        effectSource.volume = effectVolume * masterVolume;
    }

    /// <summary>
    /// BGM ������ �����մϴ�.
    /// </summary>
    /// <param name="volume"></param>
    public void SetBgmVolume(float volume)
    {
        bgmVolume = volume / 100f;  // �����̴� ��(0~1)�� ���� ���� ��(0~1)���� ��ȯ
        bgmSource.volume = bgmVolume * masterVolume;
    }

    /// <summary>
    /// ����Ʈ ������ �����մϴ�.
    /// </summary>
    /// <param name="volume"></param>
    public void SetEffectVolume(float volume)
    {
        effectVolume = volume / 100f;  // �����̴� ��(0~1)�� ���� ���� ��(0~1)���� ��ȯ
        effectSource.volume = effectVolume * masterVolume;
    }
}