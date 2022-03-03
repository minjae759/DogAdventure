using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{

    private Rigidbody rigidbody;
    private Vector3 target;

    float speed = 5f;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void OnEnable()
    {
        target = target - transform.position;
        target = target.normalized;
    }

    private void FixedUpdate()
    {
        rigidbody.velocity = target * speed;
    }

    public void SetTarget(Vector3 pos)
    {
        target = pos + new Vector3(0.0f, 0.5f, 0.0f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ground" || other.tag == "Player")
        {
            gameObject.SetActive(false);
        }
    }
}
