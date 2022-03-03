using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamageText : MonoBehaviour
{

    private Transform target;

    float speed = 0.1f;

    private void Awake()
    {
        target = Camera.main.transform;
    }

    private void OnEnable()
    {
        Invoke("Disable", 2f);
    }

    void Update()
    {
        transform.rotation = Quaternion.LookRotation(transform.position - target.position);
        transform.Translate(Vector3.up * Time.deltaTime * speed);
    }

    private void Disable()
    {
        gameObject.SetActive(false);
    }
}
