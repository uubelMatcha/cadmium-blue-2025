using System;
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

    private void Start()
    {
        
    }

    public void ChangeBGM(string bgmTitle)
    {
        SoundData desiredBGM = GetBGM(bgmTitle);
        if(currentBGM != desiredBGM)
        {
            bgmSource.clip = desiredBGM.audioClip;
            bgmSource.Play();
            currentBGM = desiredBGM;
        }
    }

    private SoundData GetBGM(string title)
    {
        foreach(SoundData bgm in bgmList)
        {
            if (bgm.musicTitle == title) return bgm;
        }

        {
            throw new Exception("music title not found. Was the right title defined?");
        }
    }

}
