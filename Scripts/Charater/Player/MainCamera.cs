using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCamera : MonoBehaviour
{

    private Transform cameraTransform;
    float mindis = -5f;
    float maxdis = -2.5f;

    void Start()
    {
        cameraTransform = GetComponent<Transform>();
    }

    void Update()
    {
        AdjacentDistance();
    }

    void AdjacentDistance()
    {
        if(Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            Vector3 cameraPos = cameraTransform.localPosition;
            cameraPos.z += 0.4f;
            if (cameraPos.z > maxdis) cameraPos.z = maxdis;
            cameraTransform.transform.localPosition = cameraPos;
        }
        else if(Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            Vector3 cameraPos = cameraTransform.localPosition;
            cameraPos.z -= 0.4f;
            if (cameraPos.z < mindis) cameraPos.z = mindis;
            cameraTransform.transform.localPosition = cameraPos;
        }
    }
}
