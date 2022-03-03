using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightSlider : MonoBehaviour
{

    private Slider slider;

    private void Awake()
    {
        slider = GetComponent<Slider>();
        LoadData();
    }

    private void OnEnable()
    {
    }

    private void OnDisable()
    {
        SaveData();
    }

    public void ChangedValue()
    {
        float value = slider.value;
        LightScript.instance.AdjacentLight(value);
    }

    void LoadData()
    {
        float value = OptionDataManager.instance.GetBrightness();
        slider.value = value;
    }

    void SaveData()
    {
        OptionDataManager.instance.SetBrightness(slider.value);
    }

    private void OnMouseUp()
    {
        SFXManager.instance.PlaySliderclip();
    }
}
