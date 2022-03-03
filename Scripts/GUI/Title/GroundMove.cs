using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundMove : MonoBehaviour
{

    public float speed;
    public float gap_x_positive = 0.0f;
    public float gap_y_positive = 0.0f;
    public float gap_z_positive = 0.0f;
    public float gap_x_negative = 0.0f;
    public float gap_y_negative = 0.0f;
    public float gap_z_negative = 0.0f;

    bool direction;

    Vector3 origin_position;
    Vector3 a_position;
    Vector3 b_position;

    void Start()
    {
        speed = 1.0f;
        direction = true;
        origin_position = transform.position;
        a_position = new Vector3(origin_position.x + gap_x_positive, origin_position.y + gap_y_positive, origin_position.z + gap_z_positive);
        b_position = new Vector3(origin_position.x - gap_x_negative, origin_position.y - gap_y_negative, origin_position.z - gap_z_negative);
    }

    void Update()
    {
        if (direction)
        {
            transform.position = Vector3.MoveTowards(transform.position, a_position, Time.deltaTime * speed);
            if (transform.position == a_position) direction = false;
        }
        else if (!direction)
        {
            transform.position = Vector3.MoveTowards(transform.position, b_position, Time.deltaTime * speed);
            if (transform.position == b_position) direction = true;
        }
    }
}
