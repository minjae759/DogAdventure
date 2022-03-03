using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawnzone : MonoBehaviour
{
    [SerializeField]
    private int maxMonsterCount;
    [SerializeField]
    private GameObject slimePrefab;

    List<GameObject> objectspool;
    float respawnTime;
    float time;
    int curidx;
    int enemyCount;
    bool isActive;

    private void Start()
    {
        respawnTime = 5f;
        time = 0f;
        curidx = 0;
        enemyCount = 0;
        isActive = false;

        objectspool = new List<GameObject>();

        for (int i = 0; i < maxMonsterCount; i++)
        {
            GameObject slime = Instantiate(slimePrefab);
            slime.transform.position = this.transform.position;
            slime.name = "silme " + i;
            slime.SetActive(false);
            objectspool.Add(slime);
        }
    }

    private void Update()
    {
        SpawnEnemy();
    }

    void SpawnEnemy()
    {
        time += Time.deltaTime;
        CountActiveEnemy();
        if (respawnTime < time && enemyCount < objectspool.Count)
        {
            while (!isActive)
            {
                if (objectspool[curidx].activeSelf == false)
                {
                    objectspool[curidx].transform.position = gameObject.transform.position;
                    objectspool[curidx].SetActive(true);
                    isActive = true;
                    time = 0f;
                }
                curidx++;
                if (curidx >= objectspool.Count) curidx = 0;
            }
            isActive = false;
        }
    }

    void CountActiveEnemy()
    {
        enemyCount = 0;
        for (int i = 0; i < objectspool.Count; i++)
        {
            if (objectspool[i].activeSelf == true) enemyCount++;
        }
    }
}
