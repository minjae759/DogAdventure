using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    static public SFXManager instance;

    private AudioSource audioSource;

    public AudioClip swing;
    public AudioClip jump;
    public AudioClip defence;
    public AudioClip getItem;

    public AudioClip stageClear;
    public AudioClip pause;
    public AudioClip resume;
    public AudioClip select;
    public AudioClip slider;
    public AudioClip playerDotMove;
    public AudioClip levelUp;
    public AudioClip playerDead;


    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        VolumeInit();
    }

    public void PlaySwingclip()
    {
        audioSource.PlayOneShot(swing);
    }

    public void PlayJumpclip()
    {
        audioSource.PlayOneShot(jump);
    }
    
    public void PlayDefenceclip()
    {
        audioSource.PlayOneShot(defence);
    }

    public void PlayGetItemclip()
    {
        audioSource.PlayOneShot(getItem);
    }

    public void PlayStageClearclip()
    {
        audioSource.PlayOneShot(stageClear);
    }

    public void PlayPauseclip()
    {
        audioSource.PlayOneShot(pause);
    }
    
    public void PlayResumeclip()
    {
        audioSource.PlayOneShot(resume);
    }

    public void PlaySliderclip()
    {
        audioSource.PlayOneShot(slider);
    }

    public void PlayPlayerDotMoveclip()
    {
        audioSource.PlayOneShot(playerDotMove);
    }

    public void PlayPlayerLevelUpclip()
    {
        audioSource.PlayOneShot(levelUp);
    }

    public void PlayPlayerDeadclip()
    {
        audioSource.PlayOneShot(playerDead);
    }

    public void AdjacentVolume(float volume)
    {
        audioSource.volume = volume;
    }

    

    private void VolumeInit()
    {
        audioSource.volume = OptionDataManager.instance.GetSFXVolume();
    }
}
