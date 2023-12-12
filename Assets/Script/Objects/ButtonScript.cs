using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    private bool Activate;
    [SerializeField] private bool PressurePlate;
    [SerializeField] private Sprite[] ButtonsSprites;
    private float Delay = 1f;
    private SpriteRenderer SpriteRenderer;

    private void Update()
    {
        if (Delay <= 1f)
        {
            Delay += Time.deltaTime;
        }
    }
    public bool IsActivated() {  return Activate; }

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Box" && Delay >= 1f)
        {
            Activate = true;
            SpriteRenderer.sprite = ButtonsSprites[0];
        }

        else if (collision.gameObject.tag == "Player")
        {
            Activate = true;
            SpriteRenderer.sprite = ButtonsSprites[0];
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Box")
        {
            Activate = true;
        }
        else if (collision.gameObject.tag == "Player")
        {
            Activate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject.tag == "Player" || collision.gameObject.tag == "Box") && PressurePlate)
        {
            Activate = false;
            SpriteRenderer.sprite = ButtonsSprites[1];
        }
    }

}

        

    

