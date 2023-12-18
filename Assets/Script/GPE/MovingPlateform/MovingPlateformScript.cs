using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlateformScript : FreezeMasterScript
{
    [Header("MOVEMENT INFOS\n")]
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;

    [Header("\n\nWORLD DIRECTION AND POSITION")]
    [Header("put the X value if you check isHorizontal, else put the Y value\n")]
    [SerializeField] private bool isHorizontal;
    [SerializeField] private float targetPosition;

    private float initialPositionY;
    private float y;
    private bool goPointB;
    private GameObject target = null;
    private Vector3 offset;


    void Start()
    {
        target = null;
        if (!isHorizontal)
        {
            y = transform.localPosition.y;
        }
        else
        {
            y = transform.localPosition.x;
        }
        initialPositionY = y;
        Invoke("FlipFlopPosition", waitTime);
    }

    private void FlipFlopPosition()
    {
        if (!goPointB)
        {
            goPointB = true;
            y = targetPosition;
        }
        else
        {
            goPointB = false;
            y = initialPositionY;
        }
        Invoke("FlipFlopPosition", waitTime);
    }

    private void Update()
    {
        if (!freezed)
        {
            if (!isHorizontal)
            {
                float targetPosition = Mathf.Lerp(transform.localPosition.y, y, (speed/50) * Time.deltaTime);
                transform.localPosition = new Vector3(0, targetPosition, 0);
            }
            else
            {
                float targetPosition = Mathf.Lerp(transform.localPosition.x, y, (speed/50) * Time.deltaTime);
                transform.localPosition = new Vector3(targetPosition, 0, 0);
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        target = collision.gameObject;
        offset = target.transform.position - transform.position;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        target = null;
    }

    void LateUpdate()
    {
        if (target != null)
        {
            target.transform.position = transform.position + offset;
        }

    }
}
