using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public class PistonScript : FreezeMasterScript
{
    [Header("MOVEMENT INFOS\n")]
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    [SerializeField] private bool startFirst;
    [Header("\n\nWORLD DIRECTION AND POSITION")]
    [Header("put the X value if you check isHorizontal, else put the Y value\n")]
    [SerializeField] private bool isHorizontal;
    [SerializeField] private float targetDownPosition;


    private float initialPositionY;
    private float y;

    private bool moveUp;


    void Start()
    {
        if(!isHorizontal)
        {
            y = transform.position.y;
        }
        else
        {
            y = transform.position.x;
        }

        initialPositionY = y;
        if(startFirst)
        {
            FlipFlopPosition();
        }
        else
        {
            Invoke("FlipFlopPosition", waitTime);
        }
    }

    void Update()
    {
        if (!freezed)
        {
            if(!isHorizontal)
            {
                Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y, transform.position.z), speed * Time.deltaTime);
                transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
            }
            else
            {
                Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(y, transform.position.y, transform.position.z), speed * Time.deltaTime);
                transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
            }
        }
            
    }

    private void FlipFlopPosition()
    {
        if (!moveUp)
        {
            moveUp = true;
            y = targetDownPosition;
        }
        else
        {
            moveUp = false;
            y = initialPositionY;
        }

        Invoke("FlipFlopPosition", waitTime);
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!freezed)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerMovement>().Die();
            }
        }
    }
}
