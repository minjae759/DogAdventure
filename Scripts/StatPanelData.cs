using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class StatPanelData
{
    [SerializeField]
    private int hp_idx;
    [SerializeField]
    private int str_idx;
    [SerializeField]
    private int def_idx;
    [SerializeField]
    private int stat_Point;

    public int HP_IDX { get { return hp_idx; } set { hp_idx = value; } }
    public int STR_IDX { get { return str_idx; } set { str_idx = value; } }
    public int DEF_IDX { get { return def_idx; } set { def_idx = value; } }
    public int STAT_POINT { get { return stat_Point; } set { stat_Point = value; } }
}
