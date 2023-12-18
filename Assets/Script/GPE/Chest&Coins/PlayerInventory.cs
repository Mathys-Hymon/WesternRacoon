using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private const int MAX_INVENTORY = 3;

    private List<Item> items = new List<Item>();

    //static is a keyword used to store a value across all of the instances of a class
    //That means that every new object of this type will share this value
    public static PlayerInventory Instance;

    private void Awake()
    {
        //The player inventory is what we call a Singleton
        //Only one object PlayerInventory exists in the world, so we can share it with the class instead of a specific instance
        //PlayerInventory.Instance gives me the object reference without needing to know which one I am talking about
        Instance = this;
    }

    private void Start()
    {
        InventoryUI.Instance.UpdateUI();
    }

    //Returns true if the item was successfully added, false otherwise
    public bool AddItemToInventory(Item newItem)
    {
        if (items.Count < MAX_INVENTORY)
        {
            items.Add(newItem);
            InventoryUI.Instance.Wait();
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItemFromInventory(string oldItemName)
    {
        Item oldItem = items.Find(item => item.uniqueID == oldItemName);
        if (oldItem != null)
        {
            items.Remove(oldItem);
        }
        InventoryUI.Instance.UpdateUI();
    }



    public bool IsInInventory(Item item)
    {
        return items.Contains(item);
    }

    public bool IsInInventory(string itemName)
    {
        return items.Find(item => item.uniqueID == itemName) != null;
    }

    public List<Item> GetInventory()
    {
        return items;
    }
}
