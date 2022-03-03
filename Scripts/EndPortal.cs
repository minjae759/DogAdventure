using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndPortal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            SFXManager.instance.PlayStageClearclip();
            GameManager.instance.PauseOn();
            StartCoroutine("WaitSeconds");
        }
    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSecondsRealtime(2f);
        GameManager.instance.LoadScene("StageSelection");
        yield break;
    }
}
