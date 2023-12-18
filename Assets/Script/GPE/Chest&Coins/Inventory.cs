using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    public static InventoryUI Instance;
    [SerializeField] public GameObject inventoryBoxContainer;
    private List<Transform> inventoryBoxes = new List<Transform>();

    private void Awake()
    {
        Instance = this;
        foreach (Transform child in inventoryBoxContainer.transform)
        {
            inventoryBoxes.Add(child);
        }
        
    }
    public void Wait()
    {
        StartCoroutine(DelayedItemAddition());
    }

    public IEnumerator DelayedItemAddition()
    {
        yield return new WaitForSeconds(1f);
        UpdateUI();
    }

    public void UpdateUI()
    {
        int itemCount = 0;
        foreach (Item item in PlayerInventory.Instance.GetInventory())
        {
            if (itemCount < inventoryBoxes.Count)
            {
                Image boxImage = inventoryBoxes[itemCount].GetChild(0).GetComponent<Image>();
                boxImage.sprite = item.icon;
                boxImage.enabled = true;
                itemCount++;
            }
        }

        for (int i = itemCount; i < inventoryBoxes.Count; i++)
        {
            inventoryBoxes[i].GetChild(0).GetComponent<Image>().enabled = false;
        }
    }
}
