using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingArea : MonoBehaviour
{
    public static TargetingArea instance;

    [SerializeField]
    private List<GameObject> enemimes;

    void Start()
    {
        if (instance == null)
            instance = this;
        enemimes = new List<GameObject>();
    }

    public Vector3 GetPosition()
    {
        float mindis = 100f;
        Vector3 targetPos = Vector3.zero;

        for (int i = 0; i < enemimes.Count; i++)
        {
            Vector3 enemyPos = enemimes[i].transform.position;
            float dis = Vector3.Distance(transform.position, enemyPos);
            if (mindis > dis)
            {
                mindis = dis;
                targetPos = enemimes[i].transform.position;
            }
        }
        return targetPos;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemimes.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            for (int i = 0; i < enemimes.Count; i++)
            {
                if (other.name == enemimes[i].name)
                {
                    enemimes.RemoveAt(i);
                }
            }
        }
    }
}
