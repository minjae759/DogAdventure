using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character
{
    [SerializeField]
    private int hp;
    [SerializeField]
    private int mp;
    [SerializeField]
    private int str;
    [SerializeField]
    private int def;

    public int HP { get { return hp; } set { hp = value; } }
    public int MP { get { return mp; } set { mp = value; } }
    public int STR { get { return str; } set { str = value; } }
    public int DEF { get { return def; } set { def = value; } }
}
