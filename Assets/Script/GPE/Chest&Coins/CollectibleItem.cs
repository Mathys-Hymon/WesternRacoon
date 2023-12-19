using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private Item itemData;
    [SerializeField] private GameObject target;

    public float speed = 10f;
    private bool moveCoin;
    
    void Update()
    {
        if(moveCoin)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //PlayerInventory.Instance.AddItemToInventory(itemData);
            gameObject.GetComponent<Collider2D>().enabled = false;
            moveCoin = true;
            Destroy(gameObject, 1.5f);
        }
    }
    
}

[Serializable]
public class Item
{
    public string uniqueID;
    public Sprite icon;
}
