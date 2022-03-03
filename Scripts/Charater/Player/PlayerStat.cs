using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class PlayerStat : Character
{
    [SerializeField]
    private int maxhp;
    [SerializeField]
    private int exp;
    [SerializeField]
    private int maxexp;
    [SerializeField]
    private int maxjump;
    [SerializeField]
    private int level;

    public List<bool> isHavingWeapon = new List<bool>() { true, false, false };

    public int MAXHP { get { return maxhp; } set { maxhp = value; } }
    public int EXP { get { return exp; } set { exp = value; } }
    public int MAXEXP { get { return maxexp; } set { maxexp = value; } }
    public int MAXJUMP { get { return maxjump; } set { maxjump = value; } }
    public int LEVEL { get { return level; } set { level = value; } }
}
