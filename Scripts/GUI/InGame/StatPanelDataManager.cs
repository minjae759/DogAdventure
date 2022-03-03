using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/*
 * 레벨업을 하고나서 스텟포인트가 제대로 로드가안됨
 * 스텟패널을 켯다끄고 레벨업을 하면 정상적으로 출력
 * 스텟패널 초기화를 한번 진행하고 해야하는것으로 생각중 어떻게할지
 */

public class StatPanelDataManager : MonoBehaviour
{
    static public StatPanelDataManager instance;

    StatPanelData statPanelData = new StatPanelData();

    string DATA_PATH;
    string DATA_FILENAME = "/StatPanelData.txt";

    private void Awake()
    {
        if(instance == null)
            instance = this;

        DATA_PATH = Application.dataPath + "/Save/";
        if (!Directory.Exists(DATA_PATH))
            Directory.CreateDirectory(DATA_PATH);

        LoadData();
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(statPanelData);
        File.WriteAllText(DATA_PATH + DATA_FILENAME, json);
    }

    public void LoadData()
    {
        if (File.Exists(DATA_PATH + DATA_FILENAME))
        {
            string json = File.ReadAllText(DATA_PATH + DATA_FILENAME);
            statPanelData = JsonUtility.FromJson<StatPanelData>(json);
        }
        else
        {
            StatPanelInit();
        }
    }

    public void StatPanelInit()
    {
        statPanelData.HP_IDX = 0;
        statPanelData.STR_IDX = 0;
        statPanelData.DEF_IDX = 0;
        statPanelData.STAT_POINT = 0;
        SaveData();
    }

    public int GetHpidx()
    {
        return statPanelData.HP_IDX;
    }

    public int GetSTRidx()
    {
        return statPanelData.STR_IDX;
    }

    public int GetDEFidx()
    {
        return statPanelData.DEF_IDX;
    }

    public int GetStatPoint()
    {
        return statPanelData.STAT_POINT;
    }

    public void SetHpidx(int idx)
    {
        statPanelData.HP_IDX = idx;
    }

    public void SetSTRidx(int idx)
    {
        statPanelData.STR_IDX = idx;
    }

    public void SetDEFidx(int idx)
    {
        statPanelData.DEF_IDX = idx;
    }

    public void SetStatPoint(int idx)
    {
        statPanelData.STAT_POINT = idx;
    }
}
