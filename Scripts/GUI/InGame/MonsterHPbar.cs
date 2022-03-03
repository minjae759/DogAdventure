using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterHPbar : MonoBehaviour
{
    private Transform target;

    private void Awake()
    {
        target = Camera.main.transform;
    }
    void Update()
    {
        transform.LookAt(target);
    }
}
