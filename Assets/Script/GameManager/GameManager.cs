using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] Sprite fullBullet;
    [SerializeField] Sprite emptyBullet;
    [SerializeField] List<Image> bullets = new List<Image>();
    
    int freezeCount = 0;
    public int money;

    private void Awake()
    {
        instance = this;
        coinText.text = "" + money;
    }

    private void Update()
    {
        coinText.text = "" + money;
        
        if (PlayerMovement.Instance.GetFreezedObject().Count > freezeCount)
        {
            Debug.Log(freezeCount);
            bullets[PlayerMovement.Instance.GetFreezedObject().Count - 1].sprite = emptyBullet;
            freezeCount ++;
        }
        if (PlayerMovement.Instance.GetFreezedObject().Count < freezeCount)
        {
            freezeCount--;
            bullets[freezeCount].sprite = fullBullet;

        }
    }

    public void SetCoin()
    {
        money++;
        coinText.text = "" + money;
    }
}
