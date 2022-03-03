using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleMenuPanel : MonoBehaviour
{
    public GameObject[] menu;
    public GameObject selectCursor;
    public GameObject optionPanel;

    int curidx;

    private void OnEnable()
    {
        curidx = 0;
    }

    void Update()
    {
        GetKey();
    }

    void GetKey()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            curidx++;
            if (curidx >= menu.Length) curidx = 0;
            SFXManager.instance.PlayPlayerDotMoveclip();
            selectCursor.transform.SetParent(menu[curidx].transform);
            selectCursor.transform.localPosition = Vector3.zero;
        }
        else if(Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            curidx--;
            if (curidx < 0) curidx = menu.Length - 1;
            SFXManager.instance.PlayPlayerDotMoveclip();
            selectCursor.transform.SetParent(menu[curidx].transform);
            selectCursor.transform.localPosition = Vector3.zero;
        }
        else if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            string selectedName = selectCursor.transform.parent.name;

            if(selectedName == "GameStartText")
            {
                GameManager.instance.LoadScene("StageSelection");
            }
            else if(selectedName == "OptionText")
            {
                optionPanel.SetActive(true);
            }
            else if (selectedName == "ExitText")
            {
                Application.Quit();
            }
            
        }
    }
}
