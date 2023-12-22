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
    [Header("Sound Effects")]
    [SerializeField] private AudioSource audioSRC;


    private float initialPositionY;
    private float y;

    private bool moveUp;
    private BoxCollider2D bc;

    void Start()
    { 
        bc = GetComponent<BoxCollider2D>();
        bc.enabled = false;
        if (!isHorizontal)
        {
            y = transform.localPosition.y;
        }
        else
        {
            y = transform.localPosition.x;
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
                float targetPosition = Mathf.Lerp(transform.localPosition.y, y, speed * Time.deltaTime);
                transform.localPosition = new Vector3(0, targetPosition, 0);
            }
            else
            {
                float targetPosition = Mathf.Lerp(transform.localPosition.x, y, speed * Time.deltaTime);
                transform.localPosition = new Vector3(targetPosition,0, 0);
            }
        }
    }

    private void FlipFlopPosition()
    {
        if (!moveUp)
        {
            if(!freezed)
            {
                audioSRC.pitch = Random.Range(0.6f, 0.9f);
                audioSRC.Play();
            }
            moveUp = true;
            y = targetDownPosition;
        }
        else
        {
            if (!freezed)
            {
                audioSRC.pitch = Random.Range(1f, 1.15f);
                audioSRC.Play();
            }
            moveUp = false;
            y = initialPositionY;
        }
        Invoke("FlipFlopPosition", waitTime);
    }

    public void PlayerIsHere(bool playerHere)
    {
        bc.enabled = playerHere;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!freezed)
        {

            if (collision.gameObject.CompareTag("Player") && PlayerMovement.Instance.getGrounded())
            {
                collision.gameObject.GetComponent<PlayerMovement>().Die();
            }
        }
    }
}
