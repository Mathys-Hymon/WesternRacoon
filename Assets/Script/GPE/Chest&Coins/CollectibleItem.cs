using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CollectableItem : MonoBehaviour
{
    [Header("Sound Effects")]
    [SerializeField] private AudioSource audioSRC;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            audioSRC.Play();
            GameManager.Instance.SetCoin();
            Destroy(gameObject, 0.1f);
        }
    }
}
