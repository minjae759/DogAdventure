using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGMManager : MonoBehaviour
{
    static public BGMManager instance;

    public List<AudioClip> audioClips = new List<AudioClip>();

    private AudioSource audioSource;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        audioSource = GetComponent<AudioSource>();
        audioSource.loop = true;
    }

    private void Start()
    {
        VolumeInit();
    }

    public void Playclip(string clipname)
    {
        foreach(AudioClip audioClip in audioClips)
        {
            if(audioClip.name == clipname)
            {
                audioSource.clip = audioClip;
                audioSource.Play();
            }
        }
    }
    
    public void AdjacentVolume(float volume)
    {
        audioSource.volume = volume;
    }

    private void VolumeInit()
    {
        audioSource.volume = OptionDataManager.instance.GetBGMVolume();
    }
}
