using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestOpen : CollectableItem
{
    public GameObject coinPrefab;

    private bool isOpened = false;
    //private int collisionCount = 0;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player") && !isOpened)
        {
            // Debug.Log("colliding");
            // Animator animator = GetComponent<Animator>();
            // if (animator != null)
            // {
            //     animator.SetTrigger("ChestOpens");
            // }
            SpawnCoin();
            isOpened = true;
            //collisionCount++;
            //AfterSpawnCoin();
        }
    }

    private void SpawnCoin()
    {
        if (coinPrefab != null)
        {
            Instantiate(coinPrefab, transform.position + Vector3.up, Quaternion.identity);
        }
    }
}
