using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private GameObject target;

    public float speed = 10f;
    
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(gameObject, 1.5f);
        }
    }
}
