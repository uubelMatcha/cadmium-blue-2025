using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioSource anxietySource;
    
    [SerializeField] private AudioSource sfxSource;
    
    [SerializeField] private AudioSource footstepsSource;

    [SerializeField] private List<SoundData> bgmList;
    [SerializeField] private List<SoundData> sfxList;

    [SerializeField] private AudioClip footstepClip;
    
    //debug only
    [SerializeField] private SoundData currentBGM;

    public static AudioManager audioManagerInstance;

    private float originalBGVolume = 0.5f;
    
    private void Awake()
    {
        if (audioManagerInstance == null)
        {
            audioManagerInstance = this;   
        }
        else
        {
            Destroy(gameObject);
        }
        //DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        originalBGVolume = bgmSource.volume;
    }

    public void ChangeBGM(string bgmTitle)
    {
        SoundData desiredBGM = GetSound(bgmTitle, bgmList);
        if(currentBGM != desiredBGM)
        {
            bgmSource.clip = desiredBGM.audioClip;
            bgmSource.Play();
            currentBGM = desiredBGM;
        }
    }
    
    public void PlayAnxietyMusic(string bgmTitle, bool enable)
    {
        SoundData desiredBGM = GetSound(bgmTitle, bgmList);
        if(currentBGM != desiredBGM && enable)
        {
            bgmSource.volume = 0.01f;
            anxietySource.clip = desiredBGM.audioClip;
            anxietySource.Play();
        }
        else if(!enable)
        {
            bgmSource.volume = originalBGVolume;
            anxietySource.Stop();
        }
    }
    
    //coroutine to trigger a sound effect
    public void PlaySoundEffect(string sfxTitle)
    {
        SoundData desiredSfx = GetSound(sfxTitle, sfxList);
        sfxSource.PlayOneShot(desiredSfx.audioClip);
    }

    public void PlayFootsteps(bool enableFootsteps)
    {
        if (enableFootsteps && !footstepsSource.isPlaying)
        {
            footstepsSource.clip = footstepClip;
            footstepsSource.PlayOneShot(footstepClip);
        }
        else if(!enableFootsteps && footstepsSource.isPlaying)
        {
            footstepsSource.Stop();
        }
    }

    private SoundData GetSound(string title, List<SoundData> dataBank)
    {
        foreach(SoundData bgm in dataBank)
        {
            if (bgm.musicTitle == title) return bgm;
        }

        {
            throw new Exception("music title not found. Was the right title defined?");
        }
    }
}
