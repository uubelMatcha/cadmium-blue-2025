using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgmSource;

    [SerializeField] private AudioSource sfxSource;

    [SerializeField] private List<SoundData> bgmList;
    [SerializeField] private List<SoundData> sfxList;
    
    //debug only
    [SerializeField] private SoundData currentBGM;

    public static AudioManager audioManagerInstance;
    
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
        DontDestroyOnLoad(gameObject);
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
    
    //coroutine to trigger a sound effect
    public IEnumerator PlaySoundEffect(string sfxTitle)
    {
        SoundData desiredSfx = GetSound(sfxTitle, sfxList);
        sfxSource.PlayOneShot(desiredSfx.audioClip);
        yield return new WaitForSeconds(1f);
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
