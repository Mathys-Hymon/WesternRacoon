using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private Item itemData;

    public float speed = 10f;
    bool moveCoin;

    GameObject target;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("toCoins");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerInventory.Instance.AddItemToInventory(itemData);
            gameObject.GetComponent<Collider2D>().enabled = false;
            moveCoin = true;
            Destroy(gameObject, 5f);
        }
    }

    void Update()
    {
        if(moveCoin)
        {
            transform.position = Vector3.Lerp(transform.position, target.transform.position, speed * Time.deltaTime);
        }
    }
}

[Serializable]
public class Item
{
    public string uniqueID;
    public Sprite icon;
}
