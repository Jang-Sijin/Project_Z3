using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class SoundManager : SingletonBase<SoundManager>
{
    private AudioSource bgmSource;
    private AudioSource sfxSource;

    private Dictionary<string, AudioClip> bgmDictionary;
    private Dictionary<string, AudioClip> sfxDictionary;

    protected override void Awake()
    {
        base.Awake();

        // AudioSource ������Ʈ �߰� �� ����
        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        bgmSource.loop = true;

        // ����� Ŭ�� �ε�
        LoadAudio();
    }

    void LoadAudio()
    {
        // BGM �� SFX�� Dictionary�� �߰�
        bgmDictionary = new Dictionary<string, AudioClip>();
        sfxDictionary = new Dictionary<string, AudioClip>();

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
            sfxDictionary.Add(sfx.name, sfx);
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
        if (sfxDictionary.ContainsKey(sfxName))
        {
            sfxSource.PlayOneShot(sfxDictionary[sfxName]);
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
        sfxSource.Stop();
    }
}