using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonScript : MonoBehaviour
{

    private bool Activate;
    [SerializeField] private bool PressurePlate;
    [SerializeField] private Sprite[] ButtonsSprites;
    [Header("Sound Effects")]
    [SerializeField] private AudioSource audioSRC;

    private SpriteRenderer SpriteRenderer;
    public bool IsActivated() {  return Activate; }

    private void Start()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<BoxScript>() != null || collision.gameObject.tag == "Player" || collision.gameObject.GetComponent<EnemyScript>() != null && !Activate)
        {
            audioSRC.pitch = 1.0f;
            audioSRC.Play();
            Activate = true;
            SpriteRenderer.sprite = ButtonsSprites[0];
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<BoxScript>() != null || collision.gameObject.tag == "Player" || collision.gameObject.GetComponent<EnemyScript>() != null)
        {
            Activate = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if ((collision.gameObject == PlayerMovement.Instance.gameObject || collision.gameObject.GetComponent<EnemyScript>() != null) && PressurePlate)
        {
            audioSRC.pitch = 0.7f;
            audioSRC.Play();
            Activate = false;
            SpriteRenderer.sprite = ButtonsSprites[1];
        }
    }

}

        

    

