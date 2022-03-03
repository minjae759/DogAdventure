
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionData
{
    [SerializeField]
    float bgmvolume;
    [SerializeField]
    float sfxvolume;
    [SerializeField]
    float brightness;
    [SerializeField]
    Vector2 recentpos;

    public float BGMVOLUME { get { return bgmvolume; } set { bgmvolume = value; } }
    public float SFXVOLUME { get { return sfxvolume; } set { sfxvolume = value; } }
    public float BRIGHTNESS { get { return brightness; } set { brightness = value; } }
    public Vector2 RECENTPOS { get { return recentpos; }  set { recentpos = value; } } 
}
