using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : FreezeMasterScript
{
    [SerializeField] private GameObject bulletRef;
    [SerializeField] private float delay;
    
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating(nameof(Shoot),1f,delay);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Shoot()
    {
        if (!freezed)
        {
            Vector2 DirectiontoTarget = PlayerMovement.Instance.transform.position - transform.position;
            float angle = -90+Mathf.Atan2(DirectiontoTarget.y, DirectiontoTarget.x) * Mathf.Rad2Deg;
            Instantiate(bulletRef, transform.position, Quaternion.Euler(0,0,angle));
        }
        
    }
    
}
