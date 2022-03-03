using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatTable
{
    [SerializeField]
    public int maxLevel = 3;
    [SerializeField]
    public int[] maxHpTable;
    [SerializeField]
    public int[] maxExpTable;
    [SerializeField]
    public int[] maxSTRTable;
    [SerializeField]
    public int[] maxDEFTable;

    public PlayerStatTable()
    {
        maxHpTable = new int[] { 100, 110, 120, 130, 140, 150 };
        maxExpTable = new int[] { 10, 15, 20 };
        maxSTRTable = new int[] { 5, 7, 9, 11, 13, 15 };
        maxDEFTable = new int[] { 1, 2, 3, 4, 5, 6 };
    }
}
