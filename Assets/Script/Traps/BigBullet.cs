using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBullet : FreezeMasterScript
{
    public LayerMask floorLayer;

    [SerializeField] private float bulletSpeed = 0;

    private void Update()
    {
        if (!freezed)
        {
            transform.position += transform.right * bulletSpeed * Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Rigidbody2D rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
        SpriteRenderer spreiteBullet = gameObject.GetComponent<SpriteRenderer>();
        if (!freezed)
        {
            if(collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerMovement>().Die();
            }
                Destroy(gameObject);
        }
        else
        {
            if (freezed)
            {
                if (collision.gameObject.CompareTag("Bullet"))
                {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
