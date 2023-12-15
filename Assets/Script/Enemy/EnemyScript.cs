using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : FreezeMasterScript
{
    [SerializeField] private GameObject bulletRef;
    [SerializeField] private LayerMask obstacle;
    [SerializeField] private float delay;

    private Vector2 directiontoTarget;

    private bool canShoot = true;
    private bool isInRange;

    // Update is called once per frame
    void Update()
    {
        if (!freezed && isInRange)
        {
            float distance = Vector3.Distance(transform.position, PlayerMovement.Instance.transform.position);
            RaycastHit2D touchPlayer =
                Physics2D.Raycast(transform.position, (transform.position - PlayerMovement.Instance.transform.position)*(-1), distance, obstacle);
            Debug.DrawRay(transform.position, (transform.position - PlayerMovement.Instance.transform.position)*(-1));

            if (touchPlayer.collider == null && canShoot)
            {
                if (transform.position.x - PlayerMovement.Instance.transform.position.x < 0)
                {
                    Vector3 rotator = new Vector3(transform.rotation.x, 0f, transform.rotation.z);
                    transform.rotation = Quaternion.Euler(rotator);
                }
                else
                {
                    Vector3 rotator = new Vector3(transform.rotation.x, 180f, transform.rotation.z);
                    transform.rotation = Quaternion.Euler(rotator);
                }
                
                canShoot = false;
                Shoot();
            }

           
        }
        
       
    }

    public void Shoot()
    {
        directiontoTarget = PlayerMovement.Instance.transform.position - transform.position;
        float angle = -90+Mathf.Atan2(directiontoTarget.y, directiontoTarget.x) * Mathf.Rad2Deg;
        Instantiate(bulletRef, transform.position, Quaternion.Euler(0,0,angle));
        
        Invoke("ResetShoot",delay);
        
    }

    private void ResetShoot()
    {
        canShoot = true;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject == PlayerMovement.Instance.gameObject)
        {
            isInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == PlayerMovement.Instance.gameObject)
        {
            isInRange = false;
        }
    }
}
