using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Piston_Fast : FreezeMasterScript
{
    public float speed;
    public float MaxY;
    public float MinY;
    public float waitTime;

    private bool moveUp = true;


    void Start()
    {
        transform.position = new Vector3(transform.position.x, MinY);
    }

    void Update()
    {

        if (!freezed)
        {
            // Move up to MaxY
            if (moveUp && transform.position.y < MaxY)
            {
                transform.position += Vector3.up * speed * Time.deltaTime;

                if(transform.position.y >= MaxY)
                {
                    StartCoroutine(WaitForSecondsCoroutine(waitTime));
                }
            }
            else if(!moveUp && transform.position.y > MinY)
            {
                transform.position += Vector3.down * speed * Time.deltaTime;

                if (transform.position.y <= MinY)
                {
                    StartCoroutine(WaitForSecondsCoroutine(waitTime));
                }
            }
        }
    }

    IEnumerator WaitForSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if(transform.position.y >= MaxY)
        {
            moveUp = false;
        }
        else if(transform.position.y <= MinY)
        {
            moveUp = true;
        }
        
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
