using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverText : MonoBehaviour
{
    void Update()
    {
        GetAnyKey();
    }

    void GetAnyKey()
    {
        if(Input.anyKey)
        {
            StartCoroutine(WaitSeconds(2f));
        }
    }

    IEnumerator WaitSeconds(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        GameManager.instance.LoadScene("MainTitle");
        yield break;
    }
}
