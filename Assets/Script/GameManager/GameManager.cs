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
    [SerializeField] private int money;

    private void Awake()
    {
        instance = this;
    }

    public void SetCoin()
    {
        money++;
        coinText.text = "" + money;
    }
}
