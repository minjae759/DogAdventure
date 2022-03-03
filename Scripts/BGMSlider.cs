using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGMSlider : MonoBehaviour
{

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
    }

    private void OnEnable()
    {
        LoadData();
    }

    private void OnDisable()
    {
        SaveData();
    }

    public void ChangedValue()
    {
        float value = slider.value;
        BGMManager.instance.AdjacentVolume(value);
    }

    void LoadData()
    {
        float value = OptionDataManager.instance.GetBGMVolume();
        slider.value = value;
    }

    void SaveData()
    {
        OptionDataManager.instance.SetBGMVolume(slider.value);
    }
    private void OnMouseUp()
    {
        SFXManager.instance.PlaySliderclip();
    }
}
