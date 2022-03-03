using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    static public ItemManager instance;
    public List<GameObject> potionPool;

    void Start()
    {
        if (instance == null)
            instance = this;
    }

    public void DropPotion(Vector3 pos)
    {
        bool isDrop = FindProbability();
        int potionidx = Random.Range(0, 3);
        if (isDrop)
        {
            GameObject potion = Instantiate(potionPool[potionidx]);
            potion.transform.position = pos;
        }
    }

    bool FindProbability()
    {
        float value = Random.Range(0f, 1.0f);
        bool result = value < 0.333f ? true : false;
        return result;
    }
}
