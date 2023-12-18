using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private Item itemData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInventory.Instance.AddItemToInventory(itemData);
            Destroy(gameObject);
        }
    }
}

[Serializable]
public class Item
{
    public string uniqueID;
    public Sprite icon;
}
