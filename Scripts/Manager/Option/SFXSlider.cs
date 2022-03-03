using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SFXSlider : MonoBehaviour
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
        SFXManager.instance.AdjacentVolume(value);
    }

    void LoadData()
    {
        float value = OptionDataManager.instance.GetSFXVolume();
        slider.value = value;
    }

    void SaveData()
    {
        OptionDataManager.instance.SetSFXVolume(slider.value);
    }

    private void OnMouseUp()
    {
        Debug.Log("OnMouseUp");
        SFXManager.instance.PlaySliderclip();
    }

    private void OnMouseUpAsButton()
    {
        Debug.Log("OnMouseUpAsButton");

    }
}
