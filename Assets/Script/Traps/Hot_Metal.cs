using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hot_Metal : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerMovement>().Die();
        }
    }
}
