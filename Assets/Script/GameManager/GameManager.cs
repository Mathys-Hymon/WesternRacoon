using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    
    [SerializeField] private TextMeshProUGUI coinText;
    [SerializeField] Sprite fullBullet;
    [SerializeField] Sprite emptyBullet;
    [SerializeField] List<Image> bullets = new List<Image>();
    [SerializeField] GameObject[] objectsToKeep;
    
    int freezeCount = 0;
    public int money;

    public static GameManager instance;
    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }

        foreach (var item in objectsToKeep)
        {
            DontDestroyOnLoad(item);
        }

        for (int i = 0; i < bullets.Count; i++)
        { 
            bullets[i].sprite = fullBullet;
        }

        Instance = this;
        coinText.text = "" + money;
    }

    private void Start()
    {
        Invoke("LoadScene", 0.5f);
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

    private void LoadScene()
    {
        SaveSystem.Instance.Load();
    }

    public void SetCoin()
    {
        money++;
        coinText.text = "" + money;
    }
}
