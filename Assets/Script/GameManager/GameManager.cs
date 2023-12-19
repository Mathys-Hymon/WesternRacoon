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
    public int money;

    private void Awake()
    {
        instance = this;
        coinText.text = "" + money;
    }

    private void Update()
    {
        coinText.text = "" + money;
    }

    public void SetCoin()
    {
        money++;
        coinText.text = "" + money;
    }
}
