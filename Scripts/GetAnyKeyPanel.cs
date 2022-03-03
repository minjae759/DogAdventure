using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetAnyKeyPanel : MonoBehaviour
{
    public GameObject menuPanel;

    void Update()
    {
        GetAnyKey();
    }

    void GetAnyKey()
    {
        if(Input.anyKey)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnDisable()
    {
        menuPanel.SetActive(true);
    }
}
