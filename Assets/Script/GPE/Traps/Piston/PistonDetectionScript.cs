using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PistonDetectionScript : MonoBehaviour
{
    [SerializeField] PistonScript scriptRef;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            scriptRef.PlayerIsHere(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == PlayerMovement.Instance.gameObject)
        {
            scriptRef.PlayerIsHere(false);
        }
    }
}
