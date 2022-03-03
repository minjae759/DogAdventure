using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{

    public static LightScript instance;

    private Light light;

    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }

        light = GetComponent<Light>();
    }
    
    private void Start()
    {
        BrightnessInit();
    }

    public void AdjacentLight(float value)
    {
        light.intensity = value;
    }

    private void BrightnessInit()
    {
        light.intensity = OptionDataManager.instance.GetBrightness();
    }
}
