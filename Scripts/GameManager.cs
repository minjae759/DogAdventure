using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    static public GameManager instance;

    private GameObject[] portals;
    
    int deadMonster;

    private bool isPause;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        PauseOff();
        Application.targetFrameRate = 60;
        deadMonster = 0;
    }

    void Start()
    {
        BGMManager.instance.Playclip(SceneManager.GetActiveScene().name);
    }

    private void OnEnable()
    {
        isPause = false;
    }

    public void ActivePortal()
    {
        portals = GameObject.FindGameObjectsWithTag("Portal");

        if (portals.Length > 0)
        {
            foreach (GameObject portal in portals)
            {
                if (portal.name == "EndPortalParent")
                {
                    portal.transform.Find("EndPortal").gameObject.SetActive(true);
                }
            }
        }
    }

    public void GameEnd()
    {
        PauseOn();
        SFXManager.instance.PlayPlayerDeadclip();
        OptionDataManager.instance.ResetPos();
        PlayerStatManager.instance.PlayerStatInit();
        StatPanelDataManager.instance.StatPanelInit();
        UGUIManager.instance.SetActiveGameOverText();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    public string GetSceneName()
    {
        return SceneManager.GetActiveScene().name;
    }

    public void PauseOn()
    {
        Time.timeScale = 0;
        isPause = true;
    }

    public void PauseOff()
    {
        Time.timeScale = 1;
        isPause = false;
    }

    public int GetDamage(int str, int def)
    {
        return str - def;
    }

    public bool GetIsPause()
    {
        return isPause;
    }

    public void SetDeadMonster()
    {
        deadMonster++;
        if (deadMonster > 99) deadMonster = 99;
    }

    public int GetDeadMonster()
    {
        return deadMonster;
    }
}