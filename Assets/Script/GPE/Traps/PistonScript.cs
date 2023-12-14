using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonScript : FreezeMasterScript
{
    [Header("MOVEMENT INFOS")]
    [SerializeField] private float speed;
    [SerializeField] private float waitTime;
    [SerializeField] private bool startFirst;
    [Header("WORLD TARGET POSITION")]
    [SerializeField] private float targetY_Position;

    private float initialPositionY;
    private float y;

    private bool moveUp;


    void Start()
    {
        initialPositionY = transform.position.y;
        y = initialPositionY;
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
            Vector3 targetPosition = Vector3.Lerp(transform.position, new Vector3(transform.position.x, y, transform.position.z), speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
        }
            
    }

    private void FlipFlopPosition()
    {
        if (!moveUp)
        {
            moveUp = true;
            y = targetY_Position;
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
