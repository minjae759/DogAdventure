using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundScrolling : MonoBehaviour
{

    public Transform[] spritesTransform;
    public SpriteRenderer[] sprites;
    float[] spriteWidths;

    Vector3 adjustPos;
    public float speed;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        spriteWidths = new float[sprites.Length];

        for (int i = 0; i < sprites.Length; i++)
        {
            sprites[i] = spritesTransform[i].GetComponent<SpriteRenderer>();
            spriteWidths[i] = sprites[i].bounds.size.x;
        }
    }

    void Update()
    {
        Move();
        Scrolling();
    }

    private void Move()
    {
        Vector3 curPos = transform.position;
        Vector3 newPos = Vector3.left * speed * Time.deltaTime;

        transform.position = curPos + newPos;
    }

    private void Scrolling()
    {
        for(int i = 0; i < spritesTransform.Length; i++)
        {
            if(spritesTransform[i].position.x < -1f * spriteWidths[i])
            {
                adjustPos = new Vector3(spriteWidths[i] * 2f, 0f, 0f);
                spritesTransform[i].position += adjustPos;
            }
        }
    }
}
