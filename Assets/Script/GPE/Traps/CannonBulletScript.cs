using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBulletScript : FreezeMasterScript
{
    [SerializeField] private LayerMask floorLayer;
    private float bulletSpeed = 0;

    public void SetBulletSpeed(float newSpeed)
    {
        bulletSpeed = newSpeed;
    }

    private void Update()
    {
        if (!freezed)
        {
            transform.position += transform.right * bulletSpeed * Time.deltaTime;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!freezed)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerMovement>().Die();
            }

            if (collision.gameObject.GetComponent<ShootScript>() == null)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (freezed)
            {
                if (collision.gameObject.GetComponent<CannonBulletScript>() != null)
                {
                    Destroy(collision.gameObject);
                    Destroy(gameObject);
                }
            }
        }
    }
}
