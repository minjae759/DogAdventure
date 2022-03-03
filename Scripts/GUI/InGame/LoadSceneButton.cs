using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneButton : MonoBehaviour
{
    [SerializeField]
    private string sceneName;
    private string curSceneName;

    void Start()
    {
        curSceneName = GameManager.instance.GetSceneName();
    }

    public void CallLoadScene()
    {
        GameManager.instance.LoadScene(sceneName);
    }

    public void CallThisScene()
    {
        GameManager.instance.LoadScene(curSceneName);
    }

}
