using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeOverText : MonoBehaviour
{

    public float time;
    private Text timelimitText;
    private bool isTimeover;

    void Start()
    {
        isTimeover = false;
        timelimitText = GetComponent<Text>();
    }

    void Update()
    {
        if(time > 0f)
        {
            time -= Time.deltaTime;
        }
        else if(!isTimeover)
        {
            isTimeover = true;
            GameManager.instance.GameEnd();
        }
        SetText();
    }

    void SetText()
    {
        timelimitText.text = string.Format("{0:F1}", time);
    }
}
