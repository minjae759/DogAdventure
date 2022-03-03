using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UGUIManager : MonoBehaviour
{
    public static UGUIManager instance;

    public GameObject pausePanel;

    public Text hpbarText;
    public Text expbarText;
    public Text levelText;

    public Image hpbarImage;
    public Image expbarImage;

    public GameObject textPrefab;
    public GameObject gameoverText;

    private List<GameObject> floatingTextpool = new List<GameObject>();

    int textidx;
    int textCount = 10;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        textidx = 0;
        for (int i = 0; i < textCount; i++)
        {
            GameObject text = Instantiate(textPrefab);
            text.SetActive(false);
            floatingTextpool.Add(text);
        }

        pausePanel = GameObject.FindGameObjectWithTag("PausePanel");
        if (pausePanel != null) pausePanel.SetActive(false);

        SetCursorMode();
    }

    void Update()
    {
        GetShortCutKeys();
    }

    public void DisplayText(Canvas canvas, string text)
    {
        floatingTextpool[textidx].SetActive(true);
        floatingTextpool[textidx].transform.SetParent(canvas.transform);
        floatingTextpool[textidx].GetComponent<Text>().text = text;
        floatingTextpool[textidx].transform.localScale = new Vector3(0.001f, 0.001f, 0.001f);
        floatingTextpool[textidx++].transform.localPosition = new Vector3(0, 0.5f, 0);
        if (textidx >= textCount) textidx = 0;
    }

    public void SetActiveGameOverText()
    {
        if (gameoverText != null)
        {
            gameoverText.SetActive(true);
        }
    }

    private void GetShortCutKeys()
    {
        // esc키로 메뉴호출
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (pausePanel != null)
            {
                if (pausePanel.activeSelf) OffPausePanel();
                else OnPausePanel();
            }
        }

        // LeftControl키로 커서 조절
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            SetCursorNone();
        }
        else if (Input.GetKeyUp(KeyCode.LeftControl))
        {
            SetCursorMode();
        }
    }

    private void SetCursorMode()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName.Contains("Stage") && !sceneName.Contains("Selection"))
        {
            SetCursorLocked();
        }
        else
        {
            SetCursorConfined();
        }
    }

    public void SetCursorLocked()
    {
        // 중앙에 마우스커서 고정,숨기기
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void SetCursorConfined()
    {
        // 창안에 마우스커서 가두기
        Cursor.lockState = CursorLockMode.Confined;
    }

    public void SetCursorNone()
    {
        // 노멀 모드
        Cursor.lockState = CursorLockMode.None;
    }

    public void OnPausePanel()
    {
        SFXManager.instance.PlayPauseclip();
        GameManager.instance.PauseOn();
        SetCursorConfined();
        pausePanel.SetActive(true);

    }

    public void OffPausePanel()
    {
        SFXManager.instance.PlayResumeclip();
        GameManager.instance.PauseOff();
        SetCursorLocked();
        pausePanel.SetActive(false);
    }

    public void AdjacentLevelText(string level)
    {
        if (levelText != null)
        {
            levelText.text = level;
        }
    }

    public void AdjacentHPbarText(string text)
    {
        if (hpbarText != null)
        {
            hpbarText.text = text;
        }
    }

    public void AdjacentHPbar(float fill)
    {
        if (hpbarImage != null)
        {
            hpbarImage.fillAmount = fill;
        }
    }

    public void AdjacentEXPbarText(string text)
    {
        if (expbarText != null)
        {
            expbarText.text = text;
        }
    }

    public void AdjacentEXPbar(float fill)
    {
        if (expbarImage != null)
        {
            expbarImage.fillAmount = fill;
        }
    }
}
