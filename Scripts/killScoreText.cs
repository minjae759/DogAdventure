using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class killScoreText : MonoBehaviour
{

    static public killScoreText instance;

    Text killScore;

    void Start()
    {
        if (instance == null)
            instance = this;

        killScore = gameObject.GetComponent<Text>();
    }

    private void Update()
    {
        SetText();
    }
    public void SetText()
    {
        int deadCount = GameManager.instance.GetDeadMonster();
        if (deadCount >= 10) GameManager.instance.ActivePortal();
        killScore.text = deadCount + " / 10";
    }
}
