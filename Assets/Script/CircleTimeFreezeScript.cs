using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTimeFreezeScript : MonoBehaviour
{
    [SerializeField] private Image[] circleImage;
    private Image image;
    private float time;
    

    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();
    }

    private void ChangeClock()
    {
        
    }

    public void SetTimer(float newTime)
    {
        time = newTime;
    }
}
