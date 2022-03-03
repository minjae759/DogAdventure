using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDot : MonoBehaviour
{
    static public PlayerDot instance;

    public RectTransform playerPos;
    public List<RectTransform> positionList = new List<RectTransform>();

    Animator animator;

    Vector2 nextPos;
    Vector2 targetPos;
    Vector2 adjustPos;
    Vector3 direction;

    float speed;
    float targetRadius;
    float maxDistance;

    int curidx;
    int targetidx;

    bool isMove;
    bool inStage;
    bool isOnTrriger;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

        animator = GetComponent<Animator>();

        speed = 400f;
        curidx = 0;
        targetRadius = 50f;
        maxDistance = 9999f;
        adjustPos = new Vector2(0f, -10f);
        SetStartPlayerPos();
        isMove = false;
        isOnTrriger = false;
    }

    void SetStartPlayerPos()
    {
        playerPos.anchoredPosition = OptionDataManager.instance.GetRecentPos();
        nextPos = playerPos.anchoredPosition;
        targetPos = nextPos;
    }

    void Update()
    {
        if(!isOnTrriger)
        {
            GetInput();
            Move();
            Stop();
            SetNextPos();
        }
    }

    void FindVertex()
    {
        RaycastHit[] raycastHits = Physics.SphereCastAll(transform.position, targetRadius, direction, maxDistance);
        float mindis = 9999f;

        int minidx = 0;

        for (int i = 0; i < raycastHits.Length; i++)
        {
            if (mindis > raycastHits[i].distance && raycastHits[i].distance > 0f)
            {
                mindis = raycastHits[i].distance;
                minidx = i;
                isMove = raycastHits[minidx].transform.gameObject.CompareTag("Vertex");
            }
        }

        if (isMove && raycastHits.Length > 0)
        {
            nextPos = raycastHits[minidx].transform.gameObject.GetComponent<RectTransform>().anchoredPosition;
            nextPos += adjustPos;
            targetPos = nextPos;
            SFXManager.instance.PlayPlayerDotMoveclip();
        }
    }
    private void GetInput()
    {
        if ((Input.GetKeyDown(KeyCode.RightArrow) || (Input.GetKeyDown(KeyCode.D))) && !isMove)
        {
            direction = Vector3.right;
            FindVertex();
        }
        else if ((Input.GetKeyDown(KeyCode.LeftArrow) || (Input.GetKeyDown(KeyCode.A))) && !isMove)
        {
            direction = Vector3.left;
            FindVertex();
        }
        else if ((Input.GetKeyDown(KeyCode.UpArrow) || (Input.GetKeyDown(KeyCode.W))) && !isMove)
        {
            direction = Vector3.up;
            FindVertex();
        }
        else if ((Input.GetKeyDown(KeyCode.DownArrow) || (Input.GetKeyDown(KeyCode.S))) && !isMove)
        {
            direction = Vector3.down;
            FindVertex();
        }
    }

    private void Move()
    {
        if (isMove)
            playerPos.anchoredPosition = Vector2.MoveTowards(playerPos.anchoredPosition, nextPos, speed * Time.deltaTime);
    }

    private void Stop()
    {
        if (playerPos.anchoredPosition == nextPos) isMove = false;
    }

    private void SetNextPos()
    {
        if (!isMove && playerPos.anchoredPosition != targetPos)
        {
            for (int i = 0; i < positionList.Count; i++)
            {
                if (playerPos.anchoredPosition == positionList[i].anchoredPosition + adjustPos)
                {
                    curidx = i;
                }
            }

            for (int i = 0; i < positionList.Count; i++)
            {
                if (targetPos == positionList[i].anchoredPosition + adjustPos)
                {
                    targetidx = i;
                }
            }

            if (curidx > targetidx)
            {
                curidx--;
                nextPos = positionList[curidx].anchoredPosition + adjustPos;
                SFXManager.instance.PlayPlayerDotMoveclip();
                isMove = true;
            }
            else if (curidx < targetidx)
            {
                curidx++;
                nextPos = positionList[curidx].anchoredPosition + adjustPos;
                SFXManager.instance.PlayPlayerDotMoveclip();
                isMove = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Contains("Stage"))
        {
            string sceneName = other.gameObject.name.Substring(0, 6);
            SelectStageScene_Canvas.instance.SetSceneName(sceneName);
            inStage = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name.Contains("Stage"))
        {
            inStage = false;
        }
    }

    public void SetTargetPos(Vector2 pos)
    {
        if (!isMove)
        {
            targetPos = pos;
        }
    }

    public Vector2 GetPlayerDotPos()
    {
        return playerPos.anchoredPosition;
    }

    public void SetSelectTrriger()
    {
        if(inStage)
        {
            isOnTrriger = true;
            OptionDataManager.instance.SetRecentPos(playerPos.anchoredPosition);
            OptionDataManager.instance.SaveOptionData();
            animator.SetTrigger("Select");
        }
    }

    public bool GetInStage()
    {
        return inStage;
    }
}
