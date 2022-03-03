using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class OptionDataManager : MonoBehaviour
{
    static public OptionDataManager instance;

    OptionData optionData = new OptionData();


    string DATA_PATH;
    string DATA_OPTIONDATA_FILENAME = "/OptionData.txt";

    private void Awake()
    {
        if (instance == null)
            instance = this;

        DATA_PATH = Application.dataPath + "/Save/";
        OptionInit();
    }

    public void SaveOptionData()
    {
        string json = JsonUtility.ToJson(optionData);
        File.WriteAllText(DATA_PATH + DATA_OPTIONDATA_FILENAME, json);
    }

    public void LoadOptionData()
    {
        string json = File.ReadAllText(DATA_PATH + DATA_OPTIONDATA_FILENAME);
        optionData = JsonUtility.FromJson<OptionData>(json);
    }

    private void OptionInit()
    {
        if (File.Exists(DATA_PATH + DATA_OPTIONDATA_FILENAME))
        {
            LoadOptionData();
        }
        else
        {
            optionData.BGMVOLUME = 1f;
            optionData.SFXVOLUME = 1f;
            optionData.BRIGHTNESS = 1f;
            ResetPos();
        }
    }

    public void ResetPos()
    {
        optionData.RECENTPOS = new Vector2(140f, -310f);
        SaveOptionData();
    }

    public void SetSFXVolume(float value)
    {
        optionData.SFXVOLUME = value;
    }

    public void SetBGMVolume(float value)
    {
        optionData.BGMVOLUME = value;
    }

    public void SetBrightness(float value)
    {
        optionData.BRIGHTNESS = value;
    }

    public void SetRecentPos(Vector2 value)
    {
        optionData.RECENTPOS = value;
    }

    public float GetSFXVolume()
    {
        return optionData.SFXVOLUME;
    }

    public float GetBGMVolume()
    {
        return optionData.BGMVOLUME;
    }

    public float GetBrightness()
    {
        return optionData.BRIGHTNESS;
    }

    public Vector2 GetRecentPos()
    {
        return optionData.RECENTPOS;
    }
}
