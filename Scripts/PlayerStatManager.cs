using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


public class PlayerStatManager : MonoBehaviour
{
    public static PlayerStatManager instance;

    PlayerStatTable playerStatTable = new PlayerStatTable();
    PlayerStat playerStat = new PlayerStat();

    string DATA_PATH;
    string DATA_FILENAME = "/Playerstat.txt";

    private void Start()
    {
        if (instance == null)
            instance = this;

        DATA_PATH = Application.dataPath + "/Save/";
        if (!Directory.Exists(DATA_PATH))
            Directory.CreateDirectory(DATA_PATH);

        LoadData();
    }

    public void LoadData()
    {
        if (File.Exists(DATA_PATH + DATA_FILENAME))
        {
            string json = File.ReadAllText(DATA_PATH + DATA_FILENAME);
            playerStat = JsonUtility.FromJson<PlayerStat>(json);
        }
        else
        {
            PlayerStatInit();
        }
    }

    public void SaveData()
    {
        string json = JsonUtility.ToJson(playerStat);
        File.WriteAllText(DATA_PATH + DATA_FILENAME, json);
    }

    public void PlayerStatInit()
    {
        // 초기값 설정
        playerStat.LEVEL = 1;
        playerStat.MAXHP = playerStatTable.maxHpTable[0];
        playerStat.HP = playerStat.MAXHP;
        playerStat.EXP = 0;
        playerStat.MAXEXP = playerStatTable.maxExpTable[0];
        playerStat.STR = playerStatTable.maxSTRTable[0];
        playerStat.DEF = 1;
        playerStat.MAXJUMP = 1;

        for (int i = 1; i < playerStat.isHavingWeapon.Count; i++)
        {
            playerStat.isHavingWeapon[i] = false;
        }

        SaveData();
    }

    public int GetLevel()
    {
        return playerStat.LEVEL;
    }

    public int GetMaxHp()
    {
        return playerStat.MAXHP;
    }

    public int GetHp()
    {
        return playerStat.HP;
    }

    public int GetMaxExp()
    {
        return playerStat.MAXEXP;
    }

    public int GetExp()
    {
        return playerStat.EXP;
    }

    public int GetSTR()
    {
        return playerStat.STR;
    }
    public int GetDEF()
    {
        return playerStat.DEF;
    }

    public int GetMaxJump()
    {
        return playerStat.MAXJUMP;
    }

    public int GetMaxLevel()
    {
        return playerStatTable.maxLevel;
    }

    public bool GetIsHavingWeapon(int idx)
    {
        return playerStat.isHavingWeapon[idx];
    }

    public void SetLevel(int value)
    {
        playerStat.LEVEL = value;
    }

    public void SetMaxHp(int idx)
    {
        playerStat.MAXHP = playerStatTable.maxHpTable[idx];
    }

    public void SetHp(int value)
    {
        playerStat.HP = value;
    }

    public void SetMaxExp(int idx)
    {
        playerStat.MAXEXP = playerStatTable.maxExpTable[idx];
    }

    public void SetExp(int value)
    {
        playerStat.EXP= value;
    }
    public void SetSTR(int idx)
    {
        playerStat.STR = playerStatTable.maxSTRTable[idx];
    }

    public void SetDEF(int idx)
    {
        playerStat.DEF = playerStatTable.maxDEFTable[idx];
    }

    public void SetIsHavingWeapon(int idx, bool b)
    {
        playerStat.isHavingWeapon[idx] = b;
    }

}
