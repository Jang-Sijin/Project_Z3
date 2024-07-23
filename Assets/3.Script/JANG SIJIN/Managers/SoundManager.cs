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

        // AudioSource 컴포넌트 추가 및 설정
        bgmSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        bgmSource.loop = true;

        // 오디오 클립 로드
        LoadAudio();
    }

    void LoadAudio()
    {
        // BGM 및 SFX를 Dictionary에 추가
        bgmDictionary = new Dictionary<string, AudioClip>();
        sfxDictionary = new Dictionary<string, AudioClip>();

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
            sfxDictionary.Add(sfx.name, sfx);
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
        if (sfxDictionary.ContainsKey(sfxName))
        {
            sfxSource.PlayOneShot(sfxDictionary[sfxName]);
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
        sfxSource.Stop();
    }
}