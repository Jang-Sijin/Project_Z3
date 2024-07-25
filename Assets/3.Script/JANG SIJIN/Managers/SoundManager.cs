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

    private float masterVolume = 1.0f;   // 마스터 볼륨
    private float bgmVolume = 0.5f;      // BGM 볼륨
    private float effectVolume = 0.5f;   // 이펙트 볼륨

    public float MasterVolume => masterVolume * 100;        
    public float BgmVolume => bgmVolume * 100;
    public float EffectVolume => effectVolume * 100;

    private float audioFadeDuration = 5f;

    protected override void Awake()
    {
        base.Awake();

        // 레거시
        bgmSource = gameObject.AddComponent<AudioSource>();
        effectSource = gameObject.AddComponent<AudioSource>();

        bgmSource.loop = true;

        // 사운드 초기 볼륨 크기 설정
        bgmSource.volume = bgmVolume * masterVolume;
        effectSource.volume = effectVolume * masterVolume;

        // 오디오 클립 로드
        LoadAudio();
    }

    void LoadAudio()
    {
        // BGM 및 SFX를 Dictionary에 추가
        bgmDictionary = new Dictionary<string, AudioClip>();
        effectDictionary = new Dictionary<string, AudioClip>();

        // Resources 폴더 내 bgm, sfx 경로 설정       
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
    /// 파일 이름으로 배경 사운드를 실행합니다.
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
            Debug.LogWarning("BGM 클립을 찾을 수 없습니다: " + bgmName);
        }
    }

    /// <summary>
    /// 배경 사운드를 종료합니다.
    /// </summary>
    public void StopBgm()
    {
        bgmSource.Stop();
    }

    /// <summary>
    /// 파일 이름으로 효과음 사운드를 실행합니다.
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
            Debug.LogWarning("SFX 클립을 찾을 수 없습니다: " + sfxName);
        }
    }

    /// <summary>
    /// 효과음 사운드를 종료합니다.
    /// </summary>
    public void StopEffect()
    {
        effectSource.Stop();
    }

    /// <summary>
    /// 마스터 볼륨을 설정합니다.
    /// </summary>
    /// <param name="volume"></param>
    public void SetMasterVolume(float volume)
    {
        masterVolume = volume / 100f;  // 슬라이더 값(0~1)을 실제 볼륨 값(0~1)으로 변환
        bgmSource.volume = bgmVolume * masterVolume;
        effectSource.volume = effectVolume * masterVolume;
    }

    /// <summary>
    /// BGM 볼륨을 설정합니다.
    /// </summary>
    /// <param name="volume"></param>
    public void SetBgmVolume(float volume)
    {
        bgmVolume = volume / 100f;  // 슬라이더 값(0~1)을 실제 볼륨 값(0~1)으로 변환
        bgmSource.volume = bgmVolume * masterVolume;
    }

    /// <summary>
    /// 이펙트 볼륨을 설정합니다.
    /// </summary>
    /// <param name="volume"></param>
    public void SetEffectVolume(float volume)
    {
        effectVolume = volume / 100f;  // 슬라이더 값(0~1)을 실제 볼륨 값(0~1)으로 변환
        effectSource.volume = effectVolume * masterVolume;
    }
}