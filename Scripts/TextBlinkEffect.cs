using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinkEffect : MonoBehaviour
{

    Text text;

    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine("Blink");
    }

    IEnumerator Blink()
    {
        float gap = 0.1f;
        while(true)
        {
            for (int i = 0; i < 10; i++)
            {
                text.color = new Color(1f, 1f, 1f, text.color.a - gap);
                yield return new WaitForSecondsRealtime(0.1f);
            }
            for (int i = 0; i < 10; i++)
            {
                text.color = new Color(1f, 1f, 1f, text.color.a + gap);
                yield return new WaitForSecondsRealtime(0.1f);
            }
        }
        yield break;
    }
}