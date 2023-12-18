using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : CollectableItem
{
    private void OnCollisionEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            
        }
    }
}
