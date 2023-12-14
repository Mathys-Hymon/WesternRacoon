using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Piston_Fast : FreezeMasterScript
{
    public float speed;
    public float MaxY;
    public float MinY;
    public float waitTime;

    private bool moveUp = true;


    void Start()
    {
        transform.position = new Vector3(transform.position.x, MaxY);
    }

    void Update()
    {
        if (!freezed)
        {
            // Move up to MaxY
            if (moveUp && transform.position.y < MaxY)
            {
                transform.position += Vector3.up * speed * Time.deltaTime;

            }
            else if(!moveUp && transform.position.y > MinY)
            {
                transform.position += Vector3.down * speed * Time.deltaTime;
            }
            else
            {
                moveUp = !moveUp;
                StartCoroutine(WaitForSecondsCoroutine(waitTime));
            }
        }
    }

    IEnumerator WaitForSecondsCoroutine(float seconds)
    {
        yield return new WaitForSeconds(seconds);
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
