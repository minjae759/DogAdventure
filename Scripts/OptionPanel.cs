using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionPanel : MonoBehaviour
{
    private void OnDisable()
    {
        OptionDataManager.instance.SaveOptionData();
        gameObject.SetActive(false);
    }

    private void Update()
    {
        GetKey();
    }

    void GetKey()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            OnDisable();
        }
    }
}