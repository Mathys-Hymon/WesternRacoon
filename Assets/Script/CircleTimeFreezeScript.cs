using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CircleTimeFreezeScript : MonoBehaviour
{
    [SerializeField] private Sprite[] circleImage;
    //private Image image;
    private SpriteRenderer sr;
    private Sprite image;
    private float time;
    private int nextImage;

    private void ChangeClock()
    {
        
        if (nextImage < circleImage.Length)
        {
            sr.sprite = circleImage[nextImage];
            nextImage++;
        }
        else
        {
            Destroy(gameObject);
        }
        
        Invoke("ChangeClock",time/circleImage.Length);
    }

    public void SetTimer(float newTime)
    {
        sr = GetComponent<SpriteRenderer>();
        time = newTime;
        ChangeClock();
    }
}
