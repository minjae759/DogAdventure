using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectStageScene_Canvas : MonoBehaviour
{

    static public SelectStageScene_Canvas instance;

    GraphicRaycaster graphicRaycaster;
    PointerEventData pointerEventData;
    EventSystem eventSystem;

    Vector2 adjustPos;
    string sceneName;
    int stageNumber;

    public GameObject confirmPanel;

    void Start()
    {
        if (instance == null)
            instance = this;
        graphicRaycaster = GetComponent<GraphicRaycaster>();
        eventSystem = GetComponent<EventSystem>();
        adjustPos = new Vector2(0f, -10f);
    }

    void Update()
    {
        OnClickStage();
        Getkey();
    }

    void OnClickStage()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0) && !confirmPanel.activeSelf)
        {
            pointerEventData = new PointerEventData(eventSystem);
            pointerEventData.position = Input.mousePosition;

            List<RaycastResult> results = new List<RaycastResult>();

            graphicRaycaster.Raycast(pointerEventData, results);

            foreach (RaycastResult result in results)
            {
                if (result.gameObject.name.Contains("Vertex"))
                {
                    // playerDot 과 정점의 위치가 같을 경우
                    if (PlayerDot.instance.GetInStage() && (PlayerDot.instance.GetPlayerDotPos() == (result.gameObject.GetComponent<RectTransform>().anchoredPosition + adjustPos)) )
                    {
                        PlayerDot.instance.SetSelectTrriger();
                        Invoke("CallLoadScene", 2f);
                    }
                    else
                    {
                        Vector2 pos = result.gameObject.GetComponent<RectTransform>().anchoredPosition;
                        pos += adjustPos;
                        PlayerDot.instance.SetTargetPos(pos);
                    }
                }
            }
        }
    }

    void Getkey()
    {
        if (PlayerDot.instance.GetInStage() && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)) )
        {
            PlayerDot.instance.SetSelectTrriger();
            Invoke("CallLoadScene", 2f);
        }
    }

    private void CallLoadScene()
    {
        GameManager.instance.LoadScene(sceneName);
    }

    public void SetSceneName(string name)
    {
        sceneName = name;
    }
}