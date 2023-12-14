using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet : FreezeMasterScript
{
    public void OnCollisionEnter2D(Collider2D collision)
    {
        if (!freezed)
        {
            if (collision.gameObject.CompareTag("Bullet"))
            {
                Destroy(collision.gameObject);
            }
            if(collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerMovement>().Die();
            }
        }
    }
}
